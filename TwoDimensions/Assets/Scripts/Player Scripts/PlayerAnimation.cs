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
                break;
            case "idle":
                animator.SetBool("isRunning", false);
                break;
            case "jump":
                animator.Play("legjumpanim");
                break;
        }
    }
}
