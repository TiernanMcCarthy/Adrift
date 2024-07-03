using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameSettings
{
    public class ControlManagement : OptionManagement
    {
        BaseOptionsUIManager uiManager;

        public Toggle result;
        public override void ApplySettings()
        {
            base.ApplySettings();
        }

        private void Start()
        {
            uiManager=FindObjectOfType<BaseOptionsUIManager>();
            ControlOptions controlOptions = LoadControlOptions();
            uiManager.settings = controlOptions;
            uiManager.GenerateUI();
        }

        ControlOptions LoadControlOptions()
        {
            string json = PlayerPrefs.GetString(nameof(ControlOptions), string.Empty);
            if (!string.IsNullOrEmpty(json))
            {
                return JsonUtility.FromJson<ControlOptions>(json);
            }
            return new ControlOptions();
        }
    }
}
