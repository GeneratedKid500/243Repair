using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Movement")]
    bool canMove = true;
    float moveSpeedmin = 0.5f;
    float moveSpeedmax = 1.3f;
    float moveSpeed;
    float tempMoveSpeed;

    EnemyAnimRef animRef;
    Transform tf;
    Transform target;
    Animator animator;

    bool facingLeft = false;
    Vector3 targetDistance;

    [Header("Attacking")]
    public GameObject[] attackHitboxes;
    int attackCounter;

    public float timeBetweenAttacks;
    float attackTimer;

    [Header("Being Hit")]
    public int HP;
    public float stunTime;
    public int floorTime;
    public Collider2D hurtbox;

    void Start()
    {
        tf = transform;
        target = Object.FindObjectOfType<PlayerMovement>().transform;
        animator = GetComponentInChildren<Animator>();
        animRef = GetComponentInChildren<EnemyAnimRef>();

        moveSpeed = Random.Range(moveSpeedmin, moveSpeedmax);
        tempMoveSpeed = moveSpeed;
    }

    void Update()
    {
        if (canMove)
        {
            moveSpeed = tempMoveSpeed;
            targetDistance = target.position - transform.position;
            Flip();
        }
        else
        {
            moveSpeed = 0;
        }

    }

    void FixedUpdate()
    {
        if (Mathf.Abs(targetDistance.x) >= 1f && canMove)
        {
            tf.position += Vector3.MoveTowards(tf.position, targetDistance, 20) * (moveSpeed/200);
            animator.SetInteger("isMoving", 1);

            if (attackCounter > 0) attackCounter = 0;

            for (int i = 0; i < attackHitboxes.Length; i++)
            {
                if (attackHitboxes[i].activeInHierarchy)
                {
                    attackHitboxes[i].SetActive(false);
                }
            }
            animator.SetBool("Attack", false);
            animator.SetBool("Attack2", false);
            animator.SetBool("Attack3", false);
        }

        if (Mathf.Abs(targetDistance.x) < 1f)
        {
            animator.SetInteger("isMoving", 0);
            Attack();
        }
    }

    void Attack()
    {
        if (attackTimer <= 0)
        {
            if (animRef.currentState == animRef.idleState)
            {
                animator.SetBool("Attack", true);
                animator.SetBool("Attack3", false);

                attackHitboxes[0].SetActive(true);
                attackHitboxes[2].SetActive(false);

                attackTimer = timeBetweenAttacks;
                attackCounter += 1;
            }
            else if (animRef.currentState == animRef.punch1State)
            {
                animator.SetBool("Attack2", true);
                animator.SetBool("Attack", false);

                attackHitboxes[1].SetActive(true);
                attackHitboxes[0].SetActive(false);

                attackTimer = timeBetweenAttacks;
                attackCounter += 1;

            }
            else if (animRef.currentState == animRef.punch2State && attackCounter >= 6)
            {
                animator.SetBool("Attack3", true);
                animator.SetBool("Attack2", false);

                attackHitboxes[2].SetActive(true);
                attackHitboxes[1].SetActive(false);

                attackTimer = timeBetweenAttacks;
            }
            else if (animRef.currentState == animRef.punch3State ||
            animRef.currentState == animRef.punch2State && attackCounter < 6)
            {
                animator.SetBool("Attack2", false);
                animator.SetBool("Attack3", false);

                attackHitboxes[1].SetActive(false);
                attackHitboxes[2].SetActive(false);

                attackTimer = timeBetweenAttacks;
            }
        }
        else
        {
            attackTimer-= Time.fixedDeltaTime;
        }
    }

    public void GetHit(int damage)
    {
        canMove = false;
        Debug.Log(damage);
        HP -= damage;
        if (HP > 0 && damage < 3)
        {
            StartCoroutine(TakeDamage());
        }
        else
        {
            StartCoroutine(KnockOver());
        }
    }

    IEnumerator TakeDamage()
    {
        if (animRef.currentState == animRef.fallState)
            yield break;

        animator.SetBool("Hurt", true);

        yield return new WaitForSeconds(stunTime);

        animator.SetBool("Hurt", false);
        canMove = true;
    }

    IEnumerator KnockOver()
    {
        canMove = false;
        animator.Play("Fall");
        hurtbox.enabled = false;

        yield return new WaitForSecondsRealtime(floorTime);

        if (HP <= 0)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerAttack>().enemiesKilled++;
            EnemySpawning.instance.currentEnemies--;
            Destroy(this.gameObject);
        }


        hurtbox.enabled = true;
        canMove = true;

        animator.Play("Idle");
    }

    void Flip()
    {
        if (targetDistance.x > 0 && facingLeft || targetDistance.x < 0 && !facingLeft)
        {
            tf.localScale = new Vector3(-tf.localScale.x, tf.localScale.y);
            facingLeft = !facingLeft;
        }
    }
}
