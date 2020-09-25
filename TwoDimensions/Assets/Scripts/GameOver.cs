using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameManagement gameManagement;

    public void clickedTryAgain(){
        gameManagement.resetLevel();
    }

    public void clickedMenu(){
        SceneManager.LoadScene("MainMenu");
    }
}
