using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
using System;
using GameSettings;
public class SettingsUIManager : MonoBehaviour
{
    public BaseSetting settings;
    public GameObject uiContainer; // A parent GameObject to hold all UI elements

    void Start()
    {
        GenerateUI();
    }

    public void GenerateUI()
    {
        Type settingsType = settings.GetType();
        FieldInfo[] fields = settingsType.GetFields(BindingFlags.Public | BindingFlags.Instance);

        foreach (FieldInfo field in fields)
        {
            if (field.FieldType == typeof(bool))
            {
                CreateToggle(field);
            }
            else if (field.FieldType == typeof(float))
            {
                CreateSlider(field);
            }
        }
    }

    void CreateToggle(FieldInfo field)
    {
        GameObject toggleObject = new GameObject(field.Name);
        toggleObject.transform.SetParent(uiContainer.transform);

        toggleObject.transform.position = Vector3.zero;

        Toggle toggle = toggleObject.AddComponent<Toggle>();
        Text label = toggleObject.AddComponent<Text>();
        label.text = field.Name;

        toggle.isOn = (bool)field.GetValue(settings);
        toggle.onValueChanged.AddListener((value) =>
        {
            field.SetValue(settings, value);
            settings.SaveData();
        });
    }

    void CreateSlider(FieldInfo field)
    {
        GameObject sliderObject = new GameObject(field.Name);
        sliderObject.transform.SetParent(uiContainer.transform);
        sliderObject.transform.position = Vector3.zero;
        Slider slider = sliderObject.AddComponent<Slider>();
        Text label = sliderObject.AddComponent<Text>();
        label.text = field.Name;

        slider.value = (float)field.GetValue(settings);
        slider.onValueChanged.AddListener((value) =>
        {
            field.SetValue(settings, value);
            settings.SaveData();
        });

        // Assuming you want to set min and max values for the slider
        RangeAttribute range = field.GetCustomAttribute<RangeAttribute>();
        if (range != null)
        {
            slider.minValue = range.min;
            slider.maxValue = range.max;
        }
    }
}