using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectC.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class singleplayer : ContentPage
    {
        public int wordRows = 7;
        public int wordLength = 7;
        public singleplayer()
        {
            InitializeComponent();
            UICreater();
        }

        public void UICreater()
        {
            Grid grid = new Grid();
            StackLayout wordContainer = new StackLayout();
            Grid insideGrid = new Grid();

            for (int i = 0; i < wordRows; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            }
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) });


            for (int row = 0; row < grid.RowDefinitions.Count(); row++)
            {
                wordContainer.HorizontalOptions = LayoutOptions.CenterAndExpand;
                grid.Children.Add(new Label() { Text = "Mark" }, 0, row);

                for (int i = 0; i < wordLength; i++)
                {
                    insideGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                }

                for (int i = 0; i < insideGrid.ColumnDefinitions.Count; i++)
                {
                    insideGrid.Children.Add(new Frame()
                    {
                        Content = new Label()
                        {
                            Text = "a",
                            FontSize = 20,
                            HorizontalOptions = LayoutOptions.CenterAndExpand
                        },
                        Margin = 0,
                        Padding = 0,
                        BorderColor = Color.Black
                    }, i, 0);
                }


                wordContainer.Children.Add(insideGrid);
                grid.Children.Add(wordContainer, 1, row);

                insideGrid = new Grid();
                wordContainer = new StackLayout();
            }

            MiddlePart.Children.Add(grid);
        }

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
    }
}