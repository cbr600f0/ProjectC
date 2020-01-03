using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ProjectC.Pages
{
    public static class FramesToWords
    {

        public static string WordCreator(List<Frame> frames)
        {
            string fullWord = "";
            foreach (Frame frame in frames)
            {
                Grid labelGrid = (Grid)frame.Content;
                Label label = (Label)labelGrid.Children[0];
                fullWord += label.Text;
            }

            return fullWord;
        }
    }
}
