using ChatAPI.Modelo;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace ChatAPI
{
    public partial class MainPage : ContentPage
    {
        private readonly HttpClient _httpClient;

        public MainPage()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
        }


        private async void Button_Clicked(object sender, EventArgs e)
        {
            var message = new Message
            {
                Content = MessageEntry.Text,
                Timestamp = DateTime.UtcNow
            };

            var json = JsonSerializer.Serialize(message);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("http://localhost:5000/api/chat", content);
            var responseJson = await response.Content.ReadAsStringAsync();

            var responseMessage = JsonSerializer.Deserialize<Message>(responseJson);
            ResponseLabel.Text = responseMessage.Content;
        }
    }

}
