//using System;
//using System.Collections.Generic;
//using System.Text;
//using Xamarin.Forms;

//namespace ProjectC.Pages
//{
//    class UICreatorSingleplayerPage
//    {
//        public void PlayedWordsUICreator()
//        {
//            Grid grid = new Grid() { VerticalOptions = LayoutOptions.FillAndExpand };
//            StackLayout wordContainer = new StackLayout() { VerticalOptions = LayoutOptions.CenterAndExpand };
//            Grid insideGrid = new Grid() { VerticalOptions = LayoutOptions.CenterAndExpand };

//            for (Int32 i = 0; i < wordRows; i++)
//            {
//                //Defines the amount of rows needed for all the past-created (history) words
//                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
//            }
//            //Creates 2 spaces, 25% for the playername who played the word, 75% for the word that is played.
//            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
//            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) });

//            // Creates the rows where the past-created (history) words are.
//            for (Int32 row = 0; row < grid.RowDefinitions.Count(); row++)
//            {
//                wordContainer.HorizontalOptions = LayoutOptions.CenterAndExpand;
//                // Adds the label (name of the person who played the word) to the left of the word
//                grid.Children.Add(new Label() { Text = "Word " + (row + 1), HorizontalOptions = LayoutOptions.CenterAndExpand }, 0, row);

//                for (Int32 i = 0; i < wordLength; i++)
//                {
//                    //Creates gridcolumns equal to the amount of letters needed for the word. (1 column equals 1 letter)
//                    insideGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
//                }

//                //Adds the letters to create 1 word
//                for (Int32 i = 0; i < insideGrid.ColumnDefinitions.Count; i++)
//                {
//                    StackLayout stacklaout = new StackLayout();
//                    stacklaout.Children.Add(new Label()
//                    {
//                        Text = "A",
//                        FontSize = 20,
//                        HorizontalOptions = LayoutOptions.CenterAndExpand
//                    });
//                    stacklaout.Children.Add(new Label()
//                    {
//                        Text = currentLetterValue.ToString(),
//                        FontSize = 13,
//                        HorizontalTextAlignment = TextAlignment.Center
//                    });

//                    //Creates a frame to get borders around those labels
//                    insideGrid.Children.Add(new Frame()
//                    {
//                        //Creates the labels for the history words (1 label is 1 letter)
//                        Content = stacklaout,
//                        Margin = 0,
//                        Padding = 0,
//                        BorderColor = Color.Black
//                    }, i, 0);
//                }


//                //Adds the whole word (defined in the insideGrid) to the stacklayout
//                wordContainer.Children.Add(insideGrid);
//                //Adds the stacklayout with the whole word in it to the right side of the row (so the player name and the word are next to each other)
//                grid.Children.Add(wordContainer, 1, row);

//                //The playername and the made word are passed to the grid-container
//                //So now we can make the stacklayout and insideGrid empty so they can be refilled again with the next row
//                wordContainer = new StackLayout() { VerticalOptions = LayoutOptions.CenterAndExpand };
//                insideGrid = new Grid() { VerticalOptions = LayoutOptions.CenterAndExpand };
//            }
//            //This is the element on the front end. This will be the container (equal to a div) that holds all the history words
//            MiddlePart.Content = grid;
//        }
//    }
//}
