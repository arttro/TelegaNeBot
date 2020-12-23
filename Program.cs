using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using System.Resources;

namespace weather
{
    class Program
    {
        private static TelegramBotClient client;

        static void Main(string[] args)
        {

            string token = "1318724810:AAEFZish3unm5KBLkwlfepuuYTPFZZkgZFQ";

            client = new TelegramBotClient(token);
            client.OnMessage += BotOnMessageReceived;
            client.OnMessageEdited += BotOnMessageReceived;
            client.StartReceiving();

            Console.ReadLine();

            client.StopReceiving();
        }
        private static async void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message;

            string key = "29f2f4edc2ad34bf18d8a6c2f640dc45";

            try
            {
                
                string url = "http://api.openweathermap.org/data/2.5/weather?q=" + message.Text + "&units=metric&appid=" + key;
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            
                StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream());
                string result = streamReader.ReadToEnd();
                WeatherResponse weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(result);

                if (message?.Type == MessageType.Text)
                {
                    await client.SendTextMessageAsync(message.Chat.Id, "Temperature in " + weatherResponse.Name + ": " + weatherResponse.Main.Temp + "°C \n");
                }
            }
            catch {
                if (message?.Type == MessageType.Text)
                {
                    await client.SendTextMessageAsync(message.Chat.Id, "City not found");
                }
            }

            
        }
        
    }
}
