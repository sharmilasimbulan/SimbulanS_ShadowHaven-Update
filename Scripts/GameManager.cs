using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        RandomizeObjects();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void RandomizeObjects()
    {
        GameObject[] collectItems = GameObject.FindGameObjectsWithTag("Item");
        GameObject[] evilRobots = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject item in collectItems)
        {
            item.transform.position = new Vector3(Random.Range(-5f, 5f), 1f, Random.Range(-5f, 5f));
        }

        foreach (GameObject robot in evilRobots)
        {
            robot.transform.position = new Vector3(Random.Range(-10f, 10f), 1f, Random.Range(-10f, 10f));
        }

        Debug.Log("Positions randomized after scene reload.");
    }
}
