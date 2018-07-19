using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameController : MonoBehaviour {

    public static GameController instance;
    public Text HPText;
    public GameObject GameOverText;
    public GameObject WinningText;

    private int hp = 3;
    public bool gameOver = false;


    // Use this for initialization
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            HPText.text = " HP: " + hp;
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
        if (hp > 1)
        {
            hp = hp - 1;
            HPText.text = " HP: " + hp.ToString();
            return true;
        }
        else if (hp == 1)
        {
            hp = 0;
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
