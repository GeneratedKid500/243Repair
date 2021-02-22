using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject[] attackHitBoxes;
    public float attackWindow = 0.2f;

    public bool attack2Unlocked = true;
    public bool attack3Unlocked = true;

    [HideInInspector] public bool isBlocking;

    Animator animator;
    PlayerMovement playerMovement;
    float attackTimer;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        GetAttackInput();

        GetBlockInput();
    }

    void GetAttackInput()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (PlayerAnimatorRef.instance.currentState == PlayerAnimatorRef.instance.idleState || 
                PlayerAnimatorRef.instance.currentState == PlayerAnimatorRef.instance.walkState ||
                PlayerAnimatorRef.instance.currentState == PlayerAnimatorRef.instance.punch3State)
            {
                animator.SetBool("Attack", true);
                animator.SetBool("Attack3", false);

                attackHitBoxes[0].SetActive(true);
                attackHitBoxes[2].SetActive(false);

                attackTimer = attackWindow;
            }
            else if (PlayerAnimatorRef.instance.currentState == PlayerAnimatorRef.instance.punch1State && attack2Unlocked)
            {
                animator.SetBool("Attack2", true);
                animator.SetBool("Attack", false);

                attackHitBoxes[1].SetActive(true);
                attackHitBoxes[0].SetActive(false);

                attackTimer = attackWindow;
            }
            else if (PlayerAnimatorRef.instance.currentState == PlayerAnimatorRef.instance.punch2State && attack3Unlocked)
            {
                animator.SetBool("Attack3", true);
                animator.SetBool("Attack2", false);

                attackHitBoxes[2].SetActive(true);
                attackHitBoxes[1].SetActive(false);

                attackTimer = attackWindow;
            }
        }

        if (attackTimer >= 0)
        {
            attackTimer -= Time.deltaTime;
        }
        else
        {
            animator.SetBool("Attack", false);
            animator.SetBool("Attack2", false);
            animator.SetBool("Attack3", false);

            for (int i = 0; i < attackHitBoxes.Length; i++)
            {
                attackHitBoxes[i].SetActive(false);
            }
        }
    }

    void GetBlockInput()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            animator.SetBool("Block", true);
            if (!isBlocking) isBlocking = true;
        }
        else
        {
            animator.SetBool("Block", false);
            if (isBlocking) isBlocking = false;
        }
    }
}
