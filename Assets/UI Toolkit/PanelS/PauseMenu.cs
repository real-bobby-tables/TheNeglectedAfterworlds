using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
public class PauseMenu : MonoBehaviour
{
  [SerializeField] GameObject pauseMenu;
  private bool didPause = false;

  public void Pause()
  {
    pauseMenu.SetActive(true);
    Time.timeScale = 0f;
  }

  public void Resume()
  {
    pauseMenu.SetActive(false);
    Time.timeScale = 1f;
  }

  public void Home(int sceneID)
  {
    Time.timeScale = 1f;
    SceneManager.LoadScene(sceneID);
  }


  public void Update()
  {
    if (Input.GetKey(KeyCode.Escape))
    {
      if (didPause)
      {
        Resume();
        didPause = false;
      }
      else {
        didPause = true;
        Pause();
      }
    }
  }
}
