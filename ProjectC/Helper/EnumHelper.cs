using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;

namespace ProjectC.Helper
{
    class EnumHelper
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        public static Dictionary<Int32, String> GetIntWithDisplayNames<T>() where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("The given type is no Enum");
            }

            Dictionary<Int32, String> enumItems = new Dictionary<Int32, String>();

            foreach (FieldInfo fieldInfo in typeof(T).GetFields())
            {
                if (!fieldInfo.FieldType.IsEnum)
                    continue;

                object[] attributes = fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), true);

                if (attributes.Any())
                {
                    DisplayAttribute displayAttribute = attributes[0] as DisplayAttribute;

                    ResourceManager resourceManager = new ResourceManager(displayAttribute.ResourceType);

                    enumItems.Add((Int32)fieldInfo.GetValue(null), resourceManager.GetString(displayAttribute.Name));
                }
                else
                {
                    enumItems.Add((Int32)fieldInfo.GetValue(null), fieldInfo.Name);
                }
            }

            return enumItems;
        }
    }
}
