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
    private bool skipDialog = false;

    // Start is called before the first frame update
    void Start()
    {
        gameManagement = GetComponent<GameManagement>();
        dialogs = new List<List<string>>();

        // Introduction
        dialogs.Add(new List<string>(){
            "Hello Subject 317",
            "The door will open and as you enter the timer will start",
            "Reach the end of the course and collect the key",
            "Return with the key before the time runs out",
            "Don't mess up"
        });

        // Level One completed
        dialogs.Add(new List<string>(){
            "You've done well",
            "Continue to do so"
        });

        // Level Two completed
        dialogs.Add(new List<string>(){
            "You've made it quite farther than the others",
            "Only one more key left",
            "Try to be a bit quicker this time"
        });

        // Level Three completed (end)
        dialogs.Add(new List<string>(){
            "I'm impressed",
            "Your the first generation to make it this far",
            "Oh, the door? There's nothing behind it",
            "Anyways, I need to make room for the next generation",
            "Good bye, 317"
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (canType){
            if (tvController.getIsEmpty()){

                if (skipDialog){
                    currentText = dialogs[currentDialog].Count-1;
                    skipDialog = false;
                }
            
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

    public void resetDialog(){
        skipDialog = true;
        currentDialog--;
        canType = true;
    }

    public void startDialog(){
        canType = true;
    }
}
