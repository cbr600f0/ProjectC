using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProjectC.Model
{
    public enum DifficultyEnum
    {
        [Display(Name = nameof(Resources.DifficultyEnum.Easy), ResourceType = typeof(Resources.DifficultyEnum))]
        Easy = 0,
        [Display(Name = nameof(Resources.DifficultyEnum.Medium), ResourceType = typeof(Resources.DifficultyEnum))]
        Medium = 1,
        [Display(Name = nameof(Resources.DifficultyEnum.Hard), ResourceType = typeof(Resources.DifficultyEnum))]
        Hard = 2,
        [Display(Name = nameof(Resources.DifficultyEnum.Legendary), ResourceType = typeof(Resources.DifficultyEnum))]
        Legendary = 3
    }
}
