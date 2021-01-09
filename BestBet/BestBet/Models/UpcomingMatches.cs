﻿//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.ComponentModel;
//using System.Drawing;

//namespace BestBet.Models
//{
  

//    public class UpcomingMatches : INotifyPropertyChanged
//    {
//        public event PropertyChangedEventHandler PropertyChanged;
//        public string sport_key { get; set; }
//        public string sport_nice { get; set; }
//        public IList<string> teams { get; set; }
//        public int commence_time { get; set; }
//        public string home_team { get; set; }
//        public List<Site> sites { get; set; }
//        public int sites_count { get; set; }
//        public string bestHomeOdds { get; set; }
//        public string bestAwayOdds { get; set; }
//        private string bestHomeSite { get; set; }
//        private string bestAwaySite { get; set; }

//        public string BestHomeSite
//        {
//            get
//            {
//                return bestHomeSite;
//            }
//            set
//            {
//                try
//                {
//                    if (value != null)
//                    {
//                        bestHomeSite = value;
//                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("BestHomeSite"));
//                    }
//                }
//                catch (Exception ex)
//                {
//                    Console.WriteLine($"crash: {ex.Message}");
//                }
//            }
//        }


//        public string BestAwaySite
//        {
//            get
//            {
//                return bestAwaySite;
//            }
//            set
//            {
//                try
//                {
//                    if (value != null)
//                    {
//                        bestAwaySite = value;
//                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("BestAwaySite"));
//                    }
//                }
//                catch (Exception ex)
//                {
//                    Console.WriteLine($"crash: {ex.Message}");
//                }
//            }
//        }


//        public string awayTeam
//        {
//            get
//            {
//                var test = homeTeamOdds;
//                var test2 = awayTeamOdds;

//                if (home_team == teams[0])
//                    return teams[1];
//                else
//                    return teams[0];
//            }
//        }

//        public List<string> homeTeamOdds
//        {
//            get
//            {
//                List<string> arrayOfOdds = new List<string>();
//                var tempBestOdds = -100000;
//                if (home_team == teams[0])
//                {
//                    foreach (var site in sites)
//                    {
//                        var odds = getAmerican(site.odds.h2h[0]);
//                        if (tempBestOdds < odds)
//                        {
//                            tempBestOdds = odds;
//                            if (odds > 0)
//                            {
//                                bestHomeOdds = $"+{odds}";
//                                bestHomeSite = site.site_nice;
//                                arrayOfOdds.Add($"+{odds}");
//                            }
//                            else
//                            {
//                                bestHomeSite = site.site_nice;
//                                bestHomeOdds = odds.ToString();
//                                arrayOfOdds.Add(odds.ToString());
//                            }
//                        }
//                    }
//                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("BestHomeSite"));
//                    //switch (bestHomeSite)
//                    //{
//                    //    case "PointsBet (US)": awaySiteImage = "PointsBet.png"; break;
//                    //    case "William Hill (US)": awaySiteImage = "WilliamHill.png"; break;
//                    //    default: homeSiteImage = $"{bestHomeSite}.png"; break;
//                    //}
//                    return arrayOfOdds;
//                }
//                else
//                {
//                    foreach (var site in sites)
//                    {
//                        var odds = getAmerican(site.odds.h2h[1]);
//                        if (tempBestOdds < odds)
//                        {
//                            tempBestOdds = odds;
//                            if (odds > 0)
//                            {
//                                bestHomeSite = site.site_nice;
//                                bestHomeOdds = $"+{odds}";
//                                arrayOfOdds.Add($"+{odds}");
//                            }
//                            else
//                            {
//                                bestHomeSite = site.site_nice;
//                                bestHomeOdds = odds.ToString();
//                                arrayOfOdds.Add(odds.ToString());
//                            }
//                        }
//                    }
//                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("BestHomeSite"));
//                    //switch (bestHomeSite)
//                    //{
//                    //    case "PointsBet (US)": awaySiteImage = "PointsBet.png"; break;
//                    //    case "William Hill (US)": awaySiteImage = "WilliamHill.png"; break;
//                    //    default: homeSiteImage = $"{bestHomeSite}.png"; break;
//                    //}
//                    return arrayOfOdds;
//                }
//            }
//        }



//        public List<string> awayTeamOdds
//        {
//            get
//            {
//                var tempBestOdds = -100000;
//                List<string> arrayOfOdds = new List<string>();
//                if (home_team == teams[0])
//                {
//                    foreach (var site in sites)
//                    {
//                        var odds = getAmerican(site.odds.h2h[1]);
//                        if (tempBestOdds < odds)
//                        {
//                            tempBestOdds = odds;
//                            if (odds > 0)
//                            {
//                                bestAwaySite = site.site_nice;
//                                bestAwayOdds = $"+{odds}";
//                                arrayOfOdds.Add($"+{odds}");
//                            }
//                            else
//                            {
//                                bestAwaySite = site.site_nice;
//                                bestAwayOdds = odds.ToString();
//                                arrayOfOdds.Add(odds.ToString());
//                            }
//                        }
//                    }
//                    //switch (bestAwaySite)
//                    //{
//                    //    case "PointsBet (US)": awaySiteImage = "PointsBet.png"; break;
//                    //    case "William Hill (US)": awaySiteImage = "WilliamHill.png"; break;
//                    //    default: awaySiteImage = $"{bestAwaySite}.png"; break;
//                    //}
//                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("BestAwaySite"));
//                    return arrayOfOdds;
//                }
//                else
//                {
//                    foreach (var site in sites)
//                    {
//                        var odds = getAmerican(site.odds.h2h[0]);
//                        if (tempBestOdds < odds)
//                        {
//                            tempBestOdds = odds;
//                            if (odds > 0)
//                            {
//                                bestAwaySite = site.site_nice;
//                                bestAwayOdds = $"+{odds}";
//                                arrayOfOdds.Add($"+{odds}");
//                            }
//                            else
//                            {
//                                bestAwaySite = site.site_nice;
//                                bestAwayOdds = odds.ToString();
//                                arrayOfOdds.Add(odds.ToString());
//                            }
//                        }
//                    }
//                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("BestAwaySite"));
//                    //switch (bestAwaySite)
//                    //{
//                    //    case "PointsBet (US)": awaySiteImage = "PointsBet.png"; break;
//                    //    case "William Hill (US)": awaySiteImage = "WilliamHill.png"; break;
//                    //    default: awaySiteImage = $"{bestAwaySite}.png"; break;
//                    //}
//                    return arrayOfOdds;
//                }
//            }
//        }

//        public int getAmerican(double decimalOdds)
//        {
//            int americanOdds;
//            if (decimalOdds == 1)
//            {
//                return (int)(-100 / (.01));
//            }
//            if (decimalOdds >= 2.00)
//            {
//                americanOdds = (int)((decimalOdds - 1) * 100);
//            }
//            else
//            {
//                americanOdds = (int)(-100 / (decimalOdds - 1));
//            }

//            return americanOdds;
//        }

//        public string awaySiteImage
//        {
//            get
//            {
//                switch (BestAwaySite)
//                {
//                    case "PointsBet (US)": return "PointsBet.png";
//                    case "William Hill (US)": return "WilliamHill.jpg";
//                    case "LowVig.ag": return "LowVig.png";
//                    case "MyBookie.ag": return "MyBookie.png";
//                    default: return $"{BestAwaySite}.png";
//                }

//            }

//            set
//            {
//                awaySiteImage = value;
//            }

//        }



//        public string homeSiteImage
//        {
//            get
//            {
//                switch (BestHomeSite)
//                {
//                    case "PointsBet (US)": return "PointsBet.png";
//                    case "William Hill (US)": return "WilliamHill.jpg";
//                    case "LowVig.ag": return "LowVig.png";
//                    case "MyBookie.ag": return "MyBookie.png";
//                    default: return $"{BestHomeSite}.png";
//                }
//            }

//            set
//            {
//                awaySiteImage = value;
//            }

//        }

//        private Color matchTimeColor { get; set; }

//        public Color MatchTimeColor
//        {
//            get
//            {
//                return matchTimeColor;
//            }
//            set
//            {
//                try
//                {
//                    if (value != null)
//                    {
//                        matchTimeColor = value;
//                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MatchTimeColor"));
//                    }
//                }
//                catch (Exception ex)
//                {
//                    Console.WriteLine($"crash: {ex.Message}");
//                }
//            }
//        }

//        public string matchTime
//        {
//            get
//            {
//                string matchTimeString = "Loading...";
//                if ((DateTimeOffset.UtcNow.ToUnixTimeSeconds() - commence_time) < 0)
//                {
//                    matchTimeString = DateTimeOffset.FromUnixTimeSeconds(commence_time).ToLocalTime().ToString("M/d h:mm tt");
//                    matchTimeColor = Color.Black;
//                }
//                else
//                {
//                    matchTimeString = "LIVE";
//                    matchTimeColor = Color.Red;
//                }
//                return matchTimeString;
//            }

//        }

//    }

//    public class AllUpcomingMatches
//    {
//        public bool success { get; set; }
//        public ObservableCollection<UpcomingMatches> data { get; set; }
//    }
//}
