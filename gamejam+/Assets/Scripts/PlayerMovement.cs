using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private bool edgyTransformation = false;

    public float movementSpeed = 0.05f, rotateSpeed = 100f;
    private Vector2 movementDirection;
    private float vertical, horizontal;
    private float rotate;

    private Vector3 topRightCorner, bottomLeftCorner;

    private Transform body;

    void Start()
    {
        topRightCorner = GameObject.Find("TopRightCorner").transform.position;
        bottomLeftCorner = GameObject.Find("BottomLeftCorner").transform.position;

        body = this.transform.GetComponentInChildren<Rigidbody2D>().transform;
    }

    void Update()
    {

        if (body == null)
            body = this.transform.GetComponentInChildren<Rigidbody2D>().transform;

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        
        rotate = Input.GetAxis("Rotate");

        if(rotate != 0)
        {
            this.rotatePlayer(rotate * rotateSpeed);
        }

        if (edgyTransformation)
        {
            this.rotatePlayer(10 * rotateSpeed);
        }

        movementDirection = new Vector2(horizontal, vertical) * movementSpeed;

        Vector3 newPos = this.transform.position + new Vector3(movementDirection.x, movementDirection.y, 0);

        if (newPos.x > topRightCorner.x || newPos.x < bottomLeftCorner.x)
            newPos.x = transform.position.x;

        if (newPos.y > topRightCorner.y || newPos.y < bottomLeftCorner.y)
            newPos.y = transform.position.y;


        this.transform.position = newPos;
    }

    public void toggleEdgyTransformation()
    {
        edgyTransformation = !edgyTransformation;
    }

    public void rotatePlayer(float angle)
    {
        body.RotateAround(body.transform.parent.position, Vector3.forward, angle * Time.deltaTime);
    }
}
