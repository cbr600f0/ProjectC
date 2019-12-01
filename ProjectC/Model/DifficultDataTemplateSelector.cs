using System;
using System.Collections.Generic;
using System.Text;

using ProjectC.Model;

using Xamarin.Forms;

namespace ProjectC.Model
{
    class DifficultDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate Easy { get; set; }
        public DataTemplate Med { get; set; }
        public DataTemplate Hard { get; set; }
        public DataTemplate Legend { get; set; }
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return  ((Difficulty)item).Clicked.Contains("easy") ? Easy : 
                    ((Difficulty)item).Clicked.Contains("med") ? Med : 
                    ((Difficulty)item).Clicked.Contains("hard") ? Hard : 
                    ((Difficulty)item).Clicked.Contains("legend") ? Legend : 
                    Med;
        }
    }
}
