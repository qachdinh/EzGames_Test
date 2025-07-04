using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public void Play1vs1()
    {
        GameConfig.SelectedMode = GameMode.OneVsOne;
        SceneManager.LoadScene("Gameplay");
    }

    public void Play1vs2()
    {
        GameConfig.SelectedMode = GameMode.OneVsTwo;
        SceneManager.LoadScene("Gameplay");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
