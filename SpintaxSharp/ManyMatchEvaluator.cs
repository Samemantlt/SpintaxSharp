using System.Text.RegularExpressions;
using PCRE;

namespace SpintaxSharp
{
    // public delegate string[] ManyMatchEvaluator(Match match);
    public delegate string[] ManyPcreMatchEvaluator(PcreMatch match);
}