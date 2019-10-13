﻿using Newtonsoft.Json;
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
    public partial class SinglePlayerPage : ContentPage
    {
        public List<Frame> wordCreationBar = new List<Frame>();
        //Amount of words already made (change this number to a large number (example: 30) to see the scroll function.)
        //Don't raise this number to high. It'll take a long time to create all the elements (100 word rows might take over 15 seconds to create)
        public int wordRows = 4;
        //the name speaks for itself. Change this to 15 to create a 15 letter word.
        public int wordLength = 7;
        // This number is used for the "heightRequest" property. Without this, the element will scale down to it's biggest element which is troublesome for frames
        // Through hight "request", the element will choose between 1: the largest available size (what we want) and 2: this number
        public static int unrealHighNumber = 1000000;

        public static char[] charPool = "AAAAAABBCCDDDDDEEEEEEEEEEEEEEEEEEFFGGGHHIIIIJJKKKLLLMMMNNNNNNNNNNOOOOOOPPQRRRRRSSSSSTTTTTUUUVVWWXYZZ".ToCharArray();
        private static Random random = new Random(DateTime.Now.Millisecond);
        //Creates a grid for the available letters the user can use.
        Grid grid = new Grid() { VerticalOptions = LayoutOptions.CenterAndExpand };

        public SinglePlayerPage()
        {
            InitializeComponent();
            AddLetersToList();
            PlayedWordsUICreator();
            UsableLettersUICreator();
            RandomLetterGenerator();
        }

        public void PlayedWordsUICreator()
        {
            Grid grid = new Grid() { VerticalOptions = LayoutOptions.FillAndExpand };
            StackLayout wordContainer = new StackLayout() { VerticalOptions = LayoutOptions.CenterAndExpand };
            Grid insideGrid = new Grid() { VerticalOptions = LayoutOptions.CenterAndExpand };

            for (int i = 0; i < wordRows; i++)
            {
                //Defines the amount of rows needed for all the past-created (history) words
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star)});
            }
            //Creates 2 spaces, 25% for the playername who played the word, 75% for the word that is played.
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) });

            // Creates the rows where the past-created (history) words are.
            for (int row = 0; row < grid.RowDefinitions.Count(); row++)
            {
                wordContainer.HorizontalOptions = LayoutOptions.CenterAndExpand;
                // Adds the label (name of the person who played the word) to the left of the word
                grid.Children.Add(new Label() { Text = "Word " + (row + 1), HorizontalOptions = LayoutOptions.CenterAndExpand}, 0, row);
                
                for (int i = 0; i < wordLength; i++)
                {
                    //Creates gridcolumns equal to the amount of letters needed for the word. (1 column equals 1 letter)
                    insideGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                }

                //Adds the letters to create 1 word
                for (int i = 0; i < insideGrid.ColumnDefinitions.Count; i++)
                {
                    //Creates a frame to get borders around those labels
                    insideGrid.Children.Add(new Frame()
                    {
                        //Creates the labels for the history words (1 label is 1 letter)
                        Content = new Label()
                        {
                            Text = "A",
                            FontSize = 20,
                            HorizontalOptions = LayoutOptions.CenterAndExpand
                        },
                        Margin = 0,
                        Padding = 0,
                        BorderColor = Color.Black,

                    }, i, 0);
                }

                //Adds the whole word (defined in the insideGrid) to the stacklayout
                wordContainer.Children.Add(insideGrid);
                //Adds the stacklayout with the whole word in it to the right side of the row (so the player name and the word are next to each other)
                grid.Children.Add(wordContainer, 1, row);

                //The playername and the made word are passed to the grid-container
                //So now we can make the stacklayout and insideGrid empty so they can be refilled again with the next row
                wordContainer = new StackLayout() { VerticalOptions = LayoutOptions.CenterAndExpand };
                insideGrid = new Grid() { VerticalOptions = LayoutOptions.CenterAndExpand };
            }
            //This is the element on the front end. This will be the container (equal to a div) that holds all the history words
            MiddlePart.Content = grid;
        }

        public void UsableLettersUICreator()
        {
            for (int i = 0; i < wordLength; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            }
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

            for (int d = 0; d < 2; d++)
            {
                //Adds the letters to create 1 word
                for (int i = 0; i < (grid.ColumnDefinitions.Count); i++)
                {
                    //Creates a frame to get borders around those labels
                    Frame frame = new Frame()
                    {
                        //Creates the labels for the history words (1 label is 1 letter)
                        Content = new Label()
                        {
                            Text = RandomLetterGenerator().ToString(),
                            FontSize = 20,
                            HorizontalOptions = LayoutOptions.CenterAndExpand,
                            VerticalOptions = LayoutOptions.CenterAndExpand
                        },
                        BorderColor = Color.Black,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        Margin = 0,
                        Padding = 0,
                        HeightRequest = unrealHighNumber
                    };

                    //These 3 lines adds the on click event to the frame (can be beautified)
                    var tapGestureRecognizer = new TapGestureRecognizer();
                    tapGestureRecognizer.Tapped += (s, e) => { DebugAlertTester(s, e); };
                    frame.GestureRecognizers.Add(tapGestureRecognizer);


                    grid.Children.Add(frame, i, d);
                }
            }

            UsableLetters.Children.Add(grid);

        }

        private async Task<bool> CheckWord(string word)
        {
            var baseUrl = $"https://languagetool.org/api/v2/check?text={word}&language=nl";
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.GetAsync(baseUrl))
                {
                    using (HttpContent content = response.Content)
                    {
                        var data = await content.ReadAsStringAsync();
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

        public void DebugAlertTester(object sender, EventArgs e)
        {
            var frame = (Frame)sender;

            foreach (var gridFrame in grid.Children)
            {
                gridFrame.BackgroundColor = Color.Transparent;
            }
            frame.BackgroundColor = Color.Red;
        }

        public void AddLetersToList()
        {
            wordCreationBar.Add(LetterOne);
            wordCreationBar.Add(LetterTwo);
            wordCreationBar.Add(LetterThree);
            wordCreationBar.Add(LetterFour);
            wordCreationBar.Add(LetterFive);
            wordCreationBar.Add(LetterSix);
            wordCreationBar.Add(LetterSeven);
        }

        public void SwapLetters(object sender, EventArgs e)
        {
            foreach (var gridFrame in grid.Children)
            {
                if (gridFrame.BackgroundColor == Color.Red)
                {
                    //making sure that the program knows which 2 labels are tapped (on your own board and on the bar you want to create the word)
                    Frame currentFrame = (Frame)gridFrame;
                    Label currentLabel = (Label)currentFrame.Content;

                    Frame wordCreationFrame = (Frame)sender;
                    Label wordCreationLabel = (Label)wordCreationFrame.Content;

                    string placeHolder = wordCreationLabel.Text;
                    wordCreationLabel.Text = currentLabel.Text;
                    currentLabel.Text = placeHolder;

                    gridFrame.BackgroundColor = Color.Transparent;
                    continue;
                }
            }
        }

        public async void PushCurrentWord()
        {
            string pushingWord = "";
            StackLayout wordContainer = new StackLayout() { VerticalOptions = LayoutOptions.CenterAndExpand };
            foreach (var frame in wordCreationBar)
            {
                var currentLabel = (Label)frame.Content;
                if (currentLabel.Text == "")
                {
                    await DisplayAlert("Alert", "Je woord is nog niet af. Vul alle letters in", "OK");
                    return;
                }
                pushingWord += currentLabel.Text;
            }

            if (!await CheckWord(pushingWord))
            {
                await DisplayAlert("Alert", "Je woord bestaat niet. Probeer een ander woord", "OK");
                return;
            }

            Grid grid = (Grid)MiddlePart.Content;
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.Children.Add(new Label() {
                Text = "Word " + (grid.RowDefinitions.Count),
                HorizontalOptions = LayoutOptions.CenterAndExpand
            }, 0, grid.RowDefinitions.Count - 1);

            Grid insideGrid = new Grid() { VerticalOptions = LayoutOptions.CenterAndExpand };
            for (int i = 0; i < wordLength; i++)
            {
                //Creates gridcolumns equal to the amount of letters needed for the word. (1 column equals 1 letter)
                insideGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            }

            for (int i = 0; i < insideGrid.ColumnDefinitions.Count; i++)
            {
                Label Label = (Label)wordCreationBar[i].Content;
                string currentLetter = Label.Text;
                //Creates a frame to get borders around those labels
                insideGrid.Children.Add(new Frame()
                {
                    //Creates the labels for the history words (1 label is 1 letter)
                    Content = new Label()
                    {
                        Text = currentLetter,
                        FontSize = 20,
                        HorizontalOptions = LayoutOptions.CenterAndExpand
                    },
                    Margin = 0,
                    Padding = 0,
                    BorderColor = Color.Black,

                }, i, 0);
            }
            wordContainer.Children.Add(insideGrid);
            grid.Children.Add(wordContainer, 1, grid.RowDefinitions.Count - 1);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            PushCurrentWord();
        }

        public char RandomLetterGenerator()
        {
            return charPool[random.Next(0, charPool.Length - 1)];
        }
    }
}   