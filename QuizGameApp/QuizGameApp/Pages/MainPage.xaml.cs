using QuizGameApp.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace QuizGameApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);

            ReadAndDisplayFileContent();

        }

        private void ReadAndDisplayFileContent()
        {
            try
            {
                string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "GlobalScore.txt");

                if (File.Exists(filePath))
                {
                    string fileContent = File.ReadAllText(filePath);

                    ScoreLabel.Text = fileContent;
                }
                else { ScoreLabel.Text = "Файл не найден."; }
            }
            catch (Exception ex) { ScoreLabel.Text = $"Ошибка: {ex.Message}"; }
        }

        private async void OnImageTappedStart(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GamePage());
        }

        private async void OnImageTappedOptions(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new OptionsPage());
        }
    }
}
