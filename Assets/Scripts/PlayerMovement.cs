using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Transform tf;
    Rigidbody2D rb;

    [Header ("Movement")]
    public float xMoveSpeed = 1;
    public float yMoveSpeed = 1;
    Vector2 moveSpeeds;
    Vector2 direction;
    bool canMove = true;

    [Header ("Movement Clamping")]
    public float xMin;
    public float xMax;
    public float yMin;
    public float yMax;

    [Header("HitStun")]
    public int HP = 100;
    public Collider2D hurtBox;
    public float damageStun = 0.3f;
    public float fallStun = 0.75f;


    Animator animator;
    public bool facingLeft = false;

    void Start()
    {
        tf = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();

        moveSpeeds = new Vector2(xMoveSpeed, yMoveSpeed);
    }

    void Update()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Facing();

        if (PlayerAnimatorRef.instance.currentState != PlayerAnimatorRef.instance.idleState && 
            PlayerAnimatorRef.instance.currentState != PlayerAnimatorRef.instance.walkState)
        {
            xMoveSpeed *= 0;
            yMoveSpeed *= 0;
        }
        else
        {
            xMoveSpeed = moveSpeeds.x;
            yMoveSpeed = moveSpeeds.y;
        }
    }

    void FixedUpdate()
    {
        //Movement Apply
        Vector3 movement = new Vector3(direction.x * xMoveSpeed, direction.y * yMoveSpeed, 0.0f);
        animator.SetInteger("isMoving", Mathf.Abs((int)direction.x) + Mathf.Abs((int)direction.y));
        tf.position += movement * Time.deltaTime;
        tf.position = new Vector3(Mathf.Clamp(tf.position.x, xMin, xMax), Mathf.Clamp(tf.position.y, yMin, yMax), tf.position.z);
    }

    public void GetHit(int damage)
    {
        if (PlayerAnimatorRef.instance.currentState != PlayerAnimatorRef.instance.blockState)
        {
            HP -= damage;
            if (HP > 0 && damage < 3)
            {
                StartCoroutine(TakeDamage());
            }
            else
            {
                StartCoroutine(KnockedOver());
            }
        }
        else
        {
            HP -= Mathf.RoundToInt(damage / 3);
        }
    }

    IEnumerator TakeDamage()
    {
        if (PlayerAnimatorRef.instance.currentState == PlayerAnimatorRef.instance.fallState)
            yield break;

        animator.SetBool("Hurt", true);
        canMove = false;
        hurtBox.enabled = false;

        yield return new WaitForSeconds(damageStun);

        animator.SetBool("Hurt", false);
        canMove = true;
        hurtBox.enabled = true;
    }

    IEnumerator KnockedOver()
    {
        if (PlayerAnimatorRef.instance.currentState == PlayerAnimatorRef.instance.hurtState)
            yield break;

        animator.Play("Fall");
        canMove = false;
        hurtBox.enabled = false;

        yield return new WaitForSecondsRealtime(fallStun);

        animator.Play("Idle");
        canMove = true;
        hurtBox.enabled = true;
    }

    /// <summary>
    /// flips the facing direction of sprites 
    /// </summary>
    void Facing()
    {
        if (canMove)
        {
            if (direction.x > 0 && facingLeft || direction.x < 0 && !facingLeft)
            {
                facingLeft = !facingLeft;
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 0.0f);
            }
        }
    }
}
