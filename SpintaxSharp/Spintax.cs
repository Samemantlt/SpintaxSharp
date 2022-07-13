using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using JetBrains.Annotations;
using PCRE;

namespace SpintaxSharp
{
    public static class Spintax
    {
        /// <summary>
        /// Generate all possible strings based on Spintax syntax
        /// </summary>
        /// <param name="input">Spintax syntax</param>
        /// <returns>All generated strings </returns>
        /// <example>
        /// <code>
        /// var strings = Spintax.GenerateAll("Hello {Bob|Tom}!");
        /// // Results:
        /// // "Hello Bob!"
        /// // "Hello Tom!"
        /// </code>
        /// </example>
        public static string[] GenerateAll(string input)
        {
            return GenerateAllInternal(input).Select(NonEscape).Distinct().ToArray();
        }

        
        private static IEnumerable<string> GenerateAllInternal(string input)
        {
            var toReturn = _groupRegex.ReplaceManyForSingleMatch(input, Replace, out bool success);

            if (!success)
                return toReturn;

            return toReturn.SelectMany(GenerateAllInternal);
        }

        private static string[] Replace(PcreMatch groupMatch)
        {
            var all = GenerateAllInternal(groupMatch.Groups["inner"].Value);

            return all.SelectMany(p => Split(p, _variantRegex)).ToArray();
        }

        private static IEnumerable<string> Split(string input, Regex regex)
        {
            var matches = regex.Matches(input);

            var index = 0;

            foreach (Match match in matches)
            {
                yield return input.Substring(index, match.Index-index);
                index = match.Index + match.Length;
            }

            yield return input.Substring(index);
        }

        private static string NonEscape(string input)
        {
            return input
                .Replace("\\\\", "\\")
                .Replace("\\{", "{")
                .Replace("\\|", "|")
                .Replace("\\}", "}");
        }
        
        
        private static readonly PcreRegex _groupRegex =
            new PcreRegex(@"(?<!\\)((?:\\{2})*)\{(?'inner'((.*?(?R)*)?)*)(?<!\\)((?:\\{2})*)\}");

        private static readonly Regex _variantRegex = new Regex(@"((?:(?<!\\)(?:\\\\)*))(\|)");
    }
}