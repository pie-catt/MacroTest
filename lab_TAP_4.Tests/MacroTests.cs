using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Lab_TAP_4.Tests {
    [TestFixture]
    public class MacroTests {

        [Test]
        [Category("Exceptions")]
        public void Sequence_Null_ThrowsException() {
            Assert.That(() => MacroClass.MacroExpansion(null, 1, new[] { 1, 2 }),
                Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        [Category("Exceptions")]
        public void NewValues_Null_ThrowsException() {
            Assert.That(() => MacroClass.MacroExpansion(new[] { 1, 2 }, 1, null),
                Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        [Category("Exceptions")]
        public void Sequences_Null_ThrowsException() {
            Assert.That(() => MacroClass.MacroExpansion(null, 1, null),
                Throws.TypeOf<ArgumentNullException>());
        }

        [Category("Exceptions")]
        [TestCase(null, 1, null)]
        [TestCase(null, 1, new[] { 1, 2 })]
        [TestCase(new[] { 1, 2 }, 1, null)]
        public void NullParameters_ThrowsException(ICollection<int> sequence, int value, ICollection<int> newValues) {
            Assert.That(() => MacroClass.MacroExpansion(sequence, value, newValues), 
                Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        [Category("Correct cases")]
        public void IntValueNotInSequence_ReturnsSequence() {
            var res = MacroClass.MacroExpansion(new[] { 1, 2, 1, 2, 3 }, 5, new[] { 7, 8, 9 });
            Assert.That(res, Is.EqualTo(new[] { 1, 2, 1, 2, 3 }));
        }

        [Test]
        [Category("Correct cases")]
        public void IntValueNotInSequence_ReturnsSequenceWithoutNewValue() {
            var res = MacroClass.MacroExpansion(new[] { 1, 2, 1, 2, 3 }, 5, new[] { 7 });
            Assert.That(res, !Contains.Item(7));
        }

        [Test]
        [Category("Correct cases")]
        public void StringValueNotInSequence_ReturnsSequence() {
            var res = MacroClass.MacroExpansion(new[] { "s1", "s2", "s3", "s4" }, "s5", new[] { "s1" });
            Assert.That(res, Is.EqualTo(new[] { "s1", "s2", "s3", "s4" }));
        }

        [Test]
        public void StringValueNotInSequence_ReturnsSequenceWithoutNewValue() {
            var res = MacroClass.MacroExpansion(new[] { "a", "b", "c" }, "f", new[] { "S" });
            Assert.That(res, !Contains.Item("S"));
        }

        [TestCase(new[] { 1, 2, 1, 2, 3 }, 5, new[] { 7, 8, 9 })]
        public void IntValueNotInSequence_ReturnsSequence(int[] sequence, int value, int[] newSequence) {
            var res = MacroClass.MacroExpansion(sequence, value, newSequence);
            Assert.That(res, Is.EqualTo(sequence));
        }

        [Test]
        public void OneValueOccurrence_Head() {
            var res = MacroClass.MacroExpansion(new[] { 3, 2, 1, 2, 1 }, 3, new[] { 7, 8, 9 });
            Assert.That(res, Is.EqualTo(new[] { 7, 8, 9, 2, 1, 2, 1 }));
        }

        [Test]
        public void OneValueOccurrence_Middle() {
            var res = MacroClass.MacroExpansion(new[] { 1, 2, 3, 2, 1 }, 3, new[] { 7, 8, 9 });
            Assert.That(res, Is.EqualTo(new[] { 1, 2, 7, 8, 9, 2, 1 }));
        }

        [Test]
        public void OneValueOccurrence_Tail() {
            var res = MacroClass.MacroExpansion(new[] { 1, 2, 1, 2, 3 }, 3, new[] { 7, 8, 9 });
            Assert.That(res, Is.EqualTo(new[] { 1, 2, 1, 2, 7, 8, 9  }));
        }

        [TestCase(new[] { 5, 2, 1, 2, 3 }, 5, new[] { 7, 8, 9 }, new[] { 7, 8, 9, 2, 1, 2, 3 })]
        [TestCase(new[] { 1, 2, 5, 2, 3 }, 5, new[] { 7, 8, 9 }, new[] { 1, 2, 7, 8, 9, 2, 3 })]
        [TestCase(new[] { 1, 2, 1, 2, 5 }, 5, new[] { 7, 8, 9 }, new[] { 1, 2, 1, 2, 7, 8, 9 })]
        public void OneValueOccurence(int[] sequence, int value, int[] newSequence, int[] resultSequence) {
            var res = MacroClass.MacroExpansion(sequence, value, newSequence);
            Assert.That(res, Is.EqualTo(resultSequence));
        }
    }
}
