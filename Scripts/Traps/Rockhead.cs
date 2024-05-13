using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rockhead : MonoBehaviour
{
    Rigidbody2D rb;
    BoxCollider2D boxCollider2D;
    public float distance;
    bool isFalling = false;

    private float startX = 0;
    private float startY = 0;
    [SerializeField] public float speed = 0.4f;

    private float counterUp = 1f;
    private float counterDown = 2f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        startX = transform.position.x;
        startY = transform.position.y;
    }

    private void Update()
    {
        Physics2D.queriesStartInColliders = false;
        if(isFalling == false)
        {
            if(counterDown >= 0)
            {
                counterDown -= Time.deltaTime;
            }
            else
            {
                rb.gravityScale = 4;
                isFalling = true;
                counterDown = 2f;
            }
        }
        else
        {
            if (counterUp >= 0)
            {
                counterUp -= Time.deltaTime;
            }
            else
            {
                rb.gravityScale = 0;
                if (transform.position.y < startY)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y + speed * Time.deltaTime, transform.position.z);
                }
                else
                {
                    counterUp = 1f;
                    isFalling = false;
                }
            }
        }
    }
}
