using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public void Play1vs1()
    {
        SceneManager.LoadScene("Gameplay1vs1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
