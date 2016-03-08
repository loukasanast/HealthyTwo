using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Lib
{
    public class Profile : INotifyPropertyChanged
    {
        public string FirstName { get; set; }
        public DateTime Birthdate { get; set; }
        public string PostalCode { get; set; }
        public Gender Gender { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public CultureInfo PreferredLocale { get; set; }
        public DateTime LastUpdateTime { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if(handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }

    public enum Gender : Byte
    {
        Male,
        Female
    }
}
