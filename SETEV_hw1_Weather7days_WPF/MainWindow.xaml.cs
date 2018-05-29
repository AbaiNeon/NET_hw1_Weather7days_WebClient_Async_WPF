using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;

namespace SETEV_hw1_Weather7days_WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private async void BtnGetWeatherClick(object sender, RoutedEventArgs e)
        {
            string url = GetUrl();

            string json = await GetJson(url);

            if (json == "")
            {
                txtBlockTemp.Text = "incorrect city name";
                return;
            }

            WeatherResponse weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(json);
            string icon = weatherResponse.Weather[0].Icon;
            string imageUrl = @"http://openweathermap.org/img/w/" + icon + @".png";

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(imageUrl, UriKind.RelativeOrAbsolute);
            bitmap.EndInit();
            
            image.Source = bitmap;

            //инфо по погоде
            txtBlockTemp.Text = weatherResponse.Name + "   " + weatherResponse.Main.Temp;
        }

        private string GetUrl()
        {
            string appid = @"3b4a6bd0a17db4d75a5330604caf4279"; //при регистрации дается appid
            string city = txtBoxCity.Text;
            string url = @"http://api.openweathermap.org/data/2.5/weather?q=" + city + @"&units=metric" + @"&APPID=" + appid;
            return url;
        }

        private Task<string> GetJson(string url)
        {
            return Task.Run(() =>
            {
                Thread.CurrentThread.Name = "2 поток";
                try
                {
                    string json;

                    using (WebClient wc = new WebClient())
                    {
                        wc.Encoding = Encoding.UTF8;
                        json = wc.DownloadString(url);
                    }
                    //Thread.Sleep(5000);
                    return json;
                }
                catch (Exception)
                {
                    return "";
                }

            });

        }
    }
}
