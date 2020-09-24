using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManagement : MonoBehaviour
{
    [Header("Levels")]
    public List<GameObject> levels;
    public List<float> levelTimers;
    public float levelLoadTime = 3f;

    [Header("References")]
    public GameObject player;
    public DoorManagement startingDoor;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI timer;

    private GameDialog gameDialog;
    private int currentLvl = -1;
    private GameObject currentLvlObj;

    private bool timerOn = false;
    private float currentTimer;
    private float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        gameDialog = GetComponent<GameDialog>();

        gameDialog.startDialog();
    }

    void Update(){
        if (timerOn){
            currentTimer -= Time.deltaTime;

            updateTimer(currentTimer.ToString("F2"));

            if (currentTimer <= 10){
                timer.color = new Color32(255, 0, 0, 255);
            }

            if (currentTimer <= 0){
                updateTimer("0.00");
                timerOn = false;
                gameOver();
            }
        }
        
    }

    // Close door and game over
    private void gameOver(){
        startingDoor.closeDoor();
        // game lost
        // open menu to either go to menu
        // or go to checkpoint
    }

    private void gameWon(){
        // game won      
        // open main door
        // take to win screen  
    }

    // Dialog finished
    public void dialogFinished(){
        if (currentLvl != (levels.Count - 1)){
            loadNextLevel();
            StartCoroutine(openDoor(levelLoadTime+0.3f));
        }else{
            gameWon();
        }
    }

    // Player enters/leaves start room
    public void playerPassedDoor(){
        // stop/start timer
        if (player.GetComponent<PlayerKey>().getKeyState()){
            // stop timer
            timerOn = false;
            resetTimer();
            startingDoor.closeDoor();
        }else{
            //start timer if didnt already
            if (!timerOn){
                timerOn = true;
                time = Time.deltaTime;
            }
        }
    }

    // Lock was taken off of final door
    public void doorUnlocked(){
        gameDialog.startDialog();
    }

    // Open door on delay
    IEnumerator openDoor(float timer){
        yield return new WaitForSeconds(timer);
        startingDoor.openDoor();
    }

    // Update timer in UI
    private void updateTimer(string timerTxt){
        timerText.SetText("Door closes in");
        timer.SetText(timerTxt);
    }

    // Reset time text
    private void resetTimer(){
        timerText.SetText("");
        timer.SetText("");
    }

    private void loadNextLevel(){
        Vector3 newPos;

        if (currentLvl != -1){
            newPos = currentLvlObj.transform.position;
            newPos.y = -20;
            StartCoroutine(LerpPosition(currentLvlObj.transform, newPos, levelLoadTime));
            Destroy(currentLvlObj, levelLoadTime+0.5f);
        }

        currentLvl ++;

        currentLvlObj = Instantiate(levels[currentLvl], null);

        newPos = currentLvlObj.transform.position;
        newPos.y = 0;
        StartCoroutine(LerpPosition(currentLvlObj.transform, newPos, levelLoadTime));

        currentTimer = levelTimers[currentLvl];
    }

    IEnumerator LerpPosition(Transform original, Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = original.position;

        while (time < duration)
        {
            original.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        original.position = targetPosition;
    }
}
