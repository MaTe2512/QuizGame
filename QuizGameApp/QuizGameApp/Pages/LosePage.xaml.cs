using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuizGameApp.Pages
{
    public partial class LosePage : ContentPage
    {
        public LosePage(int score)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            int GlobalScore = score;

            ScoreLabel.Text = GlobalScore.ToString();
        }

        private async void ClickedOnTryAgain(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GamePage());
        }

        private async void ClickedOnMainMenu(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }
    }
}