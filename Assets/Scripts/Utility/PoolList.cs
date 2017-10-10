using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolList : MonoBehaviour  {


    private int defaultPoolSize = 10;
    private GameObject pooledObject;
    List<GameObject> poolObjectList;

    Vector3 hidden = new Vector3(1000f, 1000f, 1000f);

    // Use this for initialization, creates list as new on start and populates the list
    public void Populate (int size, GameObject poolObject)
    {
        poolObjectList = new List<GameObject>();
        for (int i = 0; i < defaultPoolSize; i++)
        {
            AddItem();
        }
    }

    /// <summary>
    /// Gets last member in pool setting it to active. Needs to be relocated though.
    /// </summary>
    /// <returns></returns>
    public GameObject GetPooledObject()
    {
        for (int i = 0; i < poolObjectList.Count; i++)
        {
            if (!poolObjectList[i].activeInHierarchy)
            {
                poolObjectList[i].SetActive(true);
                return poolObjectList[i];
            }
        }
        //GameObject obj = AddItem();
        //poolObjectList.Add(obj);
        //return obj;
        return null;
        throw new System.Exception("Pool is empty bro! Come back later.");
    }

    //Adds, setsActive false, instantiates ,hides, and finally returns item
    GameObject AddItem()
    {
        GameObject obj = Instantiate(pooledObject);
        obj.SetActive(false);
        obj.transform.position = hidden;
        poolObjectList.Add(obj);
        return obj;
    }
}
