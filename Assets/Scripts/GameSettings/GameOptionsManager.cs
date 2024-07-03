using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using WebSocketSharp;

namespace GameSettings
{

    [Serializable]
    public class OptionField
    {
        [SerializeField]protected string fieldName;

        public string GetName()
        {
            return fieldName;
        }
    }


    [System.Serializable]
    public class MinMaxFloat:OptionField
    {
        public float minValue; public float maxValue;

        public float value;

        public MinMaxFloat(string fieldTitle,float minValue, float maxValue, float value)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
            this.value = value;
            this.fieldName = fieldTitle;
        }

        public void SetValue(float minValue, float maxValue, float value) 
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
            this.value = value;
        }
    }
    [System.Serializable]
    public class MinMaxFloat2 : OptionField
    {
        public float minValue; public float maxValue;

        public float value;

        public MinMaxFloat2(string fieldTitle, float minValue, float maxValue, float value)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
            this.value = value;
            this.fieldName = fieldTitle;
        }

        public void SetValue(float minValue, float maxValue, float value)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
            this.value = value;
        }
    }
    [System.Serializable]
    public class BaseSetting
    {
        public virtual void SaveData()
        {
            string newJSON = JsonUtility.ToJson(this);
            PlayerPrefs.SetString(GetType().Name,newJSON);
            PlayerPrefs.Save();
        }
    }
    [System.Serializable]
    public class ControlOptions : BaseSetting
    {
        public MinMaxFloat lookSensitivity = new MinMaxFloat("Look Sensitivity",0.1f, 100, 22);
        public bool invertView = false;

        public bool toggleCrouch = false;

        public bool newThrowOffVariable = true;

    }
    public class GameOptionsManager: MonoBehaviour
    {

        public ControlOptions controlSettings;

        
        // Start is called before the first frame update
        void Start()
        {
            ///
            /// Control Options Load
            ///
            string json = PlayerPrefs.GetString("ControlOptions");
            ControlOptions local;
            if (!json.IsNullOrEmpty())
            {
                local = JsonUtility.FromJson<ControlOptions>(PlayerPrefs.GetString("ControlOptions"));
            }
            else
            {
                local = new ControlOptions();
            }
            controlSettings = local;


        }

        public static void SaveOption()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
