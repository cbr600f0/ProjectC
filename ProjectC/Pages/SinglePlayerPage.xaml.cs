using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProjectC.Business.Service;
using ProjectC.Model;
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
        public Int32 wordRows = 2;
        //the name speaks for itself. Change this to 15 to create a 15 letter word.
        public Int32 wordLength;
        // This number is used for the "heightRequest" property. Without this, the element will scale down to it's biggest element which is troublesome for frames
        // Through hight "request", the element will choose between 1: the largest available size (what we want) and 2: this number
        public static Int32 unrealHighNumber = 1000000;

        public static Char[] charPool = "AAAAAABBCCDDDDDEEEEEEEEEEEEEEEEEEFFGGGHHIIIIJJKKKLLLMMMNNNNNNNNNNOOOOOOPPQRRRRRSSSSSTTTTTUUUVVWWXYZZ".ToCharArray();
        private static Random random = new Random(DateTime.Now.Millisecond);
        //Creates a grid for the available letters the user can use.
        Grid grid = new Grid() { VerticalOptions = LayoutOptions.CenterAndExpand };
        private bool pushWordDebug = false;
        private Int32 totalPoints = 0;
        private Int32 turn = 10;
        private int remainingShuffles = 3;
        public List<Frame> UsableLetterList = new List<Frame>();
        public string currentUser = "";
        public int difficultyMultiplier = 2;
        public int currentLetterValue;
        public SinglePlayerPage(string difficulty, int difficultyMultiplier)
        public Score score;
        public string currentUser;
        {
            switch (difficulty)
            {
                case "easy":
                    wordLength = 3;
                    break;
                case "medium":
                    wordLength = 5;
                    break;
                case "hard":
                    wordLength = 7;
                    break;
                case "legendary":
                    wordLength = 10;
                    break;
                default:
                    break;
            }
            this.difficultyMultiplier = difficultyMultiplier;
            this.InitializeComponent();
            this.PlayedWordsUICreator();
            this.UsableLettersUICreator();
            this.RandomLetterGenerator();
            this.UIPushBarCreation();

            try
            {
                score = BasePage.ScoreService.GetByUserId(BasePage.CurrentUserId.Value).OrderBy(h => h.Points).FirstOrDefault();
                viewHighscore.Text = score != null ? "HighScore: " + score.Points.ToString() : "HighScore: 0";
            }
            catch { }
            try
            {
                currentUser = BasePage.UserService.Get(BasePage.CurrentUserId.Value).UserName;
                viewCurrentPlayer.Text = currentUser;
            }
            catch { }
        }

        public void PlayedWordsUICreator()
        {
            Grid grid = new Grid() { VerticalOptions = LayoutOptions.FillAndExpand };
            StackLayout wordContainer = new StackLayout() { VerticalOptions = LayoutOptions.CenterAndExpand };
            Grid insideGrid = new Grid() { VerticalOptions = LayoutOptions.CenterAndExpand };

            for (Int32 i = 0; i < wordRows; i++)
            {
                //Defines the amount of rows needed for all the past-created (history) words
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            }
            //Creates 2 spaces, 25% for the playername who played the word, 75% for the word that is played.
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) });

            // Creates the rows where the past-created (history) words are.
            for (Int32 row = 0; row < grid.RowDefinitions.Count(); row++)
            {
                wordContainer.HorizontalOptions = LayoutOptions.CenterAndExpand;
                // Adds the label (name of the person who played the word) to the left of the word
                grid.Children.Add(new Label() { Text = "Word " + (row + 1), HorizontalOptions = LayoutOptions.CenterAndExpand }, 0, row);

                for (Int32 i = 0; i < wordLength; i++)
                {
                    //Creates gridcolumns equal to the amount of letters needed for the word. (1 column equals 1 letter)
                    insideGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                }

                //Adds the letters to create 1 word
                for (Int32 i = 0; i < insideGrid.ColumnDefinitions.Count; i++)
                {
                    StackLayout stacklaout = new StackLayout();
                    stacklaout.Children.Add(new Label()
                    {
                        Text = "A",
                        FontSize = 20,
                        HorizontalOptions = LayoutOptions.CenterAndExpand
                    });
                    stacklaout.Children.Add(new Label()
                    {
                        Text = currentLetterValue.ToString(),
                        FontSize = 13,
                        HorizontalTextAlignment = TextAlignment.Center
                    });

                    //Creates a frame to get borders around those labels
                    insideGrid.Children.Add(new Frame()
                    {
                        //Creates the labels for the history words (1 label is 1 letter)
                        Content = stacklaout,
                        Margin = 0,
                        Padding = 0,
                        BorderColor = Color.Black
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
            for (Int32 i = 0; i < wordLength; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            }
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

            for (Int32 d = 0; d < difficultyMultiplier; d++)
            {
                //Adds the letters to create 1 word
                for (Int32 i = 0; i < (grid.ColumnDefinitions.Count); i++)
                {
                    //Creates a frame to get borders around those labels
                    Frame frame = new Frame()
                    {
                        //Creates the labels for the history words (1 label is 1 letter)
                        Content = new Label()
                        {
                            Text = this.RandomLetterGenerator().ToString(),
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
                    UsableLetterList.Add(frame);
                    frame.GestureRecognizers.Add(tapGestureRecognizer);
                    grid.Children.Add(frame, i, d);
                }
            }

            UsableLetters.Children.Add(grid);

        }

        private async Task<Boolean> CheckWord(string word)
        {
            String baseUrl = $"https://languagetool.org/api/v2/check?text={word}&language=nl";
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.GetAsync(baseUrl))
                {
                    using (HttpContent content = response.Content)
                    {
                        String data = await content.ReadAsStringAsync();
                        if (data != null)
                        {
                            JContainer matches = (JContainer)JObject.Parse(data)["matches"];
                            return matches.Count == 0;
                        }
                        return false;
                    }
                }
            }
        }

        public void DebugAlertTester(Object sender, EventArgs e)
        {
            Frame frame = (Frame)sender;

            foreach (View gridFrame in grid.Children)
            {
                gridFrame.BackgroundColor = Color.Transparent;
            }
            frame.BackgroundColor = Color.Red;
        }

        public void AddLetersToList(Frame frame)

        {
            wordCreationBar.Add(frame);
        }

        public void SwapLetters(Object sender, EventArgs e)
        {
            foreach (View gridFrame in grid.Children)
            {
                if (gridFrame.BackgroundColor == Color.Red)
                {
                    //making sure that the program knows which 2 labels are tapped (on your own board and on the bar you want to create the word)
                    Frame currentFrame = (Frame)gridFrame;
                    Label currentLabel = (Label)currentFrame.Content;

                    Frame wordCreationFrame = (Frame)sender;
                    Label wordCreationLabel = (Label)wordCreationFrame.Content;

                    String placeHolder = wordCreationLabel.Text;
                    wordCreationLabel.Text = currentLabel.Text;
                    currentLabel.Text = placeHolder;

                    viewCurrentPushwordValue.Text = "Dit woord: " + WordPointsCalculator() + " punten";

                    gridFrame.BackgroundColor = Color.Transparent;
                    continue;
                }
            }
        }

        public async void PushCurrentWord()
        {
            string pushingWord = "";
            StackLayout wordContainer = new StackLayout() { VerticalOptions = LayoutOptions.CenterAndExpand };
            foreach (Frame frame in wordCreationBar)
            {
                Label currentLabel = (Label)frame.Content;
                if (currentLabel.Text == "")
                {
                    await this.DisplayAlert("Alert", "Je woord is nog niet af. Vul alle letters in", "OK");
                    return;
                }
                pushingWord += currentLabel.Text;
            }

            if (pushWordDebug)
            {
                if (!await CheckWord(pushingWord))
                {
                    await DisplayAlert("Alert", "Je woord bestaat niet. Probeer een ander woord", "OK");
                    return;
                }
            }

            Grid grid = (Grid)MiddlePart.Content;
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            Grid leftsideGrid = new Grid();
            leftsideGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            leftsideGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.Children.Add(leftsideGrid, 0, grid.RowDefinitions.Count - 1);
            leftsideGrid.Children.Add(new Label()
            {
                Text = "Word " + (grid.RowDefinitions.Count),
                HorizontalOptions = LayoutOptions.CenterAndExpand
            }, 0, 0);

            leftsideGrid.Children.Add(new Label()
            {
                Text = WordPointsCalculator().ToString()
            }, 1, 0);

            Grid insideGrid = new Grid() { VerticalOptions = LayoutOptions.CenterAndExpand };
            for (Int32 i = 0; i < wordLength; i++)
            {
                //Creates gridcolumns equal to the amount of letters needed for the word. (1 column equals 1 letter)
                insideGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            }

            for (Int32 i = 0; i < insideGrid.ColumnDefinitions.Count; i++)
            {
                Label Label = (Label)wordCreationBar[i].Content;
                String currentLetter = Label.Text;
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
            turn--;
            totalPoints += WordPointsCalculator();
            viewPointCounter.Text = "totale score: " + totalPoints;
            viewTurnCounter.Text = "beurten over: " + turn;
            if (turn <= 0)
            {
                GameOverHandler();
            }
            EmptyPushwordBar();
            FillUsableLetterBord();
            viewCurrentPushwordValue.Text = "Dit woord: " + WordPointsCalculator() + " punten";
        }

        private void PushButton_Clicked(object sender, EventArgs e)
        {
            PushCurrentWord();
        }

        public Char RandomLetterGenerator()
        {
            return charPool[random.Next(0, charPool.Length - 1)];
        }

        public void EmptyPushwordBar()
        {
            foreach (Frame frame in wordCreationBar)
            {
                Label label = (Label)frame.Content;
                label.Text = "";
            }
        }

        private void DebugButton_Clicked(object sender, EventArgs e)
        {
            if (pushWordDebug)
            {
                pushWordDebug = false;
                debugButtonPushBar.BackgroundColor = Color.Green;
            }
            else
            {
                pushWordDebug = true;
                debugButtonPushBar.BackgroundColor = Color.Red;
            }
        }

        public void FillUsableLetterBord()
        {
            foreach (Frame frame in UsableLetterList)
            {
                Label label = (Label)frame.Content;
                label.Text = label.Text == "" ? RandomLetterGenerator().ToString() : label.Text;
            }
        }

        private void ShuffleUsableLetterBord(object sender, EventArgs e)
        {
            if (remainingShuffles <= 0)
            {
                return;
            }
            foreach (Frame frame in UsableLetterList)
            {
                Label label = (Label)frame.Content;
                label.Text = RandomLetterGenerator().ToString();
            }
            EmptyPushwordBar();
            remainingShuffles--;
            shuffleCounter.Text = "Shuffles: " + remainingShuffles;
        }

        public int WordPointsCalculator()
        {
            int totalPoints = 0;
            foreach (Frame frame in wordCreationBar)
            {
                Label label = (Label)frame.Content;
                switch (label.Text)
                {
                    case "A":
                        currentLetterValue = 1;
                        totalPoints += currentLetterValue;
                        break;
                    case "B":
                        currentLetterValue = 3;
                        totalPoints += currentLetterValue;
                        break;
                    case "C":
                        currentLetterValue = 5;
                        totalPoints += currentLetterValue;
                        break;
                    case "D":
                        currentLetterValue = 2;
                        totalPoints += currentLetterValue;
                        break;
                    case "E":
                        currentLetterValue = 1;
                        totalPoints += currentLetterValue;
                        break;
                    case "F":
                        currentLetterValue = 4;
                        totalPoints += currentLetterValue;
                        break;
                    case "G":
                        currentLetterValue = 3;
                        totalPoints += currentLetterValue;
                        break;
                    case "H":
                        currentLetterValue = 4;
                        totalPoints += currentLetterValue;
                        break;
                    case "I":
                        currentLetterValue = 4;
                        totalPoints += currentLetterValue;
                        break;
                    case "J":
                        currentLetterValue = 4;
                        totalPoints += currentLetterValue;
                        break;
                    case "K":
                        currentLetterValue = 3;
                        totalPoints += currentLetterValue;
                        break;
                    case "L":
                        currentLetterValue = 3;
                        totalPoints += currentLetterValue;
                        break;
                    case "M":
                        currentLetterValue = 3;
                        totalPoints += currentLetterValue;
                        break;
                    case "N":
                        currentLetterValue = 1;
                        totalPoints += currentLetterValue;
                        break;
                    case "O":
                        currentLetterValue = 1;
                        totalPoints += currentLetterValue;
                        break;
                    case "P":
                        currentLetterValue = 3;
                        totalPoints += currentLetterValue;
                        break;
                    case "Q":
                        currentLetterValue = 10;
                        totalPoints += currentLetterValue;
                        break;
                    case "R":
                        currentLetterValue = 2;
                        totalPoints += currentLetterValue;
                        break;
                    case "S":
                        currentLetterValue = 2;
                        totalPoints += currentLetterValue;
                        break;
                    case "T":
                        currentLetterValue = 2;
                        totalPoints += currentLetterValue;
                        break;
                    case "U":
                        currentLetterValue = 4;
                        totalPoints += currentLetterValue;
                        break;
                    case "V":
                        currentLetterValue = 4;
                        totalPoints += currentLetterValue;
                        break;
                    case "W":
                        currentLetterValue = 5;
                        totalPoints += currentLetterValue;
                        break;
                    case "X":
                        currentLetterValue = 8;
                        totalPoints += currentLetterValue;
                        break;
                    case "Y":
                        currentLetterValue = 8;
                        totalPoints += currentLetterValue;
                        break;
                    case "Z":
                        currentLetterValue = 4;
                        totalPoints += currentLetterValue;
                        break;
                    default:
                        currentLetterValue = 0;
                        totalPoints += currentLetterValue;
                        break;
                }
            }

            return totalPoints;
        }
        //Tijdelijke functie
        private void PushPointsToDatabase(Int32 points)
        {
            //Gebruiker moet ingelogd zijn!!!
            if (!this._currentUserId.HasValue)
            {
                return;
            }
            Score score = new Score(BasePage.CurrentUserId.Value, points, DateTimeOffset.Now);
            BasePage.ScoreService.AddOrUpdate(score);
        }


        protected void UIPushBarCreation()
        {
            Grid grid = new Grid()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            for (Int32 i = 0; i < wordLength; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            }
            for (int i = 0; i < grid.ColumnDefinitions.Count; i++)
            {

                Frame frame = new Frame()
                {
                    Padding = 0,
                    Margin = 0,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    BorderColor = Color.Black,
                    Content = new Label()
                    {
                        Text = "",
                        FontSize = 20,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        HorizontalOptions = LayoutOptions.CenterAndExpand
                    }
                };

                var tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.Tapped += (s, e) => { SwapLetters(s, e); };
                frame.GestureRecognizers.Add(tapGestureRecognizer);
                grid.Children.Add(frame, i, 0);
                AddLetersToList(frame);
                wordCreationBarStackLayout.Children.Add(grid);
            }
        }
        public async void GameOverHandler()
        {
            if (this._currentUserId.HasValue)
            {   
            BasePage.ScoreService.AddOrUpdate(new Score(BasePage.CurrentUserId.Value, totalPoints, DateTimeOffset.Now));
            }
            await Navigation.PushAsync(new MainPage());
        }

        private async void BackButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}