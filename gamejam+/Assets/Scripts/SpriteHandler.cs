﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteHandler : MonoBehaviour
{
    private GameObject GetNewSprite(int edges)
    {
        return Resources.Load<GameObject>(Polygon.GetName(edges));
    }

    public void changeSprite(int number_of_edges)
    {
        GameObject new_object = GetNewSprite(number_of_edges);
        if (new_object != null)
        {
            Destroy(GameObject.FindObjectOfType<PolyShooter>().gameObject);
            new_object = Instantiate(new_object);
            new_object.name = Polygon.GetName(number_of_edges);
            new_object.transform.parent = this.transform;
            float x = 0, y = 0;
            int number_vertices = 0;
            foreach(Transform transform in new_object.transform)
            {
                x += transform.position.x;
                y += transform.position.y;
                number_vertices++;
            }
            new_object.transform.position = new Vector3(this.transform.position.x - x / number_vertices, this.transform.position.y - y / number_vertices, 0);
            new_object.transform.rotation = this.transform.rotation;
        }
    }

    public GameObject GetNewEnemy(int edges)
    {
        int n_edges = Random.Range(3, edges+1);















        return Resources.Load<GameObject>(Polygon.GetEnemyName(n_edges));
    }

    public GameObject GetGirlfriend()
    {
        return Resources.Load<GameObject>("Girlfriend");
    }
}
