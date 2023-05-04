using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;


public class DefeatMenu : MonoBehaviour
{
    VisualElement root;

    private void Awake()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        root.Q<Button>("RetryButton").clicked += () => SceneManager.LoadScene(0);
        root.Q<Button>("QuitButton").clicked += () => Application.Quit();
        root.visible = false;
        root.SetEnabled(false);
    }

    public void Defeat()
    {
        root.SetEnabled(true);
        root.visible = true;
        Time.timeScale = 0f;
    }
}
