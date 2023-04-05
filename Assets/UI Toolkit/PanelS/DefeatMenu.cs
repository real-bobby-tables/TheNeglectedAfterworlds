using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;


public class DefeatMenu : MonoBehaviour
{

    private void Awake()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        root.Q<Button>("RetryButton").clicked += () => SceneManager.LoadScene(0);
        root.Q<Button>("QuitButton").clicked += () => Application.Quit();
    }
}
