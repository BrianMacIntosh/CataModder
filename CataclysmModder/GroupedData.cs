using System;
using System.Collections.Generic;
using System.ComponentModel;

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
                else if (data is Dictionary<string, object>)
                {
                    Dictionary<string, object> dict = (Dictionary<string, object>)data;
                    if (dict.ContainsKey("name"))
                        return (string)dict["name"];
                    else if (dict.ContainsKey("type"))
                        return (string)dict["type"];
                    else
                        throw new NotImplementedException();
                }
                else
                    return data.ToString();
            }
            set
            {
                if (data is object[])
                    ((object[])data)[0] = value;
                else if (data is Dictionary<string, object>)
                {
                    Dictionary<string, object> dict = (Dictionary<string, object>)data;
                    if (dict.ContainsKey("name"))
                        dict["name"] = value;
                    else if (dict.ContainsKey("type"))
                        dict["type"] = value;
                    else
                        throw new NotImplementedException();
                }
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
                else if (data is Dictionary<string, object>)
                {
                    Dictionary<string, object> dict = (Dictionary<string, object>)data;
                    if (dict.ContainsKey("level"))
                        return (int)dict["level"];
                    else if (dict.ContainsKey("intensity"))
                        return (int)dict["intensity"];
                    else
                        throw new NotImplementedException();
                }
                else
                    return (int)data; //TODO: bad bad bad
            }
            set
            {
                if (data is object[])
                    ((object[])data)[1] = value;
                else if (data is Dictionary<string, object>)
                {
                    Dictionary<string, object> dict = (Dictionary<string, object>)data;
                    if (dict.ContainsKey("level"))
                        dict["level"] = value;
                    else if (dict.ContainsKey("intensity"))
                        dict["intensity"] = value;
                    else
                        throw new NotImplementedException();
                }
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
                if (data is object[] || data is Dictionary<string, object>)
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
