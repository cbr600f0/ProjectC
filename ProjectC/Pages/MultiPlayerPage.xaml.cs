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
    public partial class MultiPlayerPage : ContentPage
    {
        public int currentPlayerTurn = 1;
        public Color player1Color = Color.DeepSkyBlue;
        public Color player2Color = Color.Yellow;
        public Color currentPlayerColor;
        public List<Frame> wordCreationBar = new List<Frame>();
        public GetValues getValues = new GetValues();
        //Creates a grid for the available letters the user can use.
        Grid grid = new Grid() { VerticalOptions = LayoutOptions.CenterAndExpand };
        private bool pushWordDebug = false;
        private int remainingShufflesP1 = 3;
        private int remainingShufflesP2 = 3;
        public List<Frame> UsableLetterList = new List<Frame>();
        public int difficultyMultiplier = 2;
        public DifficultyEnum difficultyEnum;
        public int currentLetterValue;
        public Score score;
        string difficultySelected;
        string highscoreWord = "";
        int highscoreWordPoints = 0;
        public int totalPointsP1 = 0;
        public int totalPointsP2 = 0;
        public int turn = 1;
        public MultiPlayerPage(string difficulty, int difficultyMultiplier)
        {
            currentPlayerColor = player1Color;
            difficultySelected = difficulty;
            switch (difficulty)
            {
                case "easy":
                    ConfigFile.wordLength = 3;
                    difficultyEnum = DifficultyEnum.Easy;
                    break;
                case "medium":
                    ConfigFile.wordLength = 5;
                    difficultyEnum = DifficultyEnum.Medium;
                    break;
                case "hard":
                    ConfigFile.wordLength = 7;
                    difficultyEnum = DifficultyEnum.Hard;
                    break;
                case "legendary":
                    ConfigFile.wordLength = 10;
                    difficultyEnum = DifficultyEnum.Legendary;
                    break;
                default:
                    break;
            }
            this.difficultyMultiplier = difficultyMultiplier;
            this.InitializeComponent();
            this.PlayedWordsUICreator();
            this.UsableLettersUICreator();
            this.UIPushBarCreation();
        }

        public void PlayedWordsUICreator()
        {
            Grid grid = new Grid() { VerticalOptions = LayoutOptions.FillAndExpand };
            StackLayout wordContainer = new StackLayout() { VerticalOptions = LayoutOptions.CenterAndExpand };
            Grid insideGrid = new Grid() { VerticalOptions = LayoutOptions.CenterAndExpand };

            for (Int32 i = 0; i < ConfigFile.wordRows; i++)
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
                grid.Children.Add(new Label() { Text = "speler " + currentPlayerTurn, HorizontalOptions = LayoutOptions.CenterAndExpand }, 0, row);

                for (Int32 i = 0; i < ConfigFile.wordLength; i++)
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
            for (Int32 i = 0; i < ConfigFile.wordLength; i++)
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
                    Grid gridForLabels = new Grid();
                    gridForLabels.RowDefinitions.Add(new RowDefinition { Height = new GridLength(3, GridUnitType.Star) });
                    gridForLabels.RowDefinitions.Add(new RowDefinition { Height = new GridLength(2, GridUnitType.Star) });
                    gridForLabels.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) });
                    gridForLabels.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) });
                    gridForLabels.ColumnSpacing = 0;
                    gridForLabels.RowSpacing = 0;
                    gridForLabels.Children.Add(new Label()
                    {
                        Text = RandomLetterGenerator().ToString(),
                        FontSize = 20,
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        VerticalOptions = LayoutOptions.CenterAndExpand
                    }, 0, 0);

                    gridForLabels.Children.Add(new Label()
                    {
                        Text = currentLetterValue.ToString(),
                        FontSize = 12,
                        Margin = 0
                    }, 1, 1);
                    //Creates a frame to get borders around those labels
                    Frame frame = new Frame()
                    {
                        BorderColor = Color.Black,
                        BackgroundColor = currentPlayerColor,
                        Content = gridForLabels,
                        HeightRequest = ConfigFile.unrealHighNumber,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        Padding = 0,
                        Margin = 0
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
                gridFrame.BackgroundColor = currentPlayerColor;
            }
            frame.BackgroundColor = Color.Red;
            // Toetsenbord Geluid
            var player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
            player.Load("Click.wav");
            player.Play();
        }

        public void AddLetersToList(Frame frame)
        {
            wordCreationBar.Add(frame);
        }

        public void SwapLetters(Object sender, EventArgs e)
        {
            Frame wordCreationFrame = (Frame)sender;
            Grid gridTwo = (Grid)wordCreationFrame.Content;
            Label wordCreationLabel = (Label)gridTwo.Children[0];
            Label pointsLabel = (Label)gridTwo.Children[1];

            foreach (View gridFrame in grid.Children)
            {
                if (gridFrame.BackgroundColor == Color.Red)
                {
                    //making sure that the program knows which 2 labels are tapped (on your own board and on the bar you want to create the word)
                    Frame currentFrame = (Frame)gridFrame;
                    Grid gridOne = (Grid)currentFrame.Content;
                    Label currentLabel = (Label)gridOne.Children[0];
                    Label currentPointsLabel = (Label)gridOne.Children[1];

                    String placeHolder = wordCreationLabel.Text;
                    wordCreationLabel.Text = currentLabel.Text;
                    currentLabel.Text = placeHolder;

                    placeHolder = pointsLabel.Text;
                    pointsLabel.Text = currentPointsLabel.Text;
                    currentPointsLabel.Text = placeHolder;

                    viewCurrentPushwordValueLabel.Text = "woordwaarde: " + getValues.WordWorth(FramesToWords.WordCreator(wordCreationBar));

                    gridFrame.BackgroundColor = currentPlayerColor;
                    // Pushbar Geluid
                    var player2 = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
                    player2.Load("Pop.wav");
                    player2.Play();
                    return;
                }
            }

            if (wordCreationLabel.Text != "")
            {
                foreach (View gridFrame in grid.Children)
                {//making sure that the program knows which 2 labels are tapped (on your own board and on the bar you want to create the word)
                    Frame currentFrame = (Frame)gridFrame;
                    Grid gridOne = (Grid)currentFrame.Content;
                    Label currentLabel = (Label)gridOne.Children[0];
                    Label currentPointsLabel = (Label)gridOne.Children[1];
                    if (currentLabel.Text == "")
                    {
                        currentLabel.Text = wordCreationLabel.Text;
                        currentPointsLabel.Text = pointsLabel.Text;
                        wordCreationLabel.Text = "";
                        pointsLabel.Text = "";
                    }
                }
                // Pushbar Geluid
                var player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
                player.Load("Pop.wav");
                player.Play();
            }
        }

        public async void PushCurrentWord()
        {
            string pushingWord = "";
            StackLayout wordContainer = new StackLayout() { VerticalOptions = LayoutOptions.CenterAndExpand };
            foreach (Frame frame in wordCreationBar)
            {
                Grid frameGrid = (Grid)frame.Content;
                Label currentLabel = (Label)frameGrid.Children[0];
                if (currentLabel.Text == "")
                {
                    // Geluid voor als je woord niet compleet is
                    var player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
                    player.Load("Incorrect.mp3");
                    player.Play();

                    await this.DisplayAlert("Alert", "Je woord is nog niet af. Vul alle letters in", "OK");
                    return;
                }
                pushingWord += currentLabel.Text;
            }

            if (pushWordDebug)
            {
                if (!await CheckWord(pushingWord))
                {
                    // Geluid voor fout antwoord
                    var player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
                    player.Load("Incorrect.mp3");
                    player.Play();

                    await DisplayAlert("Alert", "Je woord bestaat niet. Probeer een ander woord", "OK");
                    return;
                }
            }

            Grid grid = (Grid)MiddlePart.Content;
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            Grid leftsideGrid = new Grid();
            leftsideGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) });
            leftsideGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.Children.Add(leftsideGrid, 0, grid.RowDefinitions.Count - 1);
            leftsideGrid.Children.Add(new Label()
            {
                Text = "speler " + currentPlayerTurn,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            }, 0, 0);

            leftsideGrid.Children.Add(new Label()
            {
                Text = getValues.WordWorth(FramesToWords.WordCreator(wordCreationBar)).ToString()
            }, 1, 0);

            Grid insideGrid = new Grid() { VerticalOptions = LayoutOptions.CenterAndExpand};
            for (Int32 i = 0; i < ConfigFile.wordLength; i++)
            {
                //Creates gridcolumns equal to the amount of letters needed for the word. (1 column equals 1 letter)
                insideGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            }

            for (Int32 i = 0; i < insideGrid.ColumnDefinitions.Count; i++)
            {
                Frame frame = wordCreationBar[i];
                Grid frameGrid = (Grid)frame.Content;
                Label label = (Label)frameGrid.Children[0];
                string currentLetter = label.Text.ToString();
                List<Frame> localFrameList = new List<Frame>() { wordCreationBar[i] };

                getValues.WordWorth(FramesToWords.WordCreator(localFrameList));

                Grid gridForLabels = new Grid();
                gridForLabels.RowDefinitions.Add(new RowDefinition { Height = new GridLength(3, GridUnitType.Star) });
                gridForLabels.RowDefinitions.Add(new RowDefinition { Height = new GridLength(2, GridUnitType.Star) });
                gridForLabels.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) });
                gridForLabels.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) });
                gridForLabels.ColumnSpacing = 0;
                gridForLabels.RowSpacing = 0;
                gridForLabels.Children.Add(new Label()
                {
                    Text = currentLetter,
                    FontSize = 20,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    VerticalOptions = LayoutOptions.CenterAndExpand
                }, 0, 0);

                gridForLabels.Children.Add(new Label()
                {
                    Text = getValues.LetterWorth(currentLetter.First()).ToString(),
                    FontSize = 12,
                    Margin = 0
                }, 1, 1);
                //Creates a frame to get borders around those labels
                insideGrid.Children.Add(new Frame()
                {
                    BorderColor = Color.Black,
                    BackgroundColor = currentPlayerColor,
                    Content = gridForLabels,
                    HeightRequest = 40,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Padding = 0,
                    Margin = 0
                }, i, 0);
            }

            wordContainer.Children.Add(insideGrid);
            grid.Children.Add(wordContainer, 1, grid.RowDefinitions.Count - 1);
            if(currentPlayerTurn == 2)
                turn--;
            if (highscoreWordPoints < getValues.WordWorth(FramesToWords.WordCreator(wordCreationBar)))
            {
                highscoreWord = "";
                highscoreWordPoints = getValues.WordWorth(FramesToWords.WordCreator(wordCreationBar));
                Label localLabel;
                foreach (Frame frame in wordCreationBar)
                {
                    try
                    {
                        Grid mark = (Grid)frame.Content;
                        localLabel = (Label)mark.Children[0];
                    }
                    catch (Exception)
                    {
                        localLabel = (Label)frame.Content;
                    }
                    highscoreWord += localLabel.Text;
                }
            }

            if (currentPlayerTurn == 1)
            {
                totalPointsP1 += getValues.WordWorth(FramesToWords.WordCreator(wordCreationBar));
                viewPointCounterP1Label.Text = "totale score: " + totalPointsP1.ToString();
            }
            else
            {
                totalPointsP2 += getValues.WordWorth(FramesToWords.WordCreator(wordCreationBar));
                viewPointCounterP2Label.Text = "totale score: " + totalPointsP2.ToString();
            }
                //viewPointCounter.Text = "totale score: " + totalPoints;
                viewTurnCounterLabel.Text = "beurten over: " + turn;
            // PushButton Geluid
            var player2 = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
            player2.Load("Correct.wav");
            player2.Play();
            if (turn <= 0)
            {
                GameOverHandler();
            }
            currentPlayerColor = currentPlayerColor == player1Color ? player2Color : player1Color;
            currentPlayerTurn = currentPlayerTurn == 1 ? 2 : 1;
            currentPlayersTurnLabel.Text = "beurt: speler " + currentPlayerTurn;
            EmptyPushwordBar();
            FillUsableLetterBord();
            viewCurrentPushwordValueLabel.Text = "Dit woord: " + getValues.WordWorth(FramesToWords.WordCreator(wordCreationBar)) + " punten";
        }

        private void PushButton_Clicked(object sender, EventArgs e)
        {
            // geluid voor pushbutton (sorry dat t hardcoded is)
            var player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
            player.Load("Click.wav");
            player.Play();

            PushCurrentWord();
        }

        public Char RandomLetterGenerator()
        {
            char randomLetter = ConfigFile.charPool[ConfigFile.random.Next(0, ConfigFile.charPool.Length - 1)];
            currentLetterValue = getValues.LetterWorth(randomLetter);
            return randomLetter;
        }

        public void EmptyPushwordBar()
        {
            foreach (Frame frame in wordCreationBar)
            {
                Grid grid = (Grid)frame.Content;
                Label label = (Label)grid.Children[0];
                label.Text = "";
                label = (Label)grid.Children[1];
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
                frame.BackgroundColor = currentPlayerColor;
                Grid grid = (Grid)frame.Content;
                Label label = (Label)grid.Children[0];
                Label labelPoints = (Label)grid.Children[1];
                if (label.Text == "")
                {
                    label.Text = RandomLetterGenerator().ToString();
                    labelPoints.Text = currentLetterValue.ToString();
                }
            }
        }

        private void ShuffleUsableLetterBord(object sender, EventArgs e)
        {
            //geluid voor de Shuffleknop
            var player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
            player.Load("Click.wav");
            player.Play();

            if (currentPlayerTurn == 1)
            {
                if (remainingShufflesP1 <= 0)
                {
                    return;
                }
                foreach (Frame frame in UsableLetterList)
                {
                    Grid grid = (Grid)frame.Content;
                    Label label = (Label)grid.Children[0];
                    label.Text = RandomLetterGenerator().ToString();
                    Label labelPoints = (Label)grid.Children[1];
                    labelPoints.Text = currentLetterValue.ToString();
                }
                EmptyPushwordBar();
                remainingShufflesP1--;
                shuffleCounterP1Label.Text = "Shuffles: " + remainingShufflesP1;
            }
            else
            {
                if (remainingShufflesP2 <= 0)
                {
                    return;
                }
                foreach (Frame frame in UsableLetterList)
                {
                    Grid grid = (Grid)frame.Content;
                    Label label = (Label)grid.Children[0];
                    label.Text = RandomLetterGenerator().ToString();
                    Label labelPoints = (Label)grid.Children[1];
                    labelPoints.Text = currentLetterValue.ToString();
                }
                EmptyPushwordBar();
                remainingShufflesP2--;
                shuffleCounterP2Label.Text = "Shuffles: " + remainingShufflesP2;
            }
        }

        protected void UIPushBarCreation()
        {
            Grid grid = new Grid()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            for (Int32 i = 0; i < ConfigFile.wordLength; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            }
            for (int i = 0; i < grid.ColumnDefinitions.Count; i++)
            {
                Grid gridForLabels = new Grid();
                gridForLabels.RowDefinitions.Add(new RowDefinition { Height = new GridLength(3, GridUnitType.Star) });
                gridForLabels.RowDefinitions.Add(new RowDefinition { Height = new GridLength(2, GridUnitType.Star) });
                gridForLabels.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) });
                gridForLabels.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) });
                gridForLabels.ColumnSpacing = 0;
                gridForLabels.RowSpacing = 0;
                gridForLabels.Children.Add(new Label()
                {
                    Text = "",
                    FontSize = 20,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    VerticalOptions = LayoutOptions.CenterAndExpand
                }, 0, 0);

                gridForLabels.Children.Add(new Label()
                {
                    Text = "",
                    FontSize = 12,
                    Margin = 0
                }, 1, 1);
                //Creates a frame to get borders around those labels
                Frame frame = new Frame()
                {
                    BorderColor = Color.Black,
                    Content = gridForLabels,
                    HeightRequest = ConfigFile.unrealHighNumber,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Padding = 0,
                    Margin = 0
                };

                var tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.Tapped += (s, e) => { SwapLetters(s, e); };
                frame.GestureRecognizers.Add(tapGestureRecognizer);
                grid.Children.Add(frame, i, 0);
                AddLetersToList(frame);
                wordCreationBarStackLayout.Children.Add(grid);
            }
        }
        public void GameOverHandler()
        {
            if (BasePage.CurrentUserId.HasValue)
            {
                BasePage.ScoreAPIService.AddOrUpdate(new Score(BasePage.CurrentUserId.Value, totalPointsP1, DateTimeOffset.Now, difficultyMultiplier == 3, highscoreWord, highscoreWordPoints, this.difficultyEnum));
            }

            var gesture = new TapGestureRecognizer();
            gesture.Tapped += (s, e) =>
            {
                Navigation.PushAsync(new GameOverPage(false, "speler 1", totalPointsP1, difficultyMultiplier == 3, difficultySelected, highscoreWord, highscoreWordPoints, totalPointsP2));
            };
            Content.GestureRecognizers.Add(gesture);
        }

        private async void BackButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}