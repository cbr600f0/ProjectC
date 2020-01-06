using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ProjectC.Pages
{
    public static class CreateNewFrame
    {
        public static Frame NewFrame(char letter)
        {
            Frame frame = new Frame();
            Grid grid = new Grid();
            frame.Content = grid;
            grid.Children.Add(new Label() { Text =  letter.ToString()});

            return frame;
        }
    }
}
