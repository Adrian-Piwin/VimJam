using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManagement : MonoBehaviour
{
    public bool canMove = false;
    public float speed = 0.5f;

    public Transform platform;
    public Transform rightPos;
    public Transform leftPos;

    private float timer = 0;

    // Update is called once per frame
    void Update()
    {
        if (canMove){
            timer += Time.deltaTime;
            float pingPong = Mathf.PingPong(timer * speed, 1);
            platform.position = Vector3.Lerp(rightPos.position, leftPos.position, pingPong);
        }
    }

    public void stopMoving(){
        canMove = false;
    }

    public void startMoving(){
        canMove = true;
    }

}
