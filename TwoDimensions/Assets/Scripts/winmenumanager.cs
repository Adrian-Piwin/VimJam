using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class winmenumanager : MonoBehaviour
{
    public void clickedMainMenu(){
        SceneManager.LoadScene("MainMenu");
    }
    
}
