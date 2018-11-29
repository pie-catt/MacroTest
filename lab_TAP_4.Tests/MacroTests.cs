using Lab_TAP_4;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Internal;
using Moq;

namespace lab_TAP_4.Tests
{
    [TestFixture]
    public class MacroTests
    {
        [Test]
        public void Sequence_Null_ThrowsException()
        {
            Assert.That(() => MacroClass.MacroExpansion(null, 1, null), Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void newValues_Null_ThrowsException()
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
        public void StringValueNotInSequence_ReturnsSequence(int[]_sequence, int _value, int[] _newSequence)
        {
            var res = MacroClass.MacroExpansion(_sequence, _value, _newSequence);
            Assert.That(res, Is.EqualTo(_sequence));
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
        public void OneValueOccurence(int[] _sequence, int _value, int[] _newSequence, int[] _res)
        {
            var res = MacroClass.MacroExpansion(_sequence, _value, _newSequence);
            Assert.That(res, Is.EqualTo(_res));
        }
    }
}
