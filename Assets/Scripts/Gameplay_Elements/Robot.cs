using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        Initialization();
    }


    private void Initialization()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("IsWalking", false);
        StartIdle();
    }

    private void ChangeAnimationState(string state)
    {
        switch (state)
        {
            case "Idle":
                animator.SetBool("IsWalking", false);
                break;
            case "Walking":
                animator.SetBool("IsWalking", true);
                break;
        }
    }

    public void StartWalking()
    {
        ChangeAnimationState("Walking");
    }

    public void StartIdle()
    {
        ChangeAnimationState("Idle");
    }

    public void ChangeFaceDirection()
    {
        var rotationVector = transform.rotation.eulerAngles;
        rotationVector.y *= -1;
        transform.rotation = Quaternion.Euler(rotationVector);
    }
}
