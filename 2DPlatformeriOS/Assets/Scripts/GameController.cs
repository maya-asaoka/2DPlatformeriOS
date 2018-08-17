using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameController : MonoBehaviour {

    // singleton
    public static GameController instance;

    // true if time mode, false if enemies mode
    public bool timeModeOn;

    public Text HPText;
    public Text EnemiesLeftText;
    public GameObject GameOverText;
    public GameObject WinningTextEnemies;

    public Text TimeText;
    public Text EnemiesKilledText;
    public GameObject WinningTextTime;

    public int initialHP = 3;
    public bool gameOver = false;

    // enemies mode
    public int enemiesToKillToWin = 15;
    public int enemiesKilled = 0;

    // time mode
    public int timeStart;
    private float endTime;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            HPText.text = " HP: " + initialHP;

            if (timeModeOn) 
            {
                TimeText.gameObject.SetActive(true);
                EnemiesLeftText.gameObject.SetActive(false);
                TimeText.text = "Time: " + timeStart;
                endTime = Time.time + timeStart;
            }
            else
            {
                TimeText.gameObject.SetActive(false);
                EnemiesLeftText.gameObject.SetActive(true);
                EnemiesLeftText.text = "KILLED: 0/" + enemiesToKillToWin;
            }
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // if game over and player has pressed any key to continue, reload game
        if (gameOver && Input.anyKeyDown)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if (!timeModeOn) 
        {
            EnemiesLeftText.text = "KILLED: " + enemiesKilled + "/" + enemiesToKillToWin;
        }
        else if (timeModeOn) {
            int timeLeft = Mathf.RoundToInt(endTime - Time.time);
            TimeText.text = "Time: " + timeLeft;
            if (timeLeft <= 0) 
            {
                timeLeft = 0;
                PlayerWon();
            }
        }
    }

    // updates the HP UI and shows gameover text if hp = 0
    // returns true if player is still alive, false if dead
    public bool PlayerHit()
    {
        if (initialHP > 1)
        {
            initialHP = initialHP - 1;
            HPText.text = " HP: " + initialHP.ToString();
            return true;
        }
        else if (initialHP == 1)
        {
            initialHP = 0;
            HPText.text = " HP: 0";
            GameOverText.SetActive(true);
            gameOver = true;
            return false;
        }
        return true;
    }

    // shows winning text 
    public void PlayerWon()
    {
        if (timeModeOn)
        {
            WinningTextTime.SetActive(true);
            EnemiesKilledText.text = "Enemies Killed: " + enemiesKilled;
        }
        else
        {
            WinningTextEnemies.SetActive(true);
        }

        gameOver = true;
    }


    public void returnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

}
