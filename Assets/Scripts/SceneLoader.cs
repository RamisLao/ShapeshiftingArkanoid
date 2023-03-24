using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private GameObject endScreen;
    [SerializeField] private GameObject ball;
    [SerializeField] private string playScene;

    public void ShowEndScreen()
    {
        endScreen.SetActive(true);
        Destroy(ball);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(playScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
