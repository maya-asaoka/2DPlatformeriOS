using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameController : MonoBehaviour {

    // singleton
    public static GameController instance;

    public Text HPText;
    public GameObject GameOverText;
    public GameObject WinningText;

    public int initialHP = 3;
    public bool gameOver = false;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            HPText.text = " HP: " + initialHP;
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
        WinningText.SetActive(true);
        gameOver = true;
    }
}
