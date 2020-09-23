using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public DoorManagement door;
    public PlatformManagement platform;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Cube"){
            animator.SetBool("isPressing", true);

            if (door != null){
                door.openDoor();
            }
           

            if (platform != null){
                platform.startMoving();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Cube"){
            animator.SetBool("isPressing", false);

            if (door != null){
                door.closeDoor(); 
            }

            if (platform != null){
                platform.stopMoving();
            }
        }
    }
}
