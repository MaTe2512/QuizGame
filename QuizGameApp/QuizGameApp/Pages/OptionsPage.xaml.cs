using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuizGameApp.Pages
{
    public partial class OptionsPage : ContentPage
    {
        public OptionsPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }
        private async void ClickedOnReset(object sender, EventArgs e)
        {
            try
            {
                string appDataDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                string filePath = Path.Combine(appDataDirectory, "GlobalScore.txt");

                // Записываем значение 0 в файл
                File.WriteAllText(filePath, "0");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        private async void ClickedOnMeinMenuInOptions(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }


    }
}