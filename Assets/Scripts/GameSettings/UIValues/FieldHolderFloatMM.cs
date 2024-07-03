using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
namespace GameSettings
{
    public class FieldHolderFloatMM : FieldHolder
    {
        [SerializeField]
        private MinMaxFloat localInstance;

        [SerializeField]
        private Slider slider;

        public override void PopulateDetails(BaseOptionsUIManager p,FieldInfo fi)
        {
            field = fi;
            parent = p;
            OptionField temp= (OptionField)field.GetValue(parent.settings);
            localInstance = (MinMaxFloat)temp;
            slider.minValue = localInstance.minValue;
            slider.maxValue = localInstance.maxValue;
            slider.value = localInstance.value;
            fieldTitle.text = localInstance.GetName();
            p.AddToUICanvas(gameObject);
        }
        public override void WriteValue()
        {
            base.WriteValue();
        }

    }
}
