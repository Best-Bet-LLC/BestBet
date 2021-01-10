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


        public OddsViewModel()
        {
            getOdds();
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
                    tempMatches.Add(allMatches[i]);
                }
                HotMatches = tempMatches;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AllMatches"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("HotMatches"));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"crash: {ex.Message}");
            }

            return true;
        }

        

    }
}
