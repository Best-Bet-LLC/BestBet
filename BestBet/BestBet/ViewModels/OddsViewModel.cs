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

                    await getOdds();

                    IsRefreshing = false;
                });
            }
        }

        private bool _isRefreshing_Single = false;
        public bool IsRefreshing_Single
        {
            get { return _isRefreshing_Single; }
            set
            {
                _isRefreshing_Single = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsRefreshing_Single"));
            }
        }

        public ICommand RefreshCommand_Single
        {
            get
            {
                return new Command(async () =>
                {
                    IsRefreshing_Single = true;

                    await getOdds(selectedMatch);

                    IsRefreshing_Single = false;
                });
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
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedMatch"));
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
            if(singleMatchView == true)
            {

            } else
            {
                getOdds();
            }
            
        }

        public OddsViewModel(Match matchIn)
        {
            selectedMatch = matchIn;
            singleMatchView = true;
        }



        public async Task<bool> getOdds()
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
                allMatches = tempMatches;
                tempMatches.Clear();
                for(int i=0; i<3; i++)
                {
                    allMatches[i].isHot = true;
                    tempMatches.Add(allMatches[i]);
                }
                HotMatches = tempMatches;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AllMatches"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("HotMatches"));
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
                    if(match == matchIn)
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
