using System.ComponentModel;
using System.Collections.Generic;

namespace CataclysmModder
{
    /// <summary>
    /// This is a crazy class that wraps one line in a JSON object array and behaves differently
    /// based on its type.
    /// </summary>
    public class GroupedData : INotifyPropertyChanged
    {
        public object data;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Id
        {
            get
            {
                if (data is object[])
                    return (string)((object[])data)[0];
                else
                    return data.ToString();
            }
            set
            {
                if (data is object[])
                    ((object[])data)[0] = value;
                else
                    data = value;

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Display"));
            }
        }
        public int Value
        {
            get
            {
                if (data is object[])
                    return (int)((object[])data)[1];
                else
                    return (int)data; //TODO: bad bad bad
            }
            set
            {
                if (data is object[])
                    ((object[])data)[1] = value;
                else
                    data = value;
                
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Display"));
            }
        }

        public string Display
        {
            get
            {
                if (data is object[]
                    || (data is Dictionary<string, object> && ((Dictionary<string, object>)data).Keys.Count > 1))
                    return Id + " (" + Value + ")";
                else
                    return Id;
            }
        }


        public GroupedData(object data)
        {
            this.data = data;
        }
    }
}
