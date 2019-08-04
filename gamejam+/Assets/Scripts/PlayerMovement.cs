using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private bool edgyTransformation = false;

    public float movementSpeed = 0.02f, rotateSpeed = 100f;
    public float edgeDistance = 1f;
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

        movementDirection = new Vector2(horizontal, vertical) * movementSpeed * Time.deltaTime * 60;

        Vector3 newPos = this.transform.position + new Vector3(movementDirection.x, movementDirection.y, 0);

        if (newPos.x > topRightCorner.x - edgeDistance || newPos.x < bottomLeftCorner.x + edgeDistance)
            newPos.x = transform.position.x;

        if (newPos.y > topRightCorner.y - edgeDistance || newPos.y < bottomLeftCorner.y + edgeDistance)
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
