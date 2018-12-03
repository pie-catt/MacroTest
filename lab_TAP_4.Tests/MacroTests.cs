using Lab_TAP_4;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace lab_TAP_4.Tests
{
    [TestFixture]
    public class MacroTests
    {
        [Test]
        [Category("Exceptions")]
        public void Sequence_Null_ThrowsException()
        {
            Assert.That(() => MacroClass.MacroExpansion(null, 1, null), Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        [Category("Exceptions")]
        public void NewValues_Null_ThrowsException()
        {
            Assert.That(() => MacroClass.MacroExpansion(null, 1, null), Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void IntValueNotInSequence_ReturnsSequence()
        {
            IEnumerable<int> res = MacroClass.MacroExpansion(new[] {1, 2, 1, 2, 3}, 5, new[] {7, 8, 9});
            Assert.That(res, Is.EqualTo(new[] {1, 2, 1, 2, 3}));
        }

        [Test]
        public void StringValueNotInSequence_ReturnsSequence()
        {
            IEnumerable<string> res = MacroClass.MacroExpansion(new[] {"a", "b", "c"}, "f", new[] {"S"});
            Assert.That(res, Is.EqualTo(new[] {"a", "b", "c"}));
        }

        [TestCase(new[] {1, 2, 1, 2, 3}, 5, new[] {7, 8, 9})]
        [TestCase(new[] {1, 2, 1, 2, 3}, 6, new[] {7, 8, 9})]
        [TestCase(new[] {1, 2, 1, 2, 3}, 7, new[] {7, 8, 9})]
        [TestCase(new[] {1, 2, 1, 2, 3}, 8, new[] {7, 8, 9})]
        public void StringValueNotInSequence_ReturnsSequence(int[]sequence, int value, int[] newSequence)
        {
            var res = MacroClass.MacroExpansion(sequence, value, newSequence);
            Assert.That(res, Is.EqualTo(sequence));
        }

        [Test]
        public void OneValueOccurrence_Head()
        {
            IEnumerable<int> res = MacroClass.MacroExpansion(new[] {3, 2, 1, 2, 1}, 3, new[] {7, 8, 9});
            Assert.That(res, Is.EqualTo(new[] {7, 8, 9, 2, 1, 2, 1}));
        }

        [TestCase(new[] {5, 2, 1, 2, 3}, 5, new[] {7, 8, 9}, new[] {7, 8, 9, 2, 1, 2, 3})]
        [TestCase(new[] {1, 2, 5, 2, 3}, 5, new[] {7, 8, 9}, new[] {1, 2, 7, 8, 9, 2, 3})]
        [TestCase(new[] {1, 2, 1, 2, 5}, 5, new[] {7, 8, 9}, new[] {1, 2, 1, 2, 7, 8, 9})]
        public void OneValueOccurence(int[] sequence, int value, int[] newSequence, int[] resultSequence)
        {
            var res = MacroClass.MacroExpansion(sequence, value, newSequence);
            Assert.That(res, Is.EqualTo(resultSequence));
        }
    }
}
