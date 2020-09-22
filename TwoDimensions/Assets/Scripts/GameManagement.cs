using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
    public GameObject door;
    public float doorToggleTime = 1.3f;

    public List<GameObject> levels;
    public float levelLoadTime = 3f;

    private int currentLvl = -1;

    // Start is called before the first frame update
    void Start()
    {
        loadNextLevel();
        openDoor();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void openDoor(){
        StartCoroutine(LerpPosition(door.transform.GetChild(0), door.transform.GetChild(1).position, doorToggleTime));
    }

    private void closeDoor(){
        StartCoroutine(LerpPosition(door.transform.GetChild(0), door.transform.GetChild(2).position, doorToggleTime));
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

    public void playerPassedDoor(){
        
    }
}
