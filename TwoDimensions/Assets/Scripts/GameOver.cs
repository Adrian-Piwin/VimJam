using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void clickedTryAgain(){
        SceneManager.LoadScene("MainScene");
    }

    public void clickedMenu(){
        SceneManager.LoadScene("MainMenu");
    }
}
