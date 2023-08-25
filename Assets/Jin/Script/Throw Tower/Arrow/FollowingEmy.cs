using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingEmy : MonoBehaviour
{
    //[SerializeField]
    //private float lifeTime = 2.0f;

    //public string spawnTag = "Enemy";

    //public void Spawn()
    //{
    //    GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(spawnTag);

    //    if (taggedObjects.Length > 0)
    //    {
    //        GameObject objectToSpawn = taggedObjects[Random.Range(0, taggedObjects.Length)];
    //        GameObject spawnedObject = Instantiate(objectToSpawn, transform.position, Quaternion.identity);
    //        Destroy(spawnedObject, lifeTime);
    //    }
    //    else
    //    {
    //        Debug.LogWarning("No objects found with the specified tag.");
    //    }
    //}




    [SerializeField]
    private float lifeTime = 2.0f;

    public GameObject ToSpawn = null;

    public void ChangeGameobject(GameObject obj)
    {
        ToSpawn = obj;
    }

    public void Spawn()
    {

        GameObject ga = Instantiate(ToSpawn, transform.position, Quaternion.identity);
        Destroy(ga, lifeTime);
    }
}