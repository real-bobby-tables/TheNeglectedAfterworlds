using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GraphicsOptionsUI : MonoBehaviour
{

    VisualElement root;
    DropdownField resolutionDropdown;
    DropdownField screenDropdown;

    MainMenu mui;

    void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        root.Q<Button>("ApplyButton").clicked += ApplySettings;
        root.Q<Button>("BackButton").clicked += BackToMainMenu;
        resolutionDropdown = root.Q<DropdownField>("ResolutionDropdown");
        screenDropdown = root.Q<DropdownField>("ScreenSettingsDropdown");

        mui = transform.parent.gameObject.GetComponent<MainMenu>();

        Resolution[] resolutions = Screen.resolutions;
        Debug.Log(resolutions);
        if(resolutionDropdown.choices == null)
        {
            Debug.Log("we got a null dropdown choice list");
            resolutionDropdown.choices = new List<string>();
        }

        resolutionDropdown.choices.Clear();
        
        for(int i = 0; i < resolutions.Length; i++)
        {
            resolutionDropdown.choices.Add(resolutions[i].width + "x" + resolutions[i].height);
        }
        Debug.Log("Finished adding choices, choice count is now " + resolutionDropdown.choices.Count);
        
        
        if (screenDropdown.choices == null)
        {
            Debug.Log("we got a null screen type dropdow");
            screenDropdown.choices = new List<string>();
        }
       
        //screenDropdown.choices.Clear();
        screenDropdown.choices.Add("FullScreenWindowed");
         
        screenDropdown.choices.Add("MaximizedWindowed");
        screenDropdown.choices.Add("Windowed");
        screenDropdown.choices.Add("ExclusiveFullScreen");
        Debug.Log("Screen count should now be " + screenDropdown.choices.Count);
       /* */
        
    }

    void BackToMainMenu()
    {
        Debug.Log("Attempting to go back into the main menu");
        mui.Display(true);
        this.gameObject.SetActive(false);
    }

    void ApplySettings()
    {
        string[] res = resolutionDropdown.value.Split("x");
        int width = int.Parse(res[0]);
        int height = int.Parse(res[1]);
        FullScreenMode screenMode = FullScreenMode.Windowed;
        switch(screenDropdown.value)
        {
            case "FullScreenWindowed": {
                screenMode = FullScreenMode.FullScreenWindow;
            } break;
            case "MaximizedWindowed": {
                screenMode = FullScreenMode.MaximizedWindow;
            } break;
            case "Windowed": {
                screenMode = FullScreenMode.Windowed;
            } break;
            case "ExclusiveFullScreen": {
                screenMode = FullScreenMode.ExclusiveFullScreen;
            } break;
        }
        Screen.SetResolution(width, height, screenMode);
    }
}
