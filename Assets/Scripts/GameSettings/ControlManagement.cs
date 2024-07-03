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
        public override void ApplySettings(List<FieldHolder> fieldOptions)
        {
            
        }

        private void Start()
        { 

        }
        protected override void GenerateUI()
        {
            uiManager = FindObjectOfType<BaseOptionsUIManager>();
            ControlOptions controlOptions = LoadControlOptions();
            uiManager.settings = controlOptions;
            uiManager.GenerateUI();
        }
        ControlOptions LoadControlOptions()
        {
            string json = PlayerPrefs.GetString("ControlOptions", string.Empty);
            if (!string.IsNullOrEmpty(json))
            {
                return JsonUtility.FromJson<ControlOptions>(json);
            }
            return new ControlOptions();
        }
    }
}
