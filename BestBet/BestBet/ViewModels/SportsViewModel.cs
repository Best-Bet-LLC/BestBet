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
            getSports();
            //Task.Run(async () => await
            getUpcomingMatches();
        }

        public SportsViewModel(bool refreshUpcomingMatches)
        {
           
        }

        public async Task<bool> getSports()
        {
            try
            {
                //Console.WriteLine("about to invoke");
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
                Console.WriteLine("about to invoke upcoming matches");
                var result = await _rest.getUpcomingMatches(App.sport, App.region);

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
                //Console.WriteLine($"{tempMatches.Count}");
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
                allMatches = temp_result;
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

                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"crash: {ex.Message}");
                return false;
            }
        }
    }
}
