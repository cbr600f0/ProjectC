using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectC.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class singleplayer : ContentPage
    {
        public int wordRows;
        public singleplayer()
        {
            InitializeComponent();
            wordRows = 7;
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