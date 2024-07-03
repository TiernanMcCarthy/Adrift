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

        public virtual void ApplySettings()
        {

        }

        public void OnSelected()
        {
            Color oldColour = buttonAsset.color;
            oldColour.a = 1f;
            buttonAsset.color = oldColour;
        }

        public void Deselected()
        {
            Color oldColour = buttonAsset.color;
            oldColour.a = 0.2f;
            buttonAsset.color = oldColour;
        }
    }
}
