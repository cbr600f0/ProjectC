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
    public partial class helpPage : ContentPage
    {
        public Xamarin.Forms.LineBreakMode LineBreakMode { get; set; }
        public helpPage() 
        {
            InitializeComponent();
        }
    }
}