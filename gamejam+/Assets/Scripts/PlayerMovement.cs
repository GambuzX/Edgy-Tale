using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float movementSpeed = 0.05f, rotateSpeed = 2f;
    private Vector2 movementDirection;
    private float vertical, horizontal;
    private float rotate;

    private Vector3 topRightCorner, bottomLeftCorner;

    void Start()
    {
        topRightCorner = GameObject.Find("TopRightCorner").transform.position;
        bottomLeftCorner = GameObject.Find("BottomLeftCorner").transform.position;
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Transform body = this.transform.GetComponentInChildren<PolyShooter>().transform;
        rotate = Input.GetAxis("Rotate");
        if(rotate != 0)
        {
            body.RotateAround(body.transform.parent.position, Vector3.forward, rotate*rotateSpeed);

            //body.Rotate(new Vector3(0, 0, 1), rotate * rotateSpeed);
        }

        movementDirection = new Vector2(horizontal, vertical) * movementSpeed;

        //this.gameObject.transform.Translate(movementDirection);
        Vector3 newPos = this.transform.position + new Vector3(movementDirection.x, movementDirection.y, 0);

        if (newPos.x > topRightCorner.x || newPos.x < bottomLeftCorner.x)
            newPos.x = transform.position.x;

        if (newPos.y > topRightCorner.y || newPos.y < bottomLeftCorner.y)
            newPos.y = transform.position.y;


        this.transform.position = newPos;
    }
}