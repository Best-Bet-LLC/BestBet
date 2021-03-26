using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using Xamarin.Essentials;

namespace BestBet.Models
{
    public class Totals
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public IList<double> points { get; set; }
        public IList<double> odds { get; set; }
        public IList<string> position { get; set; }
        private bool isBestOverBet { get; set; }
        private bool isBestUnderBet { get; set; }


        public bool IsBestOverBet
        {
            get
            {
                return isBestOverBet;
            }
            set
            {
                try
                {

                    if (value)
                    {
                        overBackground = ColorConverters.FromHex("#dfad41");
                        overTextColor = Color.White;
                    }
                    else
                    {
                        overBackground = Color.Transparent;
                        overTextColor = ColorConverters.FromHex("#dfad41");
                    }
                    isBestOverBet = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsBestOverBet"));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("overBackground"));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("overTextColor"));

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"crash: {ex.Message}");
                }
            }
        }
        public Color overBackground { get; set; }
        public Color overTextColor { get; set; }

        public bool IsBestUnderBet
        {
            get
            {
                return isBestUnderBet;
            }
            set
            {
                try
                {

                    if (value)
                    {
                        underBackground = ColorConverters.FromHex("#dfad41");
                        underTextColor = Color.White;
                    }
                    else
                    {
                        underBackground = Color.Transparent;
                        underTextColor = ColorConverters.FromHex("#dfad41");
                    }
                    isBestUnderBet = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsBestUnderBet"));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("underBackground"));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("underTextColor"));

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"crash: {ex.Message}");
                }
            }
        }
        public Color underBackground { get; set; }
        public Color underTextColor { get; set; }

        private string overOdds;
        public string OverOdds
        {
            get
            {
                var overIndex = 0;
                if(position[0] == "over")
                {
                    overIndex = 0;
                } else
                {
                    overIndex = 1;
                }
                var americanOdds = getAmerican(odds[overIndex]);
                if (americanOdds > 0)
                {
                    return $"+{americanOdds}";
                }
                else
                {
                    return americanOdds.ToString();
                }

            }
            set
            {
                try
                {

                    overOdds = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("OverOdds"));

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"crash: {ex.Message}");
                }
            }

        }

        private string over;
        public string Over
        {
            get
            {
                var overIndex = 0;
                if (position[0] == "over")
                {
                    overIndex = 0;
                }
                else
                {
                    overIndex = 1;
                }
                var americanOdds = getAmerican(points[overIndex]);
                if (americanOdds > 0)
                {
                    return $"+{americanOdds}";
                }
                else
                {
                    return americanOdds.ToString();
                }

            }
            set
            {
                try
                {

                    over = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Over"));

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"crash: {ex.Message}");
                }
            }

        }

        private string under;

        public string Under
        {
            get
            {
                var underIndex = 0;
                if (position[0] == "under")
                {
                    underIndex = 0;
                }
                else
                {
                    underIndex = 1;
                }
                var americanOdds = getAmerican(points[underIndex]);
                if (americanOdds > 0)
                {
                    return $"+{americanOdds}";
                }
                else
                {
                    return americanOdds.ToString();
                }

            }
            set
            {
                try
                {

                    under = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Under"));

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"crash: {ex.Message}");
                }
            }

        }

        private string underOdds;
        public string UnderOdds
        {
            get
            {
                var underIndex = 0;
                if (position[0] == "under")
                {
                    underIndex = 0;
                }
                else
                {
                    underIndex = 1;
                }
                var americanOdds = getAmerican(odds[underIndex]);
                if (americanOdds > 0)
                {
                    return $"+{americanOdds}";
                }
                else
                {
                    return americanOdds.ToString();
                }

            }
            set
            {
                try
                {

                    underOdds = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("UnderOdds"));

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"crash: {ex.Message}");
                }
            }

        }

        public int getAmerican(double decimalOdds)
        {
            int americanOdds;
            if (decimalOdds == 1)
            {
                return (int)(-100 / (.01));
            }
            if (decimalOdds >= 2.00)
            {
                americanOdds = (int)((decimalOdds - 1) * 100);
            }
            else
            {
                americanOdds = (int)(-100 / (decimalOdds - 1));
            }

            return americanOdds;
        }
    }

    public class Spreads
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public IList<double> odds { get; set; }
        public IList<string> points { get; set; }
    }

    public class Odds 
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public IList<double> h2h { get; set; }
        public IList<double> h2h_lay { get; set; }
        public Totals totals { get; set; }
        public Spreads spreads { get; set; }
        private bool isBestH2HHomeBet { get; set; }
        private bool isBestH2HAwayBet { get; set; }


        public bool IsBestH2HHomeBet
        {
            get
            {
                return isBestH2HHomeBet;
            }
            set
            {
                try
                {
                   
                    if (value)
                    {
                        homeH2HBackground = ColorConverters.FromHex("#dfad41");
                        homeH2HTextColor = Color.White;
                    }
                    else
                    {
                        homeH2HBackground = Color.Transparent;
                        homeH2HTextColor = ColorConverters.FromHex("#dfad41");
                    }
                    isBestH2HHomeBet = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsBestH2HHomeBet"));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("homeH2HBackground"));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("homeH2HTextColor"));

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"crash: {ex.Message}");
                }
            }
        }
        public Color homeH2HBackground { get; set; }
        public Color homeH2HTextColor { get; set; }

        public bool IsBestH2HAwayBet
        {
            get
            {
                return isBestH2HAwayBet;
            }
            set
            {
                try
                {
                    
                    if (value)
                    {
                        awayH2HBackground = ColorConverters.FromHex("#dfad41");
                        awayH2HTextColor = Color.White;
                    }
                    else
                    {
                        awayH2HBackground = Color.Transparent;
                        awayH2HTextColor = ColorConverters.FromHex("#dfad41");
                    }
                    isBestH2HAwayBet = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsBestH2HAwayBet"));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("awayH2HBackground"));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("awayH2HTextColor"));

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"crash: {ex.Message}");
                }
            }
        }
        public Color awayH2HBackground { get; set; }
        public Color awayH2HTextColor { get; set; }

        public string h2h_tie
        {
            get
            {
                if (h2h.Count == 3)
                {
                    var odds = getAmerican(h2h[2]);
                    if (odds > 0)
                    {
                       return $"+{odds}";
                    }
                    else
                    {
                        return odds.ToString();
                    }
                    
                }
                else return null;

            }
            set
            {
                try
                {
                    
                        h2h_tie = value;
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("h2h_tie"));
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"crash: {ex.Message}");
                }
            }
        }

        public string h2h_away
        {
            get
            {
                if (h2h == null)
                {
                    return "N/A";
                }
                else
                {
                    var odds = getAmerican(h2h[1]);
                    if (odds > 0)
                    {
                        return $"+{odds}";
                    }
                    else
                    {
                        return odds.ToString();
                    }
                }

            }
            set
            {
                try
                {
                    
                        h2h_away = value;
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("h2h_away"));
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"crash: {ex.Message}");
                }
            }
        }
        private string h2h_home;
        public string H2H_home
        {
            get
            {
                if (h2h == null)
                {
                    return "N/A";
                } else
                {
                    var odds = getAmerican(h2h[0]);
                    if (odds > 0)
                    {
                        return $"+{odds}";
                    }
                    else
                    {
                        return odds.ToString();
                    }
                }
                
            }
            set
            {
                try
                {
                    
                        h2h_home = value;
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H2H_home"));
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"crash: {ex.Message}");
                }
            }
        }

        public int getAmerican(double decimalOdds)
        {
            int americanOdds;
            if (decimalOdds == 1)
            {
                return (int)(-100 / (.01));
            }
            if (decimalOdds >= 2.00)
            {
                americanOdds = (int)((decimalOdds - 1) * 100);
            }
            else
            {
                americanOdds = (int)(-100 / (decimalOdds - 1));
            }

            return americanOdds;
        }
    }

    public class Site
    {
        public string site_key { get; set; }
        public string site_nice { get; set; }
        public int last_update { get; set; }
        public Odds odds { get; set; }
        
        public string homeSiteImage
        {
            get
            {
                switch (site_nice)
                {
                    case "PointsBet (US)": return "PointsBet.png";
                    case "William Hill (US)": return "WilliamHill.jpg";
                    case "LowVig.ag": return "LowVig.png";
                    case "MyBookie.ag": return "MyBookie.png";
                    case "SugarHouse": return "SugarHouse.png";
                    case "BetOnline.ag": return "BetOnline.png";
                    default: return $"{site_nice}.png";
                }
            }

            set
            {
                homeSiteImage = value;
            }

        }

        public string awaySiteImage
        {
            get
            {
                switch (site_nice)
                {
                    case "PointsBet (US)": return "PointsBet.png";
                    case "William Hill (US)": return "WilliamHill.jpg";
                    case "LowVig.ag": return "LowVig.png";
                    case "MyBookie.ag": return "MyBookie.png";
                    case "Sugarhouse": return "SugarHouse.png";
                    case "BetOnline.ag": return "BetOnline.png";
                    default: return $"{site_nice}.png";
                }
            }

            set
            {
                awaySiteImage = value;
            }

        }

    }

    public class Match : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string sport_key { get; set; }
        public string sport_nice { get; set; }
        public IList<string> teams { get; set; }
        public int commence_time { get; set; }
        public string id { get; set; }
        public string home_team { get; set; }
        public List<Site> sites { get; set; }
        public int sites_count { get; set; }
        public string bestHomeOdds { get; set; }
        public string bestAwayOdds { get; set; }
        private string bestHomeSite { get; set; }
        private string bestAwaySite { get; set; }
        private string bestOverSite { get; set; }
        private string bestUnderSite { get; set; }
        public bool isHot { get; set; }
        public bool isNotHot { get; set; }
        public bool isMatchLive { get; set; }
        public bool isNotLive { get; set; }

        public string BestHomeSite
        {
            get
            {
                return bestHomeSite;
            }
            set
            {
                try
                {
                    if (value != null)
                    {
                        bestHomeSite = value;
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("BestHomeSite"));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"crash: {ex.Message}");
                }
            }
        }


        public string BestAwaySite
        {
            get
            {
                return bestAwaySite;
            }
            set
            {
                try
                {
                    if (value != null)
                    {
                        bestAwaySite = value;
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("BestAwaySite"));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"crash: {ex.Message}");
                }
            }
        }

        public string BestOverSite
        {
            get
            {
                return bestOverSite;
            }
            set
            {
                try
                {
                    if (value != null)
                    {
                        bestOverSite = value;
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("BestOverSite"));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"crash: {ex.Message}");
                }
            }
        }

        public string BestUnderSite
        {
            get
            {
                return bestUnderSite;
            }
            set
            {
                try
                {
                    if (value != null)
                    {
                        bestUnderSite = value;
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("BestUnderSite"));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"crash: {ex.Message}");
                }
            }
        }

        public string awayTeam
        {
            get
            {
                if (home_team == teams[0])
                    return teams[1];
                else
                    return teams[0];
            }
        }

       
        private Color isLive { get; set; }

        public Color IsLive
        {
            get
            {
                return isLive;
            }
            set
            {
                try
                {
                    isLive = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsLive"));

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"crash: {ex.Message}");
                }
            }
        }

        private Color matchTimeColor { get; set; }

        public Color MatchTimeColor
        {
            get
            {
                return matchTimeColor;
            }
            set
            {
                try
                {
                    if (value != null)
                    {
                        matchTimeColor = value;
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MatchTimeColor"));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"crash: {ex.Message}");
                }
            }
        }

        private string matchTime { get; set; }

        public string MatchTime
        {
            get
            {
                return matchTime;
            }
            set
            {
                if (value != null)
                {
                    matchTime = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MatchTime"));
                }
            }

        }

    }

    public class ListOfOdds
    {
        //public bool success { get; set; }
        public ObservableCollection<Match> data { get; set; }

       
    }



    

}
