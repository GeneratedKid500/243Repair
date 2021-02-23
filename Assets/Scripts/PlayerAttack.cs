using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject[] attackHitBoxes;
    public float attackWindow = 0.2f;

    public bool attack2Unlocked = false;
    public bool attack3Unlocked = false;

    public int enemiesKilled = 0;

    [HideInInspector] public bool isBlocking;

    Animator animator;
    PlayerMovement playerMovement;
    float attackTimer;

    [Header("Sounds")]
    AudioSource audioS;
    public AudioClip punch1;
    public AudioClip punch2;
    public AudioClip randomLine;

    void Start()
    {
        audioS = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        GetAttackInput();

        GetBlockInput();

        int i = Random.Range(0, 100001);
        if (i == 7563)
        {
            audioS.PlayOneShot(randomLine);
        }
    }

    void LateUpdate()
    {
        if (enemiesKilled >= 2 && !attack2Unlocked)
        {
            attack2Unlocked = true;
            EnemySpawning.instance.Unlocked2();
        }
        else if (enemiesKilled >= 10 && !attack3Unlocked)
        {
            attack3Unlocked = true;
            EnemySpawning.instance.Unlocked3();
        }
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

                audioS.PlayOneShot(punch1);

                attackHitBoxes[0].SetActive(true);
                attackHitBoxes[2].SetActive(false);

                attackTimer = attackWindow;
            }
            else if (PlayerAnimatorRef.instance.currentState == PlayerAnimatorRef.instance.punch1State && attack2Unlocked)
            {
                animator.SetBool("Attack2", true);
                animator.SetBool("Attack", false);

                audioS.PlayOneShot(punch1);

                attackHitBoxes[1].SetActive(true);
                attackHitBoxes[0].SetActive(false);

                attackTimer = attackWindow;
            }
            else if (PlayerAnimatorRef.instance.currentState == PlayerAnimatorRef.instance.punch2State && attack3Unlocked)
            {
                animator.SetBool("Attack3", true);
                animator.SetBool("Attack2", false);

                audioS.PlayOneShot(punch2);

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
