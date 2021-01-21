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
    public class OddsViewModel : INotifyPropertyChanged
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

                    if (singleMatchView)
                    {
                        await getOdds(selectedMatch);
                    }
                    else await getOdds();
                    

                    IsRefreshing = false;
                });
            }
        }


        private List<Site> sites;

        private List<Site> Sites
        {
            get
            {
                return sites;
            }
            set
            {
                try
                {
                    if (value != null)
                    {
                        sites = value;
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Sites"));
                    }
                }
                catch (Exception ex)
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
                    if (value != null)
                    {
                        allMatches = value;
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AllMatches"));
                    }
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
                    if (value != null)
                    {
                        hotMatches = value;
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("HotMatches"));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"crash: {ex.Message}");
                }
            }
        }

       

        private Match selectedMatch;

        public Match SelectedMatch
        {
            get
            {
                return selectedMatch;
            }
            set
            {
                try
                {
                    if (value != null)
                    {
                        selectedMatch = value;
                       // PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedMatch"));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"crash: {ex.Message}");
                }
            }
        }

        private bool singleMatchView;

        public bool SingleMatchView
        {
            get
            {
                return singleMatchView;
            }
            set
            {
                try
                {
                    if (value != null)
                    {
                        singleMatchView = value;
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SingleMatchView"));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"crash: {ex.Message}");
                }
            }
        }



        public OddsViewModel()
        {
            
            
        }

        public OddsViewModel(Match matchIn, bool singleMatchViewIn)
        {
            
            singleMatchView = singleMatchViewIn;
            if (singleMatchView)
            {
                selectedMatch = matchIn;
                sites = selectedMatch.sites;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedMatch"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Sites"));
                getOdds(); //for testing purposes, remove for go live
            } else
            {
                getOdds();
            }
        }



        public async Task<bool> getOdds()
        {
            try
            {
                Console.WriteLine("about to invoke");
                ObservableCollection<Match> result = await _rest.getOdds(App.sport, App.region);
                List<Match> tempMatches = new List<Match>();
                allMatches = new List<Match>();
                allMatches.Add(result[0]);
                hotMatches = new List<Match>();
                hotMatches.Add(result[0]);
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
                allMatches.Clear();
                foreach (var match in tempMatches)
                {
                    allMatches.Add(match);
                    //tempMatches.Remove
                }
                hotMatches.Clear();
                for(int i=0; i<3; i++)
                {
                    allMatches[i].isHot = true;
                    hotMatches.Add(allMatches[i]);
                }
                //hotMatches = tempMatches;
               // PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AllMatches"));
              //  PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("HotMatches"));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"crash in get odds: {ex.Message}");
            }

            return true;
        }

        public async Task<bool> getOdds(Match matchIn)
        {
            try
            {
                Console.WriteLine("about to invoke");
                ObservableCollection<Match> result = await _rest.getOdds(App.sport, App.region);
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
                foreach(var match in tempMatches)
                {
                    if ((match.awayTeam == matchIn.awayTeam) && (match.home_team == matchIn.home_team) && (matchIn.commence_time == match.commence_time))
                    {
                        selectedMatch = match;
                    }
                }
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedMatch"));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"crash in get odds with match: {ex.Message}");
            }

            return true;
        }



    }
}
