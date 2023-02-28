using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeGem : MonoBehaviour
{
    public float healtRestored;
    SpriteRenderer spriteRenderer;

    Rigidbody2D rigidbody2D;
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        healtRestored = Random.Range(10, 31);
        Debug.Log(healtRestored);
        spriteRenderer.color = new Color(0f, 255f, 0f, healtRestored);

    }

    void Update()
    {
        Vector2 movementEnemy = new Vector2(-1, 0) * 9;
        rigidbody2D.velocity = movementEnemy + new Vector2(0, 1) * Mathf.Sin(Time.time * 4) * 1.3f;
    }
}
