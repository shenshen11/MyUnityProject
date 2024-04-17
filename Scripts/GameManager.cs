using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public int lives;
    public int characterIndex;

    private bool isPaused = false;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = true;
            PauseGame();
        }
    }


    public void PauseGame()
    {
        Time.timeScale = 0;
        FindObjectOfType<UIManager>().ShowMenu();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        isPaused = false;
        FindObjectOfType<UIManager>().HideMenu();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void SetVolume(float value)
    {
        AudioListener.volume = value;
    }

    public void Exit()
    {
        Application.Quit();
    }


}
