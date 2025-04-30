using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    private GameManager randomizer;

    void Start()
    {
        randomizer = FindObjectOfType<GameManager>();
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("RestartGame function called!");
    }

    public void Exit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Shadowhaven_TitleMenu");
    }
}
