using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSprite : MonoBehaviour
{
    public List<GameObject> player_sprites;
    private int number_of_edges;

    public AudioClip soundEffect;

    private AudioSource soundSource;

    // Start is called before the first frame update
    void Start()
    {
        soundSource = GetComponent<AudioSource>();
        soundSource.clip = soundEffect;
        for (int i = 0; i < player_sprites.Count; i++)
        {
            string polygon_name = Polygon.GetName(i + 3);
            player_sprites[i] = Resources.Load<GameObject>(polygon_name);
            player_sprites[i].name = polygon_name;
        }
        number_of_edges = 3;
    }

    private GameObject GetNewSprite(int edges)
    {
        if (edges - 3 > player_sprites.Count)
            return null;

        return player_sprites[edges-3];
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > 10 && number_of_edges < 4)
        {
            string object_name = Polygon.GetName(number_of_edges);
            GameObject new_object = GetNewSprite(++number_of_edges);
            if(new_object != null)
            {
                Destroy(this.transform.Find(object_name).gameObject);
                new_object = Instantiate(new_object);
                new_object.name = Polygon.GetName(number_of_edges);
                new_object.transform.parent = this.transform;
                new_object.transform.position = this.transform.position;
                new_object.transform.rotation = this.transform.rotation;
                soundSource.Play();
            }           
        }
    }
}
