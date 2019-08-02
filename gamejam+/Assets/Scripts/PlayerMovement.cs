using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed, rotateSpeed;
    private Vector2 movementDirection;
    private float vertical, horizontal;
    private float rotate;

    private void Start()
    {
        if (movementSpeed == 0)
            movementSpeed = 0.05f;
        if (rotateSpeed == 0)
            rotateSpeed = 2;
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Transform body = this.transform.GetChild(0);
        rotate = Input.GetAxis("Rotate");
        if(rotate != 0)
        {
            body.RotateAround(body.transform.parent.position, Vector3.forward, rotate*rotateSpeed);

            //body.Rotate(new Vector3(0, 0, 1), rotate * rotateSpeed);
        }

        movementDirection = new Vector2(horizontal, vertical) * movementSpeed;

        this.gameObject.transform.Translate(movementDirection);
    }
}