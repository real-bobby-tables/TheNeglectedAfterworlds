using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MainMenuPresentation : MonoBehaviour
{
    private void Awake()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        root.Q<Button>("Start").clicked += () => SceneManager.LoadScene(0);
        root.Q<Button>("Settings").clicked += () => Debug.Log("Please show some settings");
        root.Q<Button>("Quit").clicked += () => Application.Quit();
    }
}
