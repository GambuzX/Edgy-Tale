using System.Collections;
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
            new_object.transform.position = this.transform.position;
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
