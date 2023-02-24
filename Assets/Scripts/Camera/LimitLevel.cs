using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitLevel : MonoBehaviour
{
    [SerializeField] bool enemy;
    string collisionTag;

    private void Start()
    {
        if (enemy)
        {
            collisionTag = "Enemy";
        }
        else 
        {
           collisionTag = "Bullet";
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(collisionTag))
        {
            Destroy(collision.gameObject);
        }
        else if (collisionTag == "Enemy" && collision.gameObject.CompareTag("LifeGem")) 
        {
            Destroy(collision.gameObject);
        }
    }
}
