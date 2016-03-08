using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Lib
{
    public class DevicesList : ICollection<Device>
    {
        private Device[] inner;
        public int MaxCount { get; private set; }
        public int Count { get; private set; }
        public bool IsReadOnly { get { return false; } }

        public DevicesList(int max)
        {
            MaxCount = max;
            inner = new Device[MaxCount];
        }

        public Device this[int i]
        {
            get { return inner[i]; }
            set { inner[i] = value; }
        }

        public void Add(Device item)
        {
            if(Count < MaxCount)
            {
                inner[Count] = item;
                Count++;
            }
            else
            {
                throw new DeviceListFullException(3);
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

        public bool Contains(Device item)
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

        public void CopyTo(Device[] array, int arrayIndex)
        {
            for(int i = 0; i < Count; i++)
            {
                array[arrayIndex + i] = inner[i];
            }
        }

        public bool Remove(Device item)
        {
            bool result = false;

            for(int i = 0; i < Count; i++)
            {
                if(inner[i].Equals(item))
                {
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

        public IEnumerator<Device> GetEnumerator()
        {
            foreach(Device d in inner)
            {
                yield return d;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class DeviceListFullException : Exception
    {
        public int MaxCount { get; private set; }
        public override string  Message { get { return string.Format("You cannot add more than {0} devices to this collection.", MaxCount); } }

        public DeviceListFullException(int max)
            : base()
        {
            MaxCount = max;
        }

        public DeviceListFullException(string message, int max)
            : base(message)
        {
            MaxCount = max;
        }

        public DeviceListFullException(string message, Exception innerException, int max)
            : base(message, innerException)
        {
            MaxCount = max;
        }

        public DeviceListFullException(SerializationInfo info, StreamingContext context, int max)
            : base(info, context)
        {
            MaxCount = max;
        }
    }
}
