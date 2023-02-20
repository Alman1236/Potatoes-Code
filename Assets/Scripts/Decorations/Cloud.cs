using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public Vector2 movDirection;
    float speed;

    void Awake()
    {
        speed = Random.Range(Data.CloudsMinSpeed, Data.CloudsMaxSpeed);
        Destroy(gameObject, 200);
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        transform.Translate(movDirection * speed * Time.fixedDeltaTime);
    }
}
