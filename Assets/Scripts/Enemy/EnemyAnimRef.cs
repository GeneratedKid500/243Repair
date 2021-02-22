using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimRef : MonoBehaviour
{
    Animator animator;

    public int currentState;
    [HideInInspector] public int idleState = Animator.StringToHash("Base Layer.Idle");
    [HideInInspector] public int walkState = Animator.StringToHash("Base Layer.Walk");

    [HideInInspector] public int punch1State = Animator.StringToHash("Base Layer.Punch1");
    [HideInInspector] public int punch2State = Animator.StringToHash("Base Layer.Punch2");
    [HideInInspector] public int punch3State = Animator.StringToHash("Base Layer.Punch3");

    [HideInInspector] public int blockState = Animator.StringToHash("Base Layer.Block");
    [HideInInspector] public int hurtState = Animator.StringToHash("Base Layer.Hurt");
    [HideInInspector] public int fallState = Animator.StringToHash("Base Layer.Fall");

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        currentState = animator.GetCurrentAnimatorStateInfo(0).fullPathHash;

        if (currentState == idleState)
        {
            Debug.Log("ENEMY Idle");
        }
        else if (currentState == walkState)
        {
            Debug.Log("ENEMY Walk");
        }
        else if (currentState == punch1State)
        {
            Debug.Log("ENEMY Punch 1");
        }
        else if (currentState == punch2State)
        {
            Debug.Log("ENEMY Punch 2");
        }
        else if (currentState == punch3State)
        {
            Debug.Log("ENEMY Punch 3");
        }
        else if (currentState == blockState)
        {
            Debug.Log("ENEMY Block");
        }
        else if (currentState == hurtState)
        {
            Debug.Log("ENEMY Hurt");
        }
        else if (currentState == fallState)
        {
            Debug.Log("ENEMY Fall");
        }
        else
        {
            Debug.Log(currentState.ToString());
        }
    }
}
