using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ProjectC.Pages;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectC.Model
{
    class DifficultyViewModel
    {
        readonly IList<Difficulty> source;

        public ObservableCollection<Difficulty> Difficulties { get; private set; }

        public DifficultyViewModel()
        {
            source = new List<Difficulty>();
            CreateDifficultyCollection();
        }

        void CreateDifficultyCollection()
        {
            source.Add(new Difficulty
            {
                Name = "Makkelijk",
                Image = "Emoji_Easy.png",
                Clicked = "easy"
            });
            source.Add(new Difficulty
            {
                Name = "Gemiddeld",
                Image = "Emoji_Medium.png",
                Clicked = "medium"
            });
            source.Add(new Difficulty
            {
                Name = "Moeilijk",
                Image = "Emoji_Hard.png",
                Clicked = "hard"
            });
            source.Add(new Difficulty
            {
                Name = "Legendarisch",
                Image = "Emoji_Legendary.png",
                Clicked = "legend" 
            }); ;

            Difficulties = new ObservableCollection<Difficulty>(source);
        }
    }
}
