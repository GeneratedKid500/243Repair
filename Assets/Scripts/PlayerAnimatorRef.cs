using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorRef : MonoBehaviour
{
    public static PlayerAnimatorRef instance;

    Animator animator;

    public int currentState;
    [HideInInspector] public int idleState = Animator.StringToHash("Base Layer.Idle");
    [HideInInspector] public int walkState = Animator.StringToHash("Base Layer.Walk");

    [HideInInspector] public int punch1State = Animator.StringToHash("Base Layer.Punch1");
    [HideInInspector] public int punch2State = Animator.StringToHash("Base Layer.Punch2");
    [HideInInspector] public int punch3State = Animator.StringToHash("Base Layer.Punch3");

    [HideInInspector] public int shootState = Animator.StringToHash("Base Layer.Projectile");

    [HideInInspector] public int jumpState = Animator.StringToHash("Base Layer.Jump");
    [HideInInspector] public int jumpPunchState = Animator.StringToHash("Base Layer.JumpPunch");

    [HideInInspector] public int blockState = Animator.StringToHash("Base Layer.Block");
    [HideInInspector] public int hurtState = Animator.StringToHash("Base Layer.Hurt");
    [HideInInspector] public int fallState = Animator.StringToHash("Base Layer.Fall");



    void Awake()
    {
        instance = this;

        animator = GetComponent<Animator>();
    }

    void LateUpdate()
    {
        currentState = animator.GetCurrentAnimatorStateInfo(0).fullPathHash;

        if (currentState == idleState)
        {
            Debug.Log("Idle");
        }
        else if (currentState == walkState)
        {
            Debug.Log("Walk");
        }
        else if (currentState == punch1State)
        {
            Debug.Log("Punch 1");
        }
        else if (currentState == punch2State)
        {
            Debug.Log("Punch 2");
        }
        else if (currentState == punch3State)
        {
            Debug.Log("Punch 3");
        }
        else if (currentState == shootState)
        {
            Debug.Log("Projectile");
        }
        else if (currentState == jumpState)
        {
            Debug.Log("Jump");
        }
        else if (currentState == jumpPunchState)
        {
            Debug.Log("Jump Punch");
        }
        else if (currentState == blockState)
        {
            Debug.Log("Block");
        }
        else if (currentState == hurtState)
        {
            Debug.Log("Hurt");
        }
        else if (currentState == fallState)
        {
            Debug.Log("Fall");
        }
        else
        {
            Debug.Log(currentState.ToString());
        }
    }
}
