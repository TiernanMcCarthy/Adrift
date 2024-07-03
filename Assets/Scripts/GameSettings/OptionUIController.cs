using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSettings
{
    public class OptionUIController : MonoBehaviour
    {

        [Header("UI Objects")]
        public GameObject optionsMenu;

        [Header("Option Menu Settings")]
        public OptionManagement currentOption;

        //Saves and checks options to save settings
        [SerializeField] private List<OptionManagement> optionsToCheck = new List<OptionManagement>();

        private UISystemControls uiInput;
        // Start is called before the first frame update
        void Start()
        {
            //Find and populate options so that they can be managed later
            OptionManagement[] temp = FindObjectsOfType<OptionManagement>();
            foreach (OptionManagement obj in temp)
            {
                optionsToCheck.Add(obj);
            }

            uiInput = new UISystemControls();

            uiInput.Enable();

            DontDestroyOnLoad(this);
        }

        public void ApplySettings()
        {
            foreach(OptionManagement obj in optionsToCheck)
            {
                if(obj.hasChanged)
                {

                }
            }
        }

        //Exits UI, but also checks to see if settings have been changed and need to be applied
        public bool ExitMenu()
        {
            bool showDialogue = false;
            foreach (OptionManagement obj in optionsToCheck)
            {
                if (obj.hasChanged)
                {
                    showDialogue = true; break;
                }
            }
            //Prevent exit of settings until user has applied Settings
            if (showDialogue)
            {
                return true;
            }
            else //exit UI
            {
                optionsMenu.SetActive(false);
            }

            return false;
        }

        private void ToggleSettings()
        {
            if (optionsMenu.activeSelf)
            {
                ExitMenu();
            }
            else
            {
                optionsMenu.SetActive(true);
            }
        }
        // Update is called once per frame
        void Update()
        {
            if(uiInput.UIControls.ToggleMenu.WasPressedThisFrame())
            {
                ToggleSettings();
            }
        }
    }
}
