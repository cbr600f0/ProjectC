using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProjectC.Model
{
    public enum SecurityQuestionEnum
    {
        [Display(Name = nameof(Resources.SecurityQuestionEnum.PetQuestion), ResourceType = typeof(Resources.SecurityQuestionEnum))]
        PetQuestion = 0,
        [Display(Name = nameof(Resources.SecurityQuestionEnum.SchoolQuestion), ResourceType = typeof(Resources.SecurityQuestionEnum))]
        SchoolQuestion = 1,
        [Display(Name = nameof(Resources.SecurityQuestionEnum.BestFriendQuestion), ResourceType = typeof(Resources.SecurityQuestionEnum))]
        BestFriendQuestion = 2
    }
}
