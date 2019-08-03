using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLock : MonoBehaviour
{

    private Transform player;

    private Vector3 topRightCorner, bottomLeftCorner;

    private float width, height;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerMovement>().transform;
        topRightCorner = GameObject.Find("TopRightCorner").transform.position;
        bottomLeftCorner = GameObject.Find("BottomLeftCorner").transform.position;

        Camera cam = Camera.main;
        height = 2f * cam.orthographicSize;
        width = height * cam.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);

        if (newPos.x > topRightCorner.x - width/2 || newPos.x < bottomLeftCorner.x + width/2)
            newPos.x = transform.position.x;

        if (newPos.y > topRightCorner.y - height/2 || newPos.y < bottomLeftCorner.y + height/2)
            newPos.y = transform.position.y;

        this.transform.position = newPos;
    }
}
