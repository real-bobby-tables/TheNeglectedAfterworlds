using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
public class PauseMenu : MonoBehaviour
{
  //[SerializeField] GameObject pauseMenu;
  private bool didPause = false;
  VisualElement pauseMenuPanel;
  PlayerController pc;
  void Start()
  {
    pc = FindObjectOfType<PlayerController>();
    pauseMenuPanel = GetComponent<UIDocument>().rootVisualElement;
    pauseMenuPanel.Q<Button>("MainMenuButton").clicked += GoToMainMenu;
    pauseMenuPanel.Q<Button>("ResumeButton").clicked += Resume;
    pauseMenuPanel.Q<Button>("QuitButton").clicked += () => Application.Quit();
    pauseMenuPanel.SetEnabled(false);
    pauseMenuPanel.visible = false;
  }

  public void Pause()
  {
    Time.timeScale = 0f;
    pauseMenuPanel.SetEnabled(true);
    pauseMenuPanel.visible = true;
    didPause = true;
    pc.DisableMovement();
  }

  public void Resume()
  {
    Time.timeScale = 1f;
    pauseMenuPanel.SetEnabled(false);
    pauseMenuPanel.visible = false;
    didPause = false;
    pc.EnableMovement();
  }

  public void GoToMainMenu()
  {
    Time.timeScale = 1f;
    SceneManager.LoadScene("MainMenu");
  }


  public void Update()
  {
    if (Input.GetKey(KeyCode.Escape))
    {
      if (didPause)
      {
        Resume();
      }
      else {
        Pause();
      }
    }
  }
}
