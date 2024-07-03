using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using TMPro;
namespace GameSettings
{
    public class FieldHolder : MonoBehaviour
    {
        [Header("Generic Field Details")]
        public FieldInfo field;
        public BaseOptionsUIManager parent;

        [Header("Object Specific Content")]
        public TMP_Text fieldTitle;


        public virtual void ChangeValue(BaseOptionsUIManager parent)
        {

        }

        public virtual void PopulateDetails(BaseOptionsUIManager parent,FieldInfo fi)
        {
            if(this!=null)
            {
               
            }
        }

        public virtual void WriteValue()
        {

        }
    }
}
