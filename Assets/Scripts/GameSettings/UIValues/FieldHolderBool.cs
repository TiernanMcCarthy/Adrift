using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
namespace GameSettings
{
    public class FieldHolderBool : FieldHolder
    {
        private SaveBool localInstance;

        [SerializeField]
        private Toggle toggleUI;

        public override void PopulateDetails(BaseOptionsUIManager p, FieldInfo fi)
        {
            field = fi;
            parent = p;
            localInstance = (SaveBool)field.GetValue(parent.settings);
            
            fieldTitle.text = localInstance.GetName();
            p.AddToUICanvas(this);
        }

        public override void ChangeValue()
        {
            base.ChangeValue();
        }

        public override void WriteValue()
        {
            base.WriteValue();
        }

    }
}
