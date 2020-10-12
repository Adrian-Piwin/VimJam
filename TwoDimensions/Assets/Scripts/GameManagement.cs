using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManagement : MonoBehaviour
{
    [Header("Levels")]
    public List<GameObject> levels;
    public List<float> levelTimers;
    public float levelLoadTime = 3f;

    [Header("References")]
    public GameObject player;
    public CameraController cameraController;
    public EndDoorManager endDoor;
    public DoorManagement startingDoor;
    public Transform startingDoorObj;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI timer;
    public GameObject gameOverMenu;

    private GameDialog gameDialog;
    private int currentLvl;
    private GameObject currentLvlObj;

    private bool timerOn = false;
    private float currentTimer;
    private float time = 0;

    private IEnumerator startGameOver;

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
                startGameOver = gameOver(1.5f);
                cameraController.changeTarget(startingDoorObj);
                StartCoroutine(startGameOver);
            }
        }
        
    }

    // Close door and game over
    IEnumerator gameOver(float time){
        yield return new WaitForSeconds(time);
        startingDoor.closeDoor();
        yield return new WaitForSeconds(time);
        gameOverMenu.SetActive(true);
        player.GetComponent<PlayerController>().setCanMove(false);
        
    }

    IEnumerator gameWon(){
        yield return new WaitForSeconds(4f);
        endDoor.openDoor();
        SceneManager.LoadScene("WinScreen");
    }

    // Dialog finished
    public void dialogFinished(){
        if (currentLvl != (levels.Count)){
            loadNextLevel();
            StartCoroutine(openDoor(levelLoadTime+0.3f));
        }else{
            StartCoroutine(gameWon());
        }
    }

    // Player enters/leaves start room
    public void playerPassedDoor(){
        // stop/start timer
        if (player.GetComponent<PlayerKey>().getKeyState()){
            // stop timer
            if (startGameOver != null)
                StopCoroutine(startGameOver);
            cameraController.changeTarget(player.transform);
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
        timer.color = new Color32(0, 0, 0, 255);
    }

    // Reset level on fail
    public void resetLevel(){
        resetTimer();
        timerOn = false;
        currentTimer = levelTimers[currentLvl-1];
        player.transform.position = Vector3.zero;
        gameOverMenu.SetActive(false);
        player.GetComponent<PlayerController>().setCanMove(true);
        player.GetComponent<PlayerKey>().setKeyState(false);
        cameraController.changeTarget(player.transform);

        // Reset Level
        currentLvl--;
        gameDialog.resetDialog();
    }

    private void loadNextLevel(){
        Vector3 newPos;

        if (currentLvlObj != null){
            newPos = currentLvlObj.transform.position;
            newPos.y = -20;
            StartCoroutine(LerpPosition(currentLvlObj.transform, newPos, levelLoadTime));
            Destroy(currentLvlObj, levelLoadTime+0.5f);
        }

        currentLvlObj = Instantiate(levels[currentLvl], null);

        newPos = currentLvlObj.transform.position;
        newPos.y = 0;
        StartCoroutine(LerpPosition(currentLvlObj.transform, newPos, levelLoadTime));

        currentTimer = levelTimers[currentLvl];
        currentLvl ++;
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
