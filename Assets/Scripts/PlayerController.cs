using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float speed = 8f;
    Vector2 movement;

    void Update()
    {
        movement = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
        {
            //move up
            movement.y += speed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            //move down
            movement.y -= speed;
        }

        if (Input.GetKey(KeyCode.D))
        {
            //move right
            movement.x += speed;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            //move left
            movement.x -= speed;
        }

        movement.x *= Time.deltaTime;
        movement.y *= Time.deltaTime;

        transform.Translate(new Vector3(movement.x, movement.y, 0f));
    }
}
