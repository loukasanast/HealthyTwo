using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    public class ActivitiesList : ICollection<IActivity>
    {
        private IActivity[] inner;
        public int MaxCount { get; private set; }
        public int Count { get; private set; }
        public bool IsReadOnly { get { return false; } }

        public ActivitiesList(int max)
        {
            MaxCount = max;
            inner = new IActivity[MaxCount];
        }

        public IActivity this[int i]
        {
            get
            {
                if(i > Count - 1)
                {
                    throw new IndexOutOfRangeException();
                }

                return inner[i];
            }
            set
            {
                if(i > MaxCount - 1)
                {
                    throw new IndexOutOfRangeException();
                }

                inner[i] = value;
            }
        }

        public void Add(IActivity item)
        {
            if(Count < MaxCount)
            {
                inner[Count] = item;
                Count++;
            }
            else
            {
                throw new ListFullException(3);
            }
        }

        public void Clear()
        {
            if(Count > 0)
            {
                for(int i = 0; i < Count; i++)
                {
                    inner[i] = null;
                }

                Count = 0;
            }
        }

        public bool Contains(IActivity item)
        {
            for(int i = 0; i < Count; i++)
            {
                if(inner[i].Equals(item))
                {
                    return true;
                }
            }

            return false;
        }

        public void CopyTo(IActivity[] array, int arrayIndex)
        {
            for(int i = 0; i < Count; i++)
            {
                array[arrayIndex + i] = inner[i];
            }
        }

        public bool Remove(IActivity item)
        {
            bool result = false;

            for(int i = 0; i < Count; i++)
            {
                if(inner[i].Equals(item))
                {
                    Count--;
                    inner[i] = null;
                    result = true;
                }

                if(result)
                {
                    inner[i] = inner[i + 1];
                }
            }

            return result;
        }

        public IEnumerator<IActivity> GetEnumerator()
        {
            foreach(IActivity d in inner)
            {
                yield return d;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
