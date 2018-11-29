using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_TAP_4
{
    public class MacroClass
    {
        public static IEnumerable<T> MacroExpansion<T>(IEnumerable<T> sequence, T value, IEnumerable<T> newValues)
        {
            if(sequence == null)
                throw new ArgumentNullException(nameof(sequence));
            if(newValues == null)
                throw new ArgumentNullException(nameof(newValues));

            if(!sequence.Contains(value))
                return sequence;

            List<T> newList = new List<T>();
          /*Con Iterators
            IEnumerator<T> _sequeEnumerator = sequence.GetEnumerator();

            while (_sequeEnumerator.MoveNext())
            {
                if (_sequeEnumerator.Current.Equals(value))
                {            
                IEnumerator<T> __newValEnum = newValues.GetEnumerator();

                    while (__newValEnum.MoveNext())
                    {
                        newList.Add(__newValEnum.Current);
                    }
                   //_newVaEnum.reset();
                }  
               else newList.Add(_sequeEnumerator.Current);
            }

    */
            foreach (var s in sequence)
            {
                if(s.Equals(value))
                    newList.AddRange(newValues);
                else newList.Add(s);
            }

            return newList;
        }

    }
}
