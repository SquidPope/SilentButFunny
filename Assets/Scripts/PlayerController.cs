using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class PropSelectionEvent : UnityEvent<PropType> { }
public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigid;

    float speed = 8f;
    Vector2 movement;

    int selectedProp = 0;

    float scrollTimer = 0f;
    float scrollDelay = 0.2f; //how often to register the scroll wheel change

    PropSelectionEvent propSelect = new PropSelectionEvent();
    public PropSelectionEvent PropSelect { get { return propSelect; } }

    static PlayerController instance;
    public static PlayerController Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = GameObject.FindGameObjectWithTag("Player");
                instance = go.GetComponent<PlayerController>();
            }

            return instance;
        }
    }

    public Vector3 GetPosition() { return transform.position; }

    private void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        scrollTimer += Time.deltaTime;
        if (scrollTimer >= scrollDelay)
        {
            if (Input.mouseScrollDelta.y > 0f)
            {
                selectedProp++;
                if (selectedProp >= (int)PropType.None - 1)
                    selectedProp = 0;

                Debug.Log($"Selected {selectedProp}");
                PropSelect.Invoke((PropType)selectedProp);
                scrollTimer = 0f;
            }
            else if (Input.mouseScrollDelta.y < 0f)
            {
                selectedProp--;
                if (selectedProp < 0)
                    selectedProp = (int)PropType.None - 1;

                Debug.Log($"Selected {selectedProp}");
                PropSelect.Invoke((PropType)selectedProp);
                scrollTimer = 0f;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            PropManager.Instance.SetProp((PropType)selectedProp, transform.position);
        }
    }

    void FixedUpdate()
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

        rigid.MovePosition(transform.position + new Vector3(movement.x, movement.y, 0f));
    }
}
