using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Lib
{
    public class Device : INotifyPropertyChanged, IEquatable<Device>
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public DateTime LastSuccessfulSync { get; set; }
        public DeviceFamily DeviceFamily { get; set; }
        public string HardwareVersion { get; set; }
        public string SoftwareVersion { get; set; }
        public string ModelName { get; set; }
        public string Manufacturer { get; set; }
        public DeviceStatus DeviceStatus { get; set; }
        public DateTime CreatedDate { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if(handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        public bool Equals(Device other)
        {
            if(Id == other.Id)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public enum DeviceFamily : Byte
    {
        Mobile,
        Tablet,
        Desktop
    }

    public enum DeviceStatus : Byte
    {
        On,
        Off
    }
}
