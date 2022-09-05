using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Text highScore;

    public AudioSource buttonPress;

    //Метод перехода на игровую сцену:
   public void Play()
    {
        buttonPress.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //Метод выхода из игры:
    public void Exit()
    {
        Application.Quit();
    }

    private void Start()
    {
        highScore.text = "BEST SCORE : " + (int)PlayerPrefs.GetFloat("highscore");
    }
}
