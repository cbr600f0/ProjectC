using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace ProjectC.Pages
{
    public class TestPageToBeDeleted : ContentPage
    {
        public TestPageToBeDeleted()
        {
            Grid grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(3, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnSpacing = 0;
            grid.RowSpacing = 0;
            Content = new StackLayout
            {
                Children = {
                    new Frame()
                    {
                        BorderColor = Color.Black,
                        Content = grid,
                        WidthRequest = 50,
                        HeightRequest = 50,
                        HorizontalOptions = LayoutOptions.Start,
                        Padding = 0,
                        Margin = 0
                    }
                }
            };

            grid.Children.Add(
                new Label()
                {
                    Text = "U",
                    FontSize = 20,
                    Margin = 0
                }, 0, 0
            );

            grid.Children.Add(
                new Label()
                {
                    Text = "4",
                    FontSize = 10,
                    Margin = 0
                }, 1, 1
            );
        }
    }
}