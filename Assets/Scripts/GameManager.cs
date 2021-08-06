using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int respawnTime;

    public static int score, health;

    void Start()
    {

    }

    public void Respawning()
    {
        StartCoroutine(DelayedRespawn());
    }

    IEnumerator DelayedRespawn()
    {
        if (respawnTime > 0)
        {
            yield return new WaitForSeconds(respawnTime);
            SceneManager.LoadScene("Game");
        }
        else
        {
            SceneManager.LoadScene("Game");
        }
    }

    public void Finish()
    {
        if (PlayerPrefs.GetInt("highscore") < Score.score)
        {
            PlayerPrefs.SetInt("highscore", Score.score);
        }
        SceneManager.LoadScene("Finish");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Home()
    {
        SceneManager.LoadScene("Home");
    }

    public void GameOver1()
    {
        Score.trophy1 = 0;
        Score.trophy2 = 0;
        Score.trophy3 = 0;
        Score.trophy4 = 0;
        Score.trophy5 = 0;
        SceneManager.LoadScene("Gameover1");
    }

    public void GameOver2()
    {
        Score.trophy1 = 0;
        Score.trophy2 = 0;
        Score.trophy3 = 0;
        Score.trophy4 = 0;
        Score.trophy5 = 0;
        SceneManager.LoadScene("Gameover2");
    }

    public void ResetPlayer()
    {
        Score.score = 0;
        Health.health = 5;
    }
}
