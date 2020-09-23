using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetAnimation(string anim){
        switch (anim){
            case "running":
                animator.SetBool("isRunning", true);
                animator.SetBool("isCrouching", false);
                break;
            case "idle":
                animator.SetBool("isRunning", false);
                animator.SetBool("isCrouching", false);
                Debug.Log("idle");
                break;
            case "jump":
                animator.Play("legjumpanim");
                break;
            case "crouch":
                animator.SetBool("isCrouching", true);
                animator.SetBool("isRunning", false);
                Debug.Log("crouch");
                break;
            case "crouch walk":
                animator.SetBool("isCrouching", true);
                animator.SetBool("isRunning", true);
                break;
        }
    }
}
