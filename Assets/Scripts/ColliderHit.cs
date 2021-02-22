using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderHit : MonoBehaviour
{
    public int damage;

    void OnTriggerEnter2D(Collider2D other)
    {
        string thisTag = this.gameObject.GetComponentInParent<Transform>().tag;

        if (thisTag == other.tag)
        {
            Physics2D.IgnoreCollision(this.GetComponentInParent<Collider2D>(), other);
            Debug.Log("Ignored!");
        }

        switch (other.tag)
        {
            case "Player":
                Debug.Log("Hit Player!");
                other.GetComponentInParent<PlayerMovement>().GetHit(damage);
                break;

            case "Enemy":
                Debug.Log("Hit Enemy!");
                other.GetComponentInParent<EnemyMovement>().GetHit(damage);
                break;

            default:
                Debug.Log("Hit" + other.name);
                break;
        }
    }
}
