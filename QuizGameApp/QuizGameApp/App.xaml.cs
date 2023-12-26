using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuizGameApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Устанавливаем главную страницу как NavigationPage с начальной страницей MainPage
            MainPage = new NavigationPage(new MainPage())
            {
                // Устанавливаем HasNavigationBar в false, чтобы скрыть верхнюю панель навигации
                BarBackgroundColor = Color.Transparent,
                BarTextColor = Color.Black
            };
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
