using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using Xamarin.Essentials;

namespace BestBet.Models
{
    public class Odds 
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public IList<double> h2h { get; set; }
        public IList<double> h2h_lay { get; set; }
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
        public string h2h_home
        {
            get
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
            set
            {
                try
                {
                    
                        h2h_home = value;
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("h2h_home"));
                    
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
        public string home_team { get; set; }
        public List<Site> sites { get; set; }
        public int sites_count { get; set; }
        public string bestHomeOdds { get; set; }
        public string bestAwayOdds { get; set; }
        private string bestHomeSite { get; set; }
        private string bestAwaySite { get; set; }
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


        public string awayTeam
        {
            get
            {
                var test = homeTeamOdds;
                var test2 = awayTeamOdds;
               
                if (home_team == teams[0])
                    return teams[1];
                else
                    return teams[0];
            }
        }

        public List<string> homeTeamOdds
        {
            get
            {
                List<string> arrayOfOdds = new List<string>();
                var tempBestOdds = -100000;
                Site bestSite = new Site();
                
                if (home_team == teams[0])
                {
                    foreach (var site in sites)
                    {
                        var odds = getAmerican(site.odds.h2h[0]);
                        site.odds.IsBestH2HHomeBet = false;
                        if (tempBestOdds < odds)
                        {
                            tempBestOdds = odds;
                            bestSite = site;
                            
                            
                            if (odds > 0)
                            {
                                bestHomeOdds = $"+{odds}";
                                bestHomeSite = site.site_nice;
                                arrayOfOdds.Add($"+{odds}");
                            }
                            else
                            {
                                bestHomeSite = site.site_nice;
                                bestHomeOdds = odds.ToString();
                                arrayOfOdds.Add(odds.ToString());
                            }
                        }
                    }
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("BestHomeSite"));
                   
                    //switch (bestHomeSite)
                    //{
                    //    case "PointsBet (US)": awaySiteImage = "PointsBet.png"; break;
                    //    case "William Hill (US)": awaySiteImage = "WilliamHill.png"; break;
                    //    default: homeSiteImage = $"{bestHomeSite}.png"; break;
                    //}
                    
                }
                else
                {
                    foreach (var site in sites)
                    {
                        var odds = getAmerican(site.odds.h2h[1]);
                        site.odds.IsBestH2HHomeBet = false;
                        if (tempBestOdds < odds)
                        {
                            tempBestOdds = odds;
                            bestSite = site;
                           
                            
                            if (odds > 0)
                            {
                                bestHomeSite = site.site_nice;
                                bestHomeOdds = $"+{odds}";
                                arrayOfOdds.Add($"+{odds}");
                            }
                            else
                            {
                                bestHomeSite = site.site_nice;
                                bestHomeOdds = odds.ToString();
                                arrayOfOdds.Add(odds.ToString());
                            }
                        }
                    }
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("BestHomeSite"));
                    //switch (bestHomeSite)
                    //{
                    //    case "PointsBet (US)": awaySiteImage = "PointsBet.png"; break;
                    //    case "William Hill (US)": awaySiteImage = "WilliamHill.png"; break;
                    //    default: homeSiteImage = $"{bestHomeSite}.png"; break;
                    //}
                    
                }
                bestSite.odds.IsBestH2HHomeBet = true;
                return arrayOfOdds;
            }
        }

       

        public List<string> awayTeamOdds
        {
            get
            {
                var tempBestOdds= -100000;
                List<string> arrayOfOdds = new List<string>();
                Site bestSite = new Site();
                
                if (home_team == teams[0])
                {
                    foreach (var site in sites)
                    {
                        site.odds.IsBestH2HAwayBet = false;
                        var odds = getAmerican(site.odds.h2h[1]);
                        if (tempBestOdds < odds)
                        {
                            tempBestOdds = odds;
                            
                            bestSite = site;
                            

                            if (odds > 0)
                            {
                                bestAwaySite = site.site_nice;
                                bestAwayOdds = $"+{odds}";
                                arrayOfOdds.Add($"+{odds}");
                            }
                            else
                            {
                                bestAwaySite = site.site_nice;
                                bestAwayOdds = odds.ToString();
                                arrayOfOdds.Add(odds.ToString());
                            }
                        }
                    }
                    //switch (bestAwaySite)
                    //{
                    //    case "PointsBet (US)": awaySiteImage = "PointsBet.png"; break;
                    //    case "William Hill (US)": awaySiteImage = "WilliamHill.png"; break;
                    //    default: awaySiteImage = $"{bestAwaySite}.png"; break;
                    //}
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("BestAwaySite"));
                    
                }
                else
                {
                    foreach (var site in sites)
                    {
                        var odds = getAmerican(site.odds.h2h[0]);
                        site.odds.IsBestH2HAwayBet = false;
                        if (tempBestOdds < odds)
                        {
                            tempBestOdds = odds;
                            
                            bestSite = site;

                            
                            if (odds > 0)
                            {
                                bestAwaySite = site.site_nice;
                                bestAwayOdds = $"+{odds}";
                                arrayOfOdds.Add($"+{odds}");
                            }
                            else
                            {
                                bestAwaySite = site.site_nice;
                                bestAwayOdds = odds.ToString();
                                arrayOfOdds.Add(odds.ToString());
                            }
                        }
                    }
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("BestAwaySite"));
                    //switch (bestAwaySite)
                    //{
                    //    case "PointsBet (US)": awaySiteImage = "PointsBet.png"; break;
                    //    case "William Hill (US)": awaySiteImage = "WilliamHill.png"; break;
                    //    default: awaySiteImage = $"{bestAwaySite}.png"; break;
                    //}
                    
                }
                bestSite.odds.IsBestH2HAwayBet = true;
                return arrayOfOdds;
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

        public string awaySiteImage
        {
            get
            {
                switch (BestAwaySite)
                {
                    case "PointsBet (US)": return "PointsBet.png";
                    case "William Hill (US)": return "WilliamHill.jpg";
                    case "LowVig.ag": return "LowVig.png";
                    case "MyBookie.ag": return "MyBookie.png";
                    default: return $"{BestAwaySite}.png";
                }

            }

            set
            {
                awaySiteImage = value;
            }
           
        }

       

        public string homeSiteImage
        {
            get
            {
                switch (BestHomeSite)
                {
                    case "PointsBet (US)": return "PointsBet.png";
                    case "William Hill (US)": return "WilliamHill.jpg";
                    case "LowVig.ag": return "LowVig.png";
                    case "MyBookie.ag": return "MyBookie.png";
                    default: return $"{BestHomeSite}.png";
                }
            }

            set
            {
                awaySiteImage = value;
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

        public string matchTime
        {
            get {
                string matchTimeString = "Loading...";
                if ((DateTimeOffset.UtcNow.ToUnixTimeSeconds() - commence_time) < 0)
                    {
                    matchTimeString = DateTimeOffset.FromUnixTimeSeconds(commence_time).ToLocalTime().ToString("M/d h:mm tt");
                    IsLive = Color.Transparent;
                    isNotLive = true;
                    isMatchLive = false;
                    //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsLive"));
                    matchTimeColor = Color.Black;
                }
                    else
                    {
                        matchTimeString = "LIVE";
                        IsLive = Color.Red;
                        isNotLive = false;
                        isMatchLive = true;
                       // PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsLive"));
                        matchTimeColor = Color.White;
                    }
                return matchTimeString;
            }
             
        }

    }

    public class ListOfOdds
    {
        public bool success { get; set; }
        public ObservableCollection<Match> data { get; set; }

       
    }



    

}
