using System;
using System.ComponentModel;
using SQLite;

namespace BestBet
{
    public class Book
    {

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string name { get; set; }

        public bool isSelected { get; set; }

        public string image { get; set; }

        public Book()
        {

        }
        public Book(string nameIn)
        {
            this.name = nameIn;
            this.isSelected = true;
            switch (nameIn)
            {
                case "PointsBet (US)": this.image = "PointsBet.png"; break;
                case "William Hill (US)": this.image = "WilliamHill.jpg"; break;
                case "LowVig.ag": this.image = "LowVig.png"; break;
                case "MyBookie.ag": this.image = "MyBookie.png"; break;
                case "BetOnline.ag": this.image = "BetOnline.png"; break;
                case "Bookmaker": this.image = "Bookmaker.png"; break;
                default: this.image = $"{nameIn}.png"; break;
            }

        }


        //public bool IsSelected
        //{
        //    get
        //    {
        //        return isSelected;
        //    }

        //    set
        //    {
        //        if (isSelected != value)
        //        {
        //            isSelected = value;
        //            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsSelected"));
        //        }
        //    }

        //}

    }

}
