using System;
using System.Net.Http;
using Xamarin.Forms;
using System.Text.Json;
using QuizGameApp.Pages;
using System.IO;


namespace QuizGameApp
{
    public partial class GamePage : ContentPage 
    {
        public GamePage()
        {
            InitializeComponent();
            LoadQuestion();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private const string OpenTriviaApiUrl = "https://opentdb.com/api.php?amount=50&type=boolean";
        bool Globalanswer;
        int GlobalScore = -1;
        private async void LoadQuestion()
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(OpenTriviaApiUrl);

                if (response.IsSuccessStatusCode)
                {
                    GlobalScore++;
                    string json = await response.Content.ReadAsStringAsync();
                    int index = ParseIndexFromJson(json);
                    string question = ParseQuestionFromJson(json, index);
                    string answer = ParseAnswerFromJson(json, index);
                    Globalanswer = bool.Parse(answer);

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        QuestionLabel.Text = question;
                    });

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        AnswerLabel.Text = answer;
                    });

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        ScoreLabel.Text = GlobalScore.ToString();
                    });

                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }

        private int ParseIndexFromJson(string json) 
        {
            var jsonDocument = JsonDocument.Parse(json);
            var resultsArray = jsonDocument.RootElement.GetProperty("results").EnumerateArray();

            int index = 0;

            foreach (var result in resultsArray)
            {
                var question = result.GetProperty("question").GetString();

                if (!question.Contains("&"))
                {
                    return index;
                }

                index++;
            }
            return -1;
        }

        private string ParseQuestionFromJson(string json, int index)
        {
            try
            {
                var jsonDocument = JsonDocument.Parse(json);
                var question = jsonDocument.RootElement.GetProperty("results")[index].GetProperty("question").GetString();
                return question;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"JSON Parsing Exception: {ex.Message}");
                return "Failed to extract question";
            }
        }

        private string ParseAnswerFromJson(string json, int index)
        {
            try
            {
                var jsonDocument = JsonDocument.Parse(json);
                var answer = jsonDocument.RootElement.GetProperty("results")[index].GetProperty("correct_answer").GetString();
                return answer;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"JSON Parsing Exception: {ex.Message}");
                return "Failed to extract answer";
            }
        }

        private string ParseCategoryFromJson(string json, int index) 
        {
            try
            {
                var jsonDocument = JsonDocument.Parse(json);
                var category = jsonDocument.RootElement.GetProperty("results")[index].GetProperty("category").GetString();
                return category;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"JSON Parsing Exception: {ex.Message}");
                return "Failed to extract category";
            }
        }

        private void OnTrueClicked(object sender, EventArgs e)
        {
            if (Globalanswer) { LoadQuestion(); }
            else
            {
                try
                {
                    string appDataDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                    string filePath = Path.Combine(appDataDirectory, "GlobalScore.txt");

                    if (File.Exists(filePath))
                    {
                        string existingContent = File.ReadAllText(filePath);

                        if (int.TryParse(existingContent, out int existingScore))
                        {
                            if (GlobalScore > existingScore) { File.WriteAllText(filePath, GlobalScore.ToString()); }
                        }
                        else { Console.WriteLine("Содержимое файла не является числом."); }
                    }
                    else { File.WriteAllText(filePath, GlobalScore.ToString()); }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
                Navigation.PushAsync(new LosePage(GlobalScore));
            }
        }

        private void OnFalseClicked(object sender, EventArgs e)
        {
            if (!Globalanswer) { LoadQuestion(); }
            else
            {
                try
                {
                    string appDataDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                    string filePath = Path.Combine(appDataDirectory, "GlobalScore.txt");

                    if (File.Exists(filePath))
                    {
                        string existingContent = File.ReadAllText(filePath);

                        if (int.TryParse(existingContent, out int existingScore))
                        {
                            if (GlobalScore > existingScore) { File.WriteAllText(filePath, GlobalScore.ToString()); }
                        }
                        else { Console.WriteLine("Содержимое файла не является числом."); }
                    }
                    else { File.WriteAllText(filePath, GlobalScore.ToString()); }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
                Navigation.PushAsync(new LosePage(GlobalScore));
            }
        }
    }
}