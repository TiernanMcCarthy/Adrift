using UnityEngine;
using UnityEngine.UI;
using System;
using System.Reflection;
using GameSettings;
using System.Collections.Generic;
using Unity.VisualScripting;

public class BaseOptionsUIManager : MonoBehaviour
{
    public BaseSetting settings;
    public GameObject uiContainer; // Parent GameObject to hold all UI elements

    public List<FieldHolder> fieldButtons;

    public FieldHolderFloatMM minMaxFloatPrefab;
    void Start()
    {
        //GenerateUI();
    }

    public void GenerateUI()
    {
        Type settingsType = settings.GetType();
        FieldInfo[] fields = settingsType.GetFields(BindingFlags.Public | BindingFlags.Instance);
        foreach (FieldInfo field in fields)
        {
            FieldHolder temp = new();
            if (field.FieldType==typeof(float))
            {
                field.SetValue(settings, 5.0f);
            }
            else if(field.FieldType==typeof(MinMaxFloat))
            {
                temp = Instantiate(minMaxFloatPrefab);
            }

            temp.PopulateDetails(this, field);

        }
        
    }

    public void AddToUICanvas(GameObject obj)
    {
        obj.transform.parent = uiContainer.transform;

        obj.transform.localPosition = new Vector3(0, 0, 0);
    }

}
