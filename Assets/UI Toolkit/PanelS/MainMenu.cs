using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    float difficulty = 1.0f;

    VisualElement mainMenu;
    VisualElement playbar;
    VisualElement difficultybar;
    AudioSource menuMusic;
    GameObject audioMenu;
    GameObject graphicsMenu;
    private void Awake()
    {
        graphicsMenu = transform.GetChild(0).gameObject;
        audioMenu = transform.GetChild(1).gameObject;
        graphicsMenu.SetActive(false);
        audioMenu.SetActive(false);
        mainMenu = GetComponent<UIDocument>().rootVisualElement;
         playbar = mainMenu.Q<VisualElement>("PlayBar");
         difficultybar = mainMenu.Q<VisualElement>("DifficultyBar");

        playbar.Q<Button>("Play").clicked += InitGame;
        playbar.Q<Button>("GSettings").clicked += GraphicsSettings;
        playbar.Q<Button>("ASettings").clicked += AudioSettings;
        playbar.Q<Button>("Quit").clicked += () => Application.Quit();
        difficultybar.Q<Button>("EDButton").clicked += () => {difficulty = 0.75f; };
        difficultybar.Q<Button>("NDButton").clicked += () => {difficulty = 1.0f; };
        difficultybar.Q<Button>("HDButton").clicked += () => {difficulty = 1.5f; };
    }

    public void Display(bool display)
    {
        if (display)
        {
            playbar.style.display = DisplayStyle.Flex;
            playbar.SetEnabled(true);
            difficultybar.style.display = DisplayStyle.Flex;
            difficultybar.SetEnabled(true);
            Debug.Log("Displaying main menu!");
        }
        else {
            playbar.style.display = DisplayStyle.None;
            playbar.SetEnabled(false);
            difficultybar.style.display = DisplayStyle.None;
            difficultybar.SetEnabled(false);
            Debug.Log("Hiding main menu!");
        }
    }

    void Start()
    {
        menuMusic = GetComponent<AudioSource>();
    }

    public void SetNewVolume()
    {
        float newMusicVolume = PlayerPrefs.GetFloat("MainMusicVolume", 0.5f);
        Debug.Log("Changing volume to " + newMusicVolume);
        menuMusic.volume = newMusicVolume;

    }

    void AudioSettings()
    {
        Display(false);
        audioMenu.SetActive(true);
    }

    void GraphicsSettings()
    {
        Display(false);
        graphicsMenu.SetActive(true);
    }

    void InitGame()
    {
        menuMusic.Stop();
        PlayerPrefs.SetFloat("Difficulty", difficulty);
        SceneManager.LoadScene("FirstLevel");
    }
}
