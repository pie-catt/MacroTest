using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab_TAP_4 {
    public class MacroClass {
        public static ICollection<T> MacroExpansion<T>(ICollection<T> sequence, T value, ICollection<T> newValues) {

            CheckNotNull(sequence, newValues);

            if (!sequence.Contains(value))
                return sequence;

            var newSequence = new List<T>();

            using (var sequenceEnumerator = sequence.GetEnumerator())
            using (var newValEnum = newValues.GetEnumerator()) {

                while (sequenceEnumerator.MoveNext()) {
                    if (!Equals(sequenceEnumerator.Current, value)) {
                        newSequence.Add(sequenceEnumerator.Current);
                    }
                    else {
                        //Potrei usare List.AddRange(IEnumerable<T>)
                        while (newValEnum.MoveNext()) {
                            newSequence.Add(newValEnum.Current);
                        }
                        newValEnum.Reset();
                    }
                }
            }
            return newSequence;
        }

        private static void CheckNotNull<T>(ICollection<T> sequence, ICollection<T> newValues) {
            if (sequence == null)
                throw new ArgumentNullException(nameof(sequence));
            if (newValues == null)
                throw new ArgumentNullException(nameof(newValues));
        }

        public static void Main() {
            MacroExpansion(new[] { 1, 2, 1, 2, 3 }, 2, new[] { 7, 8, 9 })
                .ToList()
                .ForEach(Console.Write);
            Console.ReadLine();
        }

    }
}
