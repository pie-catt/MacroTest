using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_TAP_4 {
    public class MacroClass {
        public static IEnumerable<T> MacroExpansion<T>(IEnumerable<T> sequence, T value, IEnumerable<T> newValues) {
            if (sequence == null)
                throw new ArgumentNullException(nameof(sequence));
            if (newValues == null)
                throw new ArgumentNullException(nameof(newValues));

            if (!sequence.Contains(value))
                return sequence;

            var newSequence = new List<T>();

            var sequenceEnumerator = sequence.GetEnumerator();
            var __newValEnum = newValues.GetEnumerator();

            while (sequenceEnumerator.MoveNext()) {
                if (!sequenceEnumerator.Current.Equals(value)) {
                    newSequence.Add(sequenceEnumerator.Current);
                }
                else {
                    while (__newValEnum.MoveNext()) {
                        newSequence.Add(__newValEnum.Current);
                    }
                    __newValEnum.Reset();
                }
            }

            return newSequence;
        }

    }
}
