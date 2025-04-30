using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameoverManager : MonoBehaviour
{
    [SerializeField] GameObject GameOverScreen1;
    [SerializeField] Button restartButton;
    [SerializeField] Button exitButton;

    private GameManager randomizer;

    void Start()
    {
        randomizer = FindObjectOfType<GameManager>();

        if (restartButton != null)
            restartButton.onClick.AddListener(Restart);

        if (exitButton != null)
            exitButton.onClick.AddListener(Exit);
    }

    public void GameOver()
    {
        GameOverScreen1.SetActive(true);
        Time.timeScale = 0f;
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("RestartGame function called!");
    }


    public void Exit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Shadowhaven_TitleMenu");
    }
}
