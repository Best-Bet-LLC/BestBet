using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;

namespace BestBet
{
    public class Sport : INotifyPropertyChanged
    {
        //public Sport()
        //{

        //}

        public event PropertyChangedEventHandler PropertyChanged;

        public string key { get; set; }

        public string image_key { get; set; }

        public bool active { get; set; }

        public string group { get; set; }

        public string details { get; set; }

        public string title { get; set; }

        public bool has_outrights { get; set; }

        private bool isSelected { get; set; }

        public bool IsSelected
        {
            get
            {
                return isSelected;
            }
            set
            {
                try
                {
                    if (value != null)
                    {
                        isSelected = value;
                       
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsSelected"));
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("BackgroundColor"));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"crash: {ex.Message}");
                }
            }
        }

        private Color backgroundColor { get; set; }

        public Color BackgroundColor
        {
            get
            {
                if (isSelected)
                {
                    return Color.LightBlue;
                }
                else return Color.White;
            }
            set
            {
                try
                {
                    if (value != null)
                    {
                        backgroundColor = value;
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("BackgroundColor"));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"crash: {ex.Message}");
                }
            }
        }
    }

    //public class ThreeSports
    //{
    //    public Sport sport1 { get; set; }
    //    public Sport sport2 { get; set; }
    //    public Sport sport3 { get; set; }
    //}

    public class ListOfSports
    {
        public bool success { get; set; }
        public ObservableCollection<Sport> data { get; set; }
    }
}
