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

    private float time = 0;

    // Update is called once per frame
    void Update()
    {
        if (canMove){
            float pingPong = Mathf.PingPong(Time.time + time * speed, 1);
            platform.position = Vector3.Lerp(leftPos.position, rightPos.position, pingPong);
        }else{
            time = Time.time;
        }
    }

    public void stopMoving(){
        canMove = false;
    }

    public void startMoving(){
        canMove = true;
    }

}
