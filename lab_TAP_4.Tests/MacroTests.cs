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
        public void BothSequences_Null_ThrowsException() {
            Assert.That(() => MacroClass.MacroExpansion(null, 1, null),
                Throws.TypeOf<ArgumentNullException>());
        }

        [Category("Exceptions")]
        [TestCase(null, 1, null)]
        [TestCase(null, 1, new[] { 1, 2 })]
        [TestCase(new[] { 1, 2 }, 1, null)]
        [TestCase(new int[] { }, 1, null)]
        [TestCase(null, 1, new int[] { })]
        public void NullParameters_ThrowsException(int[] sequence, int value, int[] newValues) {
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
        [Category("Correct cases")]
        public void StringValueNotInSequence_ReturnsSequenceWithoutNewValue() {
            var res = MacroClass.MacroExpansion(new[] { "a", "b", "c" }, "f", new[] { "S" });
            Assert.That(res, !Contains.Item("S"));
        }

        [Category("Correct cases")]
        [TestCase(new[] { 1, 2, 1, 2, 3 }, 5, new[] { 7, 8, 9 })]
        [TestCase(new[] { 1, 2, 1, 2, 3 }, 6, new int[] { })]
        [TestCase(new int[] { }, 7, new[] { 7, 8, 9 })]
        [TestCase(new int[] { }, 8, new int[] { })]
        [TestCase(new[] { 1, 2, 1, 2, 3 }, null, new[] { 7, 8, 9 })]
        public void IntValueNotInSequence_ReturnsSequence(int[] sequence, int value, int[] newSequence) {
            var res = MacroClass.MacroExpansion(sequence, value, newSequence);
            Assert.That(res, Is.EqualTo(sequence));
        }

        [Category("Correct cases")]
        [TestCase(new[] { "s1", "s2", "s3", "s4" }, "s5", new[] { "s6", "s7" })]
        [TestCase(new[] { "s1", "s2", "s3", "s4" }, "s5", new string[] { })]
        [TestCase(new string[] { }, "s5", new[] { "s6", "s7" })]
        [TestCase(new string[] { }, "s5", new string[] { })]
        [TestCase(new[] { "" }, "s5", new[] { "s6", "s7" })]
        [TestCase(new[] { "s1", "s2", "s3", "s4" }, "s5", new[] { "" })]
        public void StringValueNotInSequence_ReturnsSequence(string[] sequence, string value, string[] newSequence) {
            var res = MacroClass.MacroExpansion(sequence, value, newSequence);
            Assert.That(res, Is.EqualTo(sequence));
        }

        [Test]
        [Category("Correct cases")]
        public void OneValueOccurrence_Head() {
            var res = MacroClass.MacroExpansion(new[] { 3, 2, 1, 2, 1 }, 3, new[] { 7, 8, 9 });
            Assert.That(res, Is.EqualTo(new[] { 7, 8, 9, 2, 1, 2, 1 }));
        }

        [Test]
        [Category("Correct cases")]
        public void OneValueOccurrence_Middle() {
            var res = MacroClass.MacroExpansion(new[] { 1, 2, 3, 2, 1 }, 3, new[] { 7, 8, 9 });
            Assert.That(res, Is.EqualTo(new[] { 1, 2, 7, 8, 9, 2, 1 }));
        }

        [Test]
        [Category("Correct cases")]
        public void OneValueOccurrence_Tail() {
            var res = MacroClass.MacroExpansion(new[] { 1, 2, 1, 2, 3 }, 3, new[] { 7, 8, 9 });
            Assert.That(res, Is.EqualTo(new[] { 1, 2, 1, 2, 7, 8, 9 }));
        }

        [Category("Correct cases")]
        [TestCase(new[] { 5, 2, 1, 2, 3 }, 5, new[] { 7, 8, 9 }, new[] { 7, 8, 9, 2, 1, 2, 3 })]
        [TestCase(new[] { 1, 2, 5, 2, 3 }, 5, new[] { 7, 8, 9 }, new[] { 1, 2, 7, 8, 9, 2, 3 })]
        [TestCase(new[] { 1, 2, 1, 2, 5 }, 5, new[] { 7, 8, 9 }, new[] { 1, 2, 1, 2, 7, 8, 9 })]
        [TestCase(new[] { 5, 2, 1, 2, 3 }, 5, new int[] { }, new[] { 2, 1, 2, 3 })]
        [TestCase(new[] { 1, 2, 5, 2, 3 }, 5, new int[] { }, new[] { 1, 2, 2, 3 })]
        [TestCase(new[] { 1, 2, 1, 2, 5 }, 5, new int[] { }, new[] { 1, 2, 1, 2 })]
        [TestCase(new[] { 5 }, 5, new int[] { }, new int[] { })]
        [TestCase(new[] { 5 }, 5, new[] { 5 }, new[] { 5 })]
        public void OneIntValueOccurence(int[] sequence, int value, int[] newSequence, int[] resultSequence) {
            var res = MacroClass.MacroExpansion(sequence, value, newSequence);
            Assert.That(res, Is.EqualTo(resultSequence));
        }

        [Category("Correct cases")]
        [TestCase(new[] { "s1", "s2", "s3", "s4", "s5" }, "s1", new[] { "s6", "s7" }, new[] { "s6", "s7", "s2", "s3", "s4", "s5" })]
        [TestCase(new[] { "s1", "s2", "s3", "s4", "s5" }, "s3", new[] { "s6", "s7" }, new[] { "s1", "s2", "s6", "s7", "s4", "s5" })]
        [TestCase(new[] { "s1", "s2", "s3", "s4", "s5" }, "s5", new[] { "s6", "s7" }, new[] { "s1", "s2", "s3", "s4", "s6", "s7" })]
        [TestCase(new[] { "", "s2", "s3", "s4", "s5" }, "", new[] { "s6", "s7" }, new[] { "s6", "s7", "s2", "s3", "s4", "s5" })]
        [TestCase(new[] { "s1", "s2", "", "s4", "s5" }, "", new[] { "s6", "s7" }, new[] { "s1", "s2", "s6", "s7", "s4", "s5" })]
        [TestCase(new[] { "s1", "s2", "s3", "s4", "" }, "", new[] { "s6", "s7" }, new[] { "s1", "s2", "s3", "s4", "s6", "s7" })]
        [TestCase(new[] { "s1", "s2", "s3", "s4", "s5" }, "s1", new[] { "" }, new[] { "", "s2", "s3", "s4", "s5" })]
        [TestCase(new[] { "s1", "s2", "s3", "s4", "s5" }, "s3", new[] { "", "s7" }, new[] { "s1", "s2", "", "s7", "s4", "s5" })]
        [TestCase(new[] { "s1", "s2", "s3", "s4", "s5" }, "s5", new string[] { }, new[] { "s1", "s2", "s3", "s4" })]
        public void OneStringValueOccurence(string[] sequence, string value, string[] newSequence, string[] resultSequence) {
            var res = MacroClass.MacroExpansion(sequence, value, newSequence);
            Assert.That(res, Is.EqualTo(resultSequence));
        }

        [Category("Correct cases")]
        [TestCase(new[] { 5, 2, 1, 2, 5 }, 5, new[] { 7, 8, 9 }, new[] { 7, 8, 9, 2, 1, 2, 7, 8, 9 })]
        [TestCase(new[] { 5, 2, 5, 2, 5 }, 5, new[] { 7, 8, 9 }, new[] { 7, 8, 9, 2, 7, 8, 9, 2, 7, 8, 9 })]
        [TestCase(new[] { 5, 5, 5, 5, 5 }, 5, new[] { 7 }, new[] { 7, 7, 7, 7, 7 })]
        [TestCase(new[] { 5, 2, 1, 2, 5 }, 5, new int[] { }, new[] { 2, 1, 2 })]
        [TestCase(new[] { 5, 5, 5, 5, 5 }, 5, new int[] { }, new int[] { })]
        public void MoreIntValueOccurence(int[] sequence, int value, int[] newSequence, int[] resultSequence) {
            var res = MacroClass.MacroExpansion(sequence, value, newSequence);
            Assert.That(res, Is.EqualTo(resultSequence));
        }

        [Category("Correct cases")]
        [TestCase(new[] { "s1", "s2", "s3", "s4", "s1" }, "s1", new[] { "s6", "s7" }, new[] { "s6", "s7", "s2", "s3", "s4", "s6", "s7" })]
        [TestCase(new[] { "s1", "s1", "s1", "s1", "s1" }, "s1", new[] { "s6", "s7" }, new[] { "s6", "s7", "s6", "s7", "s6", "s7", "s6", "s7", "s6", "s7" })]
        [TestCase(new[] { "s1", "s2", "s3", "s4", "s1" }, "s1", new[] { "" }, new[] { "", "s2", "s3", "s4", "" })]
        [TestCase(new[] { "s1", "s2", "s3", "s4", "s1" }, "s1", new string[] { }, new[] { "s2", "s3", "s4" })]
        [TestCase(new[] { "", "", "", "", "" }, "", new[] { "s6", "s7" }, new[] { "s6", "s7", "s6", "s7", "s6", "s7", "s6", "s7", "s6", "s7" })]
        [TestCase(new[] { "s1", "s1", "s1", "s1", "s1" }, "s1", new[] { "" }, new[] { "", "", "", "", "" })]
        [TestCase(new[] { "s1", "s2", "s3", "s4", "s1" }, "s1", new[] { "s6", "s7" }, new[] { "s6", "s7", "s2", "s3", "s4", "s6", "s7" })]
        [TestCase(new[] { "s1", "s1", "s1", "s1", "s1" }, "s1", new string[] { }, new string[] { })]
        [TestCase(new[] { "s1", "s2", "s3", "s4", "s1" }, "s1", new string[] { }, new[] { "s2", "s3", "s4" })]
        public void MoreStringValueOccurence(string[] sequence, string value, string[] newSequence, string[] resultSequence) {
            var res = MacroClass.MacroExpansion(sequence, value, newSequence);
            Assert.That(res, Is.EqualTo(resultSequence));
        }

    }
}
