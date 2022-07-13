using System;

namespace SpintaxSharp
{
    /// <summary>
    /// Spintax generated strings randomizer
    /// </summary>
    /// <example>
    /// <code>
    /// var machine = new SpintaxMachine("Hello {Bob|Tom}!");
    /// // Must be built before accessing strings
    /// machine.Build();
    /// var random = machine.GetRandom();
    /// 
    /// </code>
    /// </example>
    public class SpintaxMachine
    {
        public string Input { get; }
        
        public string[]? All { get; private set; }
        
        public bool IsBuilt => All != null;
        


        public SpintaxMachine(string input)
        {
            Input = input;
            _random = new Random();
        }

        public SpintaxMachine(string input, int seed)
        {
            Input = input;
            _random = new Random(seed);
        }


        public void Build()
        {
            All = Spintax.GenerateAll(Input);
        }
        

        public string GetRandom()
        {
            if (!IsBuilt)
                throw new InvalidOperationException($"Run {nameof(Build)}() before getting random");

            return All![_random.Next(All.Length)];
        }


        private readonly Random _random;
    }
}