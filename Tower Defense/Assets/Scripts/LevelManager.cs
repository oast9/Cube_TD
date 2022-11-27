using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static void LoadNextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public static void LoadPreviousLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex>1)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public static void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static void ExitGame()
    {
        Application.Quit();
    }
}
