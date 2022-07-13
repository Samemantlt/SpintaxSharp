using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using PCRE;

namespace SpintaxSharp
{
    internal static class PcreRegexExtensions
    {
        /// <summary>
        /// Match input string and returns array with replaced strings
        /// </summary>
        /// <param name="replace">Function that returns array of strings</param>
        /// <param name="success">True if found match, else false</param>
        /// <example>
        /// <code>
        /// var regex = new PcreRegex("123");
        /// var replaced = regex.ReplaceManyForSingleMatch("abc123abc", () => new [] { "xyz", "321" }, out bool _);
        /// // Result:
        /// // "abcxyzabc"
        /// // "abc321abc"
        /// </code>
        /// </example>
        /// <returns></returns>
        public static string[] ReplaceManyForSingleMatch(this PcreRegex regex, string input, ManyPcreMatchEvaluator replace,
            out bool success)
        {
            PcreMatch match = regex.Match(input);

            if (!match.Success)
            {
                success = false;
                return new[] {input};
            }

            string[] array = replace(match);

            string[] output = new string[array.Length];

            for (int i = 0; i < array.Length; i++)
            {
                string toReplace = array[i];

                output[i] = regex.Replace(input, toReplace, 1);
            }

            success = true;
            return output;
        }
    }

    /*
    public class SpintaxMachine
    {
        private string _randomString = null!;
        
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        private static T[] Sample<T>(Random random, IEnumerable<T> collection, int count)
        {
            IEnumerable<T> enumerable = collection as T[]
                                        ?? collection as List<T>
                                        ?? (IEnumerable<T>) collection.ToArray();

            if (enumerable.Count() > count)
                throw new ArgumentOutOfRangeException(nameof(count), count,
                    $"{nameof(count)} cannot be greater than collection's length");

            return enumerable
                .Select(p => new
                {
                    Index = random.Next(0, int.MaxValue),
                    Value = p
                }).OrderBy(p => p.Index)
                .Select(p => p.Value)
                .Take(count)
                .ToArray();
        }


        private static readonly Regex _spintaxSeparator =
            new Regex(@"((?:(?<!\\)(?:\\\\)*))(\|)", RegexOptions.Compiled);

        private static readonly Regex _spintaxBracket =
            new Regex(@"(?<!\\)((?:\\{2})*)\{([^}{}]+)(?<!\\)((?:\\{2})*)\}", RegexOptions.Compiled);

        public 

        public string Spin(string str, int? seed = null)
        {
            var random = new Random(seed ?? Environment.TickCount);

            var characters = Enumerable.Range(1234, 1368).Select(p => (char) p);
            _randomString = string.Join("", Sample(random, characters, 30))

            string ReplaceString(Match match)
            {
                var testString = _spintaxSeparator.Replace(match.Groups[2].Value, x => x.Groups[1] + _randomString);
                var splitStrings = Regex.Split(testString, _randomString);
                var randomPicked = splitStrings[random.Next(splitStrings.Length)];
                return match.Groups[1].Value + randomPicked + 
            }
            
            while (true)
            {
                var newString = _spintaxBracket.Replace(str, ReplaceString);
                
                if (newString == str)
                    break;
                
                str = newString;
            }
            

# Replaces the literal |, {,and }.
            string = re.sub(r'\\([{}|])', r'\1', string)
# Removes double \'s
            string = re.sub(r'\\{2}', r'\\', string)

            return string
        }
    }*/
}