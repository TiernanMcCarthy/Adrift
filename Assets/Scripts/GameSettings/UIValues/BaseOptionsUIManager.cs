using UnityEngine;
using UnityEngine.UI;
using System;
using System.Reflection;
using GameSettings;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.IO;

public class BaseOptionsUIManager : MonoBehaviour
{
    public BaseSetting settings;
    public GameObject uiContainer; // Parent GameObject to hold all UI elements

    public List<FieldHolder> fieldButtons = new List<FieldHolder>();

    public FieldHolderFloatMM minMaxFloatPrefab;

    public FieldHolderBool boolPrefab;
    void Start()
    {
        //GenerateUI();
    }

    public void GenerateUI()
    {
        //Collect Fields that settings will write to later
        Type settingsType = settings.GetType();
        FieldInfo[] fields = settingsType.GetFields(BindingFlags.Public | BindingFlags.Instance);

        //Clear old list of fieldholders for UI Refresh
        foreach(FieldHolder fieldHolder in fieldButtons)
        {
            Destroy(fieldHolder.gameObject);
        }
        fieldButtons.Clear();

        //Generate UI elements for field list
        foreach (FieldInfo field in fields)
        {
            FieldHolder temp;
            if (field.FieldType==typeof(float))
            {
                field.SetValue(settings, 5.0f);
            }
            else if(field.FieldType==typeof(MinMaxFloat))
            {
                temp = Instantiate(minMaxFloatPrefab);
                temp.PopulateDetails(this, field);
            }
        }
        
    }

    public void AddToUICanvas(FieldHolder obj)
    {
        obj.transform.SetParent(uiContainer.transform);
        obj.transform.localPosition = new Vector3(0, 0, 0);
        fieldButtons.Add(obj);
    }

    public void ApplySettings()
    {
        foreach (FieldHolder fh in fieldButtons)
        {
            fh.WriteValue();
        }
        OptionUIController.instance.currentOption.settings = settings;
        OptionUIController.instance.currentOption.hasChanged = false;
        GameOptionsManager.SaveOption(settings);
    }

}
