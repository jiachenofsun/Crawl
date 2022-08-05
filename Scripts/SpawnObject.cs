using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public GameObject[] objects;
    void Start()
    {
        int rand = Random.Range(0, objects.Length);
        GameObject tile = (GameObject) Instantiate(objects[rand], transform.position, Quaternion.identity);
        if (tile.tag == "Nothing")
        {
            Destroy(tile);
            Destroy(gameObject);
        } else
        {
            tile.transform.parent = transform;
        }
    }
}
