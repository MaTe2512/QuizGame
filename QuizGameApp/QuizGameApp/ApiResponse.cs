using Newtonsoft.Json;
using QuizGameApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;


class ApiResponse
{
    
    private const string OpenTriviaApiUrl = "https://opentdb.com/api.php?amount=1&type=boolean";
    private async void LoadQuestion()
    {
        try
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(OpenTriviaApiUrl);

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(json);

                // Распарсить JSON и извлечь вопрос



                // Вывести вопрос на страницу
                /*Device.BeginInvokeOnMainThread(() =>
                {
                    QuestionLabel.Text = question;
                });*/
            }
            else
            {
                // Обработка ошибки
                Console.WriteLine($"Error: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            // Обработка исключений
            Console.WriteLine($"Exception: {ex.Message}");
        }
    }
}

