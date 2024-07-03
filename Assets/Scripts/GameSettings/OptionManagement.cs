using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSettings
{

    public class OptionManagement : MonoBehaviour
    {

        public bool hasChanged = false;

        public BaseSetting settings;

        public TMPro.TMP_Text buttonAsset;

        public GameObject uiContent;

        public virtual void ToggleContent(bool toggle)
        {
            uiContent.SetActive(toggle);
        }

        public virtual void ApplySettings(List<FieldHolder> fieldOptions)
        {
            foreach(FieldHolder fh in fieldOptions)
            {
                fh.WriteValue();
            }
        }
        protected virtual void GenerateUI()
        {
            
        }
        public void OnSelected()
        {
            Color oldColour = buttonAsset.color;
            oldColour.a = 1f;
            buttonAsset.color = oldColour;
            OptionUIController.instance.currentOption = this;
            GenerateUI();
        }

        public void Deselected()
        {
            Color oldColour = buttonAsset.color;
            oldColour.a = 0.2f;
            buttonAsset.color = oldColour;
        }
    }
}
