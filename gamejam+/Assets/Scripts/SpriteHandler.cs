using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteHandler : MonoBehaviour
{
    private int player_sprites;  

    // Start is called before the first frame update
    void Start()
    {
        player_sprites = 2;        
    }

    private GameObject GetNewSprite(int edges)
    {
        if (edges - 3 > player_sprites)
            return null;

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
}
