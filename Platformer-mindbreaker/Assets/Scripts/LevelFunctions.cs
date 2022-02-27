using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFunctions : MonoBehaviour
{
    public void restart()
    {
        SceneManager.LoadScene(1);
    }
    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
    public void Resume()
    {
        Time.timeScale = 1f;
    }
    public void Pause()
    {
        Time.timeScale = 0f;
    }
}
