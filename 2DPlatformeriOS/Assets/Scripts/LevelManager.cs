using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public void startLevel(bool timeMode) {
        SceneManager.LoadScene(1);
        GameController.instance.timeModeOn = timeMode;
    }
}
