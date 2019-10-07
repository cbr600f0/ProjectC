using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectC.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SinglePlayer : ContentPage
    {
        private Boolean CheckWord(String word)
        {
            String baseUrl = $"https://languagetool.org/api/v2/check?text={word}&language=nl";
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = client.GetAsync(baseUrl).Result)
                {
                    using (HttpContent content = response.Content)
                    {
                        String data = content.ReadAsStringAsync().Result;
                        if (data != null)
                        {
                            JContainer test = (JContainer)JObject.Parse(data)["matches"];
                            return test.Count == 0;
                        }
                        return false;
                    }

                }
            }

        }

        public Int32 wordRows;
        public SinglePlayer()
        {
            this.InitializeComponent();
            wordRows = 7;
        }

        private void CheckTest(object sender, EventArgs e)
        {
            label.Text = this.CheckWord(wordEntry.Text).ToString();
        }

        public void UICreater()
        {
            //MiddlePart.Children.Add(new Grid()
            //{
            //    //RowDefinitions
            //});

            //for (int i = 0; i < wordRows; i++)
            //{
            //    MiddlePart.Children.Add(new Label()
            //    {C:\Users\cbr600f0\source\repos\ProjectC\ProjectC\Pages\singleplayer.xaml.cs
            //        Text = "Mark",

            //    });
            //}
        }
    }
}