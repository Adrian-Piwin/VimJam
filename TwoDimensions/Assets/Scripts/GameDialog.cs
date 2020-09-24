using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDialog : MonoBehaviour
{
    public TVController tvController;
    private GameManagement gameManagement;

    private List<List<string>> dialogs;
    private int currentDialog = 0;
    private int currentText = 0;
    private bool canType;

    // Start is called before the first frame update
    void Start()
    {
        gameManagement = GetComponent<GameManagement>();
        dialogs = new List<List<string>>();

        // Introduction
        dialogs.Add(new List<string>(){
            "hello fucking idoit",
            "NERD GO",
            "GOOD LUCK!"
        });

        // Level One completed
        dialogs.Add(new List<string>(){
            "test1",
            "test1"
        });

        // Level Two completed
        dialogs.Add(new List<string>(){
            "test2",
            "test2"
        });

        // Level Three completed (end)
        dialogs.Add(new List<string>(){
            "test3",
            "test3"
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (canType){
            if (tvController.getIsEmpty()){
            
                tvController.displayText(dialogs[currentDialog][currentText]);
                currentText ++;

                if (currentText >= dialogs[currentDialog].Count){
                    currentText = 0;
                    currentDialog++;
                    canType = false;
                    gameManagement.dialogFinished();
                }
            }
        }
    }

    public void startDialog(){
        canType = true;
    }
}
