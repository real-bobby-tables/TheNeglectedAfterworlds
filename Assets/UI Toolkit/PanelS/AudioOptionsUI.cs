using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UIElements;

public class AudioOptionsUI : MonoBehaviour
{
    MainMenu mui;
    VisualElement root;
    Slider musicVolume;
    Slider sfxVolume;

    void OnEnable()
    {
        mui = transform.parent.gameObject.GetComponent<MainMenu>();
        root = GetComponent<UIDocument>().rootVisualElement;
        musicVolume = root.Q<Slider>("VolSlider");
        sfxVolume = root.Q<Slider>("SfxVolSlider");
        root.Q<Button>("ApplyButton").clicked += ApplySettings;
        root.Q<Button>("BackButton").clicked += BackToMainMenu;
    }


    void BackToMainMenu()
    {
        Debug.Log("Attempting to go back into the main menu");
        mui.Display(true);
        gameObject.SetActive(false);
    }


    void ApplySettings()
    {
        float normalizedMusicVolume = musicVolume.value / 100f;
        float normalizedSFXVolume = sfxVolume.value / 100f;
        Debug.Log("Music volume set to " + normalizedMusicVolume);
        Debug.Log("SFX volume set to " + normalizedSFXVolume);
        PlayerPrefs.SetFloat("MainMusicVolume", normalizedMusicVolume);
        PlayerPrefs.SetFloat("SFXVolume", normalizedSFXVolume);
        mui.SetNewVolume();
    }

}
