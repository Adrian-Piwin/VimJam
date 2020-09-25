using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform target;
    public float smoothSpeed = 0.125f;
    public float deadzone = 3f;

    public Transform leftLimit;

    // Update is called once per frame
    void FixedUpdate()
    {

        Vector3 newPos = transform.position;
        newPos.x = target.position.x;

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, newPos, smoothSpeed); 

        if (smoothedPosition.x > leftLimit.position.x)
            transform.position = smoothedPosition;

    }

    public void changeTarget(Transform newT){
        target = newT;
    }


}
