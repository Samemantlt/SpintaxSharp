using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SpintaxSharp.Tests
{
    [TestClass]
    public class SpintaxTests
    {
        [TestMethod]
        public void All_BasicSpintax()
        {
            // Prepare
            var input = "Hello, {Tom|Bobby}!";

            // Act
            var result = Spintax.GenerateAll(input);

            // Check
            Debug.Assert(result.SequenceEqual(new[]
            {
                "Hello, Tom!",
                "Hello, Bobby!"
            }));
        }
        
        [TestMethod]
        public void All_BasicSpintaxWithEscapeSequences()
        {
            // Prepare
            var input = @"Hello \{user\}, {Tom\{us\|er\}|Bobby}!";

            // Act
            var result = Spintax.GenerateAll(input);

            // Check
            Debug.Assert(result.SequenceEqual(new[]
            {
                "Hello {user}, Tom{us|er}!",
                "Hello {user}, Bobby!"
            }));
        }
        
        [TestMethod]
        public void All_NestedSpintax()
        {
            // Prepare
            var input = "Hello, {Tom|Bobby {Singer|Robot}}!";

            // Act
            var result = Spintax.GenerateAll(input);

            // Check
            Debug.Assert(result.SequenceEqual(new[]
            {
                "Hello, Tom!",
                "Hello, Bobby Singer!",
                "Hello, Bobby Robot!"
            }));
        }
        
        [TestMethod]
        public void All_NestedSpintaxWithEscapeSequences()
        {
            // Prepare
            var input = @"Hello \{user\}, {Tom \{user\}|Bobby {Singer\|Swinger|Robot}}!";

            // Act
            var result = Spintax.GenerateAll(input);

            // Check
            Debug.Assert(result.SequenceEqual(new[]
            {
                @"Hello {user}, Tom {user}!",
                @"Hello {user}, Bobby Singer|Swinger!",
                @"Hello {user}, Bobby Robot!"
            }));
        }
        
        [TestMethod]
        public void All_VeryNestedSpintax()
        {
            // Prepare
            var input = "Do you want {some bread|some milk|{strawberry {ice cream|donut}|banana {ice cream|donut}|chocolate {ice cream|donut}}}{?|!}";

            
            // Act
            var result = Spintax.GenerateAll(input);
            
            
            // Check
            Debug.Assert(result.SequenceEqual(new[]
            {
                @"Do you want some bread?",
                @"Do you want some bread!",
                @"Do you want some milk?",
                @"Do you want some milk!",
                @"Do you want strawberry ice cream?",
                @"Do you want strawberry ice cream!",
                @"Do you want banana ice cream?",
                @"Do you want banana ice cream!",
                @"Do you want chocolate ice cream?",
                @"Do you want chocolate ice cream!",
                @"Do you want chocolate donut?",
                @"Do you want chocolate donut!",
                @"Do you want banana donut?",
                @"Do you want banana donut!",
                @"Do you want strawberry donut?",
                @"Do you want strawberry donut!",
            }));
        }
        
        [TestMethod]
        public void All_VeryVeryNestedSpintaxWithEscapeSequences()
        {
            // Prepare
            var input = @"{Do|Did|} you \{want\} {some \{bre\|ad\}|some mi\|lk|{straw\|berry {ice cream|donut}|banana {ice cream|donut}|choco\|late {ice cream|donut}}}{?|!}";

            
            // Act
            var result = Spintax.GenerateAll(input);
            
            
            // Check
            Debug.Assert(result.SequenceEqual(new[]
            {
                @"Do you {want} some {bre|ad}?",
                @"Do you {want} some {bre|ad}!",
                @"Do you {want} some mi|lk?",
                @"Do you {want} some mi|lk!",
                @"Do you {want} straw|berry ice cream?",
                @"Do you {want} straw|berry ice cream!",
                @"Do you {want} banana ice cream?",
                @"Do you {want} banana ice cream!",
                @"Do you {want} choco|late ice cream?",
                @"Do you {want} choco|late ice cream!",
                @"Do you {want} choco|late donut?",
                @"Do you {want} choco|late donut!",
                @"Do you {want} banana donut?",
                @"Do you {want} banana donut!",
                @"Do you {want} straw|berry donut?",
                @"Do you {want} straw|berry donut!",

                @"Did you {want} some {bre|ad}?",
                @"Did you {want} some {bre|ad}!",
                @"Did you {want} some mi|lk?",
                @"Did you {want} some mi|lk!",
                @"Did you {want} straw|berry ice cream?",
                @"Did you {want} straw|berry ice cream!",
                @"Did you {want} banana ice cream?",
                @"Did you {want} banana ice cream!",
                @"Did you {want} choco|late ice cream?",
                @"Did you {want} choco|late ice cream!",
                @"Did you {want} choco|late donut?",
                @"Did you {want} choco|late donut!",
                @"Did you {want} banana donut?",
                @"Did you {want} banana donut!",
                @"Did you {want} straw|berry donut?",
                @"Did you {want} straw|berry donut!",

                @" you {want} some {bre|ad}?",
                @" you {want} some {bre|ad}!",
                @" you {want} some mi|lk?",
                @" you {want} some mi|lk!",
                @" you {want} straw|berry ice cream?",
                @" you {want} straw|berry ice cream!",
                @" you {want} banana ice cream?",
                @" you {want} banana ice cream!",
                @" you {want} choco|late ice cream?",
                @" you {want} choco|late ice cream!",
                @" you {want} choco|late donut?",
                @" you {want} choco|late donut!",
                @" you {want} banana donut?",
                @" you {want} banana donut!",
                @" you {want} straw|berry donut?",
                @" you {want} straw|berry donut!",
            }));
        }
    }
}