using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
    public DoorManagement startingDoor;

    public List<GameObject> levels;
    public float levelLoadTime = 3f;

    private GameDialog gameDialog;
    private int currentLvl = -1;

    // Start is called before the first frame update
    void Start()
    {
        gameDialog = GetComponent<GameDialog>();

        gameDialog.startDialog();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void playerPassedDoor(){
        
    }

    // Dialog finished
    public void dialogFinished(){
        if (currentLvl != 3){
            loadNextLevel();
            StartCoroutine(openDoor(levelLoadTime+0.1f));
        }
    }

    public void doorUnlocked(){
        
    }

    // Open door on delay
    IEnumerator openDoor(float timer){
        yield return new WaitForSeconds(timer);
        startingDoor.openDoor();
    }

    private void loadNextLevel(){
        Vector3 newPos;

        if (currentLvl != -1){
            newPos = levels[currentLvl].transform.position;
            newPos.y = -20;
            StartCoroutine(LerpPosition(levels[currentLvl].transform, newPos, levelLoadTime));
        }

        newPos = levels[currentLvl+1].transform.position;
        newPos.y = 0;
        StartCoroutine(LerpPosition(levels[currentLvl+1].transform, newPos, levelLoadTime));

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
