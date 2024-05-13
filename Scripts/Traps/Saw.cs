using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    [SerializeField] private float endL;
    [SerializeField] private float endR;
    [SerializeField] private float speed;
    [SerializeField] private bool movingLeft = true;

    private float StartX;
    private float StartY;

    private void Start()
    {
        StartX = this.transform.position.x;
        StartY = this.transform.position.y;
    }

    private void Update()
    {
        if (movingLeft)
        {
            if (transform.position.x > StartX-endL)
            {
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
            {
                movingLeft = false;
            }
        }
        else
        {
            if (transform.position.x < StartX+endR)
            {
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
            {
                movingLeft = true;
            }
        }
    }


}
