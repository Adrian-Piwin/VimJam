using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public DoorManagement door;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Cube"){
            door.openDoor();
            animator.SetBool("isPressing", true);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Cube"){
            door.closeDoor();
            animator.SetBool("isPressing", false);
        }
    }
}
