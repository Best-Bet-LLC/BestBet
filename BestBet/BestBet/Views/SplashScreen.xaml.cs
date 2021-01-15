using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BestBet.Views
{
    public partial class SplashScreen : ContentPage
    {
        public SplashScreen()
        {
            InitializeComponent();
            launchMainPage();
        }

        private async void launchMainPage()
        {
           // await Task.Delay(20000);
            var t = Task.Run(() =>
            {
                // Do some work on a background thread, allowing the UI to remain responsive
                Xamarin.Forms.Device.BeginInvokeOnMainThread(async () => {
                    MainPage main = new MainPage();
                    await Task.Delay(3000);
                    _ = Navigation.PushAsync(main);
                });
            });
           
        }
    }
}
