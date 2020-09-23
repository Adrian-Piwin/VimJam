using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManagement : MonoBehaviour
{
    public float doorDuration = 1f;

    public Transform up;
    public Transform down;

    private IEnumerator lerpOpenDoor;
    private IEnumerator lerpCloseDoor;

    void Start(){

    }

    public void openDoor(){
        if (lerpCloseDoor != null)
            StopCoroutine(lerpCloseDoor);
        lerpOpenDoor = LerpPosition(transform, up.position, doorDuration);
        StartCoroutine(lerpOpenDoor);
    }

    public void closeDoor(){
        if (lerpOpenDoor != null)
            StopCoroutine(lerpOpenDoor);
        lerpCloseDoor = LerpPosition(transform, down.position, doorDuration);
        StartCoroutine(lerpCloseDoor);
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
