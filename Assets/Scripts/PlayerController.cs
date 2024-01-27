using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float speed = 8f;
    Vector2 movement;

    int selectedProp = 0;

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

        if (Input.mouseScrollDelta.y > 0f)
        {
            selectedProp++;
            if (selectedProp >= (int)PropType.None)
                selectedProp = 0;

            Debug.Log($"Selected {selectedProp}");
        }
        else if (Input.mouseScrollDelta.y < 0f)
        {
            selectedProp--;
            if (selectedProp < 0)
                selectedProp = (int)PropType.None - 1;

            Debug.Log($"Selected {selectedProp}");
        }

        if (Input.GetMouseButtonUp(0))
        {
            PropManager.Instance.SetProp((PropType)selectedProp, transform.position);
        }
    }
}
