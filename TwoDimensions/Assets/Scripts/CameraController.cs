using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public GameObject followCam;
    public GameObject passiveCam;

    public Transform leftLimit;
    public Transform rightLimit;

    private bool following = true;

    // Update is called once per frame
    void Update()
    {

        if (following)
            passiveCam.transform.position = new Vector3(player.transform.position.x, 0, passiveCam.transform.position.z);
        else
            followCam.transform.position = new Vector3(player.transform.position.x, 0, followCam.transform.position.z);

        if (followCam.transform.position.x <= leftLimit.position.x){
            following = false;
            followCam.SetActive(false);
            passiveCam.SetActive(true);
        }else{
            following = true;
            followCam.SetActive(true);
            passiveCam.SetActive(false);
        }
    }


}
