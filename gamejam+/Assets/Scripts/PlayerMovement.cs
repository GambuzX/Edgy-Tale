using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float movementSpeed;
    private Vector2 movementDirection;
    private float vertical, horizontal;


    private void Start()
    {

    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        movementDirection = new Vector2(horizontal, vertical) * movementSpeed;

        this.gameObject.transform.Translate(movementDirection);
    }
}