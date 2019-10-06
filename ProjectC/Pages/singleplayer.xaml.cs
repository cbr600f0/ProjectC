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
        public SinglePlayer()
        {
            Boolean test = this.CheckWord("auto");
            Boolean test2 = this.CheckWord("froo");
            this.InitializeComponent();
        }

        private void CheckTest(object sender, EventArgs e)
        {
            label.Text = this.CheckWord(wordEntry.Text).ToString();
        }
    }
}