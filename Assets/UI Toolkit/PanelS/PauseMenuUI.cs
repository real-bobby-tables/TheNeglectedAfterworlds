using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Events;
public class PauseMenuUI : MonoBehaviour
{

    public UnityEvent ResumeEvent;
    
  private void Awake()
  {
      VisualElement root = GetComponent<UIDocument>().rootVisualElement;
      root.Q<Button>("ResumeButton").clicked += () => ResumeEvent.Invoke();
      root.Q<Button>("QuitButton").clicked += () => Application.Quit();
  }



}
