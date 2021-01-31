using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using BestBet.Services;
using BestBet.Models;
using System.Windows.Input;
using System.Threading.Tasks;
using BestBet.Views;

namespace BestBet.ViewModels
{
    public class SportsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        OddsAPIInterface _rest = DependencyService.Get<OddsAPIInterface>();
        

        private bool _isRefreshing = false;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsRefreshing"));
            }
        }

        public ICommand RefreshCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IsRefreshing = true;

                    await getUpcomingMatches();

                    IsRefreshing = false;
                });
            }
        }

        private ObservableCollection<Sport> allSports;

        public ObservableCollection<Sport> AllSports
        {
            get
            {
                return allSports;
            }
            set
            {
                try
                {
                    allSports = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AllSports"));
                } catch (Exception ex)
                {
                    Console.WriteLine($"crash: {ex.Message}");
                }
            }
        }

        private List<Match> allMatches;

        public List<Match> AllMatches
        {
            get
            {
                return allMatches;
            }
            set
            {
                try
                {
                    allMatches = value;
                    //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AllMatches"));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"crash: {ex.Message}");
                }
            }
        }

        private List<Match> filteredMatches;

        public List<Match> FilteredMatches
        {
            get
            {
                return filteredMatches;
            }
            set
            {
                try
                {
                    filteredMatches = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FilteredMatches"));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"crash: {ex.Message}");
                }
            }
        }

        private List<Match> hotMatches;

        public List<Match> HotMatches
        {
            get
            {
                return hotMatches;
            }
            set
            {
                try
                {
                    hotMatches = value;
                    //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("HotMatches"));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"crash: {ex.Message}");
                }
            }
        }

        public SportsViewModel()
        {
            //var t = Task.Run(() =>
            //{
            //    // Do some work on a background thread, allowing the UI to remain responsive
            //    Xamarin.Forms.Device.BeginInvokeOnMainThread(async () => {
            //        _ = await getSports();

            //        _ = await getUpcomingMatches();
            //    });
            //});
            InitCommands();
            getSports().Wait();
            _ = getUpcomingMatches();
        }

        public SportsViewModel(bool refreshUpcomingMatches)
        {
           
        }

        public async Task<bool> getSports()
        {
            try
            {
                Console.WriteLine("about to invoke");
                var result = await _rest.getSports();
                ObservableCollection<Sport> temp_result = new ObservableCollection<Sport>();
                foreach (Sport sport in result)
                {
                    switch (sport.key)
                    {
                        case "americanfootball_ncaaf":
                            sport.image_key = "americanfootball_ncaaf.png";
                            temp_result.Add(sport);
                            break;
                        case "americanfootball_nfl":
                            sport.image_key = "americanfootball_nfl.png";
                            temp_result.Add(sport);
                            break;
                        case "basketball_nba":
                            sport.image_key = "basketball_nba.png";
                            temp_result.Add(sport);
                            break;
                        case "basketball_ncaab":
                            sport.image_key = "basketball_ncaab.png";
                            temp_result.Add(sport);
                            break;
                        case "icehockey_nhl":
                            sport.image_key = "icehockey_nhl.png";
                            temp_result.Add(sport);
                            break;
                        case "baseball_mlb":
                            sport.image_key = "baseball_mlb.png";
                            temp_result.Add(sport);
                            break;
                        case "soccer_turkey_super_league":
                            sport.image_key = "soccer_turkey_super_league.png";
                            temp_result.Add(sport);
                            break;
                        case "soccer_spain_segunda_division":
                            sport.image_key = "soccer_spain_segunda_division.png";
                            temp_result.Add(sport);
                            break;
                        case "soccer_spain_la_liga":
                            sport.image_key = "soccer_spain_la_liga.png";
                            temp_result.Add(sport);
                            break;
                        case "soccer_germany_bundesliga2":
                            sport.image_key = "soccer_germany_bundesliga2.png";
                            temp_result.Add(sport);
                            break;
                        case "soccer_france_ligue_two":
                            sport.image_key = "soccer_france_ligue_two.png";
                            temp_result.Add(sport);
                            break;
                        case "soccer_belgium_first_div":
                            sport.image_key = "soccer_belgium_first_div.png";
                            temp_result.Add(sport);
                            break;
                        case "soccer_italy_serie_a":
                            sport.image_key = "soccer_italy_serie_a.png";
                            temp_result.Add(sport);
                            break;
                        case "aussierules_afl":
                            sport.image_key = "aussierules_afl.png";
                            temp_result.Add(sport);
                            break;
                        case "basketball_euroleague":
                            sport.image_key = "basketball_euroleague.png";
                            temp_result.Add(sport);
                            break;
                        case "cricket_big_bash":
                            sport.image_key = "cricket_big_bash.png";
                            temp_result.Add(sport);
                            break;
                        default:
                            sport.image_key = "logo_transparent.png";
                            //temp_result.Add(sport);
                            break;
                    }
                }

                allSports = temp_result;
                return true;
                
                //Console.WriteLine("invoked api");
             
            } catch (Exception ex)
            {
                Console.WriteLine($"crash: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> getUpcomingMatches()
        {
            try
            {
                allMatches = new List<Match>();
                hotMatches = new List<Match>();
                filteredMatches = new List<Match>();
                //hotMatches.Add(new Match());
                //allMatches.Add(new Match());
                Console.WriteLine("about to invoke upcoming matches");
                var result = await _rest.getUpcomingMatches(App.sport, App.region);
                hotMatches.Add(result[0]);
                allMatches.Add(result[0]);
                filteredMatches.Add(result[0]);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AllMatches"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("HotMatches"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FilteredMatches"));
                List<Match> tempMatches = new List<Match>();
                foreach (var match in result)
                {
                    tempMatches.Add(match);
                }

                foreach (var match in tempMatches)
                {
                    List<Book> currentBooks = await App.Database.GetSelectedBooksAsync();
                    List<string> bookNames = new List<string>();

                    foreach (var book in currentBooks)
                    {
                        bookNames.Add(book.name);
                    }

                    match.sites.RemoveAll(site => !(bookNames.Contains(site.site_nice)));

                }
                Console.WriteLine($"{tempMatches.Count}");
                List<Match> temp_result = new List<Match>();
                var counter = 0;
                foreach (var match in tempMatches)
                {
                    foreach(var Sport in allSports)
                    {
                        if (Sport.key == match.sport_key)
                        {
                            temp_result.Add(match);
                            counter++;
                        }

                 
                    }
                    if (counter == 1)
                    {
                        counter = 0;
                    } else
                    {
                        temp_result.Add(match);
                    }
                    
                    
                    //switch (match.sport_key)
                    //{

                    //    case "americanfootball_ncaaf":
                    //        temp_result.Add(match);
                    //        break;
                    //    case "americanfootball_nfl":
                    //        temp_result.Add(match);
                    //        break;
                    //    case "basketball_nba":
                    //        temp_result.Add(match);
                    //        break;
                    //    case "basketball_ncaab":
                    //        temp_result.Add(match);
                    //        break;
                    //    case "icehockey_nhl":
                    //        temp_result.Add(match);
                    //        break;
                    //    case "baseball_mlb":
                    //        temp_result.Add(match);
                    //        break;
                    //    default:
                    //        break;
                    //}
                }
                //loop through and assign best site here
                
                
                foreach (Match match in temp_result)
                {
                    assignMatchTime(match);
                    
                    if (match.home_team == match.teams[0])
                    {
                        var BestHomeSite = assignBestHomeBet(match.sites, 0);
                        var BestAwaySite = assignBestAwayBet(match.sites, 1);

                        if (BestHomeSite.odds.h2h[0] > 0)
                        {
                            match.bestHomeOdds = $"+{BestHomeSite.odds.h2h[0]}";
                            match.BestHomeSite = BestHomeSite.site_nice;
                            //match.Add($"+{odds}");
                        }
                        else
                        {
                            match.bestHomeOdds = BestHomeSite.odds.h2h[0].ToString();
                            match.BestHomeSite = BestHomeSite.site_nice;
                            // arrayOfOdds.Add(odds.ToString());
                        }
                        if (BestAwaySite.odds.h2h[0] > 0)
                        {
                            match.bestAwayOdds = $"+{BestAwaySite.odds.h2h[1]}";
                            match.BestAwaySite = BestAwaySite.site_nice;
                            //match.Add($"+{odds}");
                        }
                        else
                        {
                            match.bestAwayOdds = BestAwaySite.odds.h2h[1].ToString();
                            match.BestAwaySite = BestAwaySite.site_nice;
                            // arrayOfOdds.Add(odds.ToString());
                        }
                    } else
                    {
                        var BestHomeSite = assignBestHomeBet(match.sites, 1);
                        var BestAwaySite = assignBestAwayBet(match.sites, 0);

                        if (BestHomeSite.odds.h2h[1] > 0)
                        {
                            match.bestHomeOdds = $"+{BestHomeSite.odds.h2h[1]}";
                            match.BestHomeSite = BestHomeSite.site_nice;
                            //match.Add($"+{odds}");
                        }
                        else
                        {
                            match.bestHomeOdds = BestHomeSite.odds.h2h[1].ToString();
                            match.BestHomeSite = BestHomeSite.site_nice;
                            // arrayOfOdds.Add(odds.ToString());
                        }
                        if (BestAwaySite.odds.h2h[0] > 0)
                        {
                            match.bestAwayOdds = $"+{BestAwaySite.odds.h2h[0]}";
                            match.BestAwaySite = BestAwaySite.site_nice;
                            //match.Add($"+{odds}");
                        }
                        else
                        {
                            match.bestAwayOdds = BestAwaySite.odds.h2h[0].ToString();
                            match.BestAwaySite = BestAwaySite.site_nice;
                            // arrayOfOdds.Add(odds.ToString());
                        }
                    }
                }
                allMatches = temp_result;
                filteredMatches = allMatches;
                tempMatches.Clear();
                for (int i = 0; i < 3; i++)
                {
                    if (i == 1)
                    {
                        allMatches[i].isHot = true;
                        allMatches[i].isNotHot = false;
                    }
                    else
                    {
                        allMatches[i].isHot = false;
                        allMatches[i].isNotHot = true;
                    }
                    tempMatches.Add(allMatches[i]);
                }
                HotMatches = tempMatches;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AllMatches"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("HotMatches"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FilteredMatches"));
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"crash: {ex.Message}");
                return false;
            }
        }

        private void assignMatchTime(Match match)
        {
            match.MatchTime = "Loading...";
            if ((DateTimeOffset.UtcNow.ToUnixTimeSeconds() - match.commence_time) < 0)
            {
                match.MatchTime = DateTimeOffset.FromUnixTimeSeconds(match.commence_time).ToLocalTime().ToString("M/d h:mm tt");
                match.IsLive = Color.Transparent;
                match.isNotLive = true;
                match.isMatchLive = false;
                //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsLive"));
                match.MatchTimeColor = Color.Black;
            }
            else
            {
                match.MatchTime = "LIVE";
                match.IsLive = Color.Red;
                match.isNotLive = false;
                match.isMatchLive = true;
                // PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsLive"));
                match.MatchTimeColor = Color.White;
            }
        }

        private Site assignBestAwayBet(List<Site> sites, int index)
        {
            var tempBestOdds = -1000000;
            List<string> arrayOfOdds = new List<string>();
            Site bestSite = new Site();

            foreach (var site in sites)
            {
                if(site.odds.h2h == null)
                {
                    //do nothing
                } else
                {
                    var odds = getAmerican(site.odds.h2h[index]);
                    site.odds.IsBestH2HAwayBet = false;
                    if (tempBestOdds < odds)
                    {
                        tempBestOdds = odds;
                        bestSite = site;
                    }
                }
                
            }
            bestSite.odds.IsBestH2HAwayBet = true;
            return bestSite;
        }

        private Site assignBestHomeBet(List<Site> sites, int index)
        {
            var tempBestOdds = -1000000;
            List<string> arrayOfOdds = new List<string>();
            Site bestSite = new Site();

            foreach (var site in sites)
            {
                if (site.odds.h2h == null)
                {
                    //do nothing
                }
                else
                {
                    var odds = getAmerican(site.odds.h2h[index]);
                    site.odds.IsBestH2HHomeBet = false;
                    if (tempBestOdds < odds)
                    {
                        tempBestOdds = odds;
                        bestSite = site;
                    }
                }
            }
            bestSite.odds.IsBestH2HHomeBet = true;
            return bestSite;
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


        public ICommand TapCommand { get; private set; }

        public ICommand SportTapCommand { get; private set; }

        private void InitCommands()
        {
            TapCommand = new Command<Match>(
                (match) => {
                    Console.WriteLine($"Match Tapped {match.awayTeam} @ {match.home_team} tapped!");
                    MatchesPage matchesPage = new MatchesPage(match);
                    Application.Current.MainPage.Navigation.PushAsync(matchesPage);
                    });
            SportTapCommand = new Command<Sport>(sport =>
            {
                Console.WriteLine("sport tap command fired");
                filteredMatches.Clear();
                foreach(var match in allMatches)
                {
                    if(match.sport_key == sport.key)
                    {
                        filteredMatches.Add(match);
                    }
                }

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FilteredMatches"));
            });

        }

    }
}
