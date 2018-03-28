using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that handles the logic of a a singular object pool to be used with
/// some sort of manager class that utilizes a management class to be added later
/// </summary>
public class PoolItem : MonoBehaviour
{

    public int inititalAmount = 6;
    GameObject pooledItem;
    int idKey; /*the integer id to be used as the key of a key,value pair 
    in the dictionary entry for the pool item party/manager*/
    List<GameObject> pool = new List<GameObject>();  //used to keep the pooled items in a manageable collection
    Vector3 hidden = new Vector3(99f, 99f, 99f);    // location to hide the inactive parts in hierarchy
    bool pooled = false;    //possibly useless boolean that determines if the poolitem class in question has indeed been pooled before or not

    /// <summary>
    /// Pools initial item in the amount specified
    /// </summary>
    /// <param name="amount"></param>
    /// <param name="obj"></param>
    public PoolItem(int amount, GameObject obj)
    {
        if (!pooled)
        {
            inititalAmount = amount;
            pooledItem = obj;
            idKey = obj.GetInstanceID();
            Populate();
        }
        else
            return;
    }

    public void Populate()
    {
        for (int i = 0; i < inititalAmount; i++)
        {
            GameObject obj = Instantiate(pooledItem);
            obj.SetActive(false);
            pool.Add(obj);
        }
        pooled = true;
    }

    public GameObject Get(Vector3 pos)
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                pool[i].SetActive(true);
                pool[i].transform.position = pos;
                return pool[i];
            }
        }
        return null;
    }

    /// <summary>
    /// Set an active game object to false to be reused later
    /// </summary>
    public void ReUse()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (pool[i].activeInHierarchy)
            {
                pool[i].SetActive(false);
            }
        }
    }

    /// <summary>
    /// Add additional items after instantiation of PoolItem class
    /// </summary>
    /// <param name="amount"></param>
    public void AddItems(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            pool.Add(pooledItem);
        }
    }

    /// <summary>
    /// Returns the integer that works as the key for the
    /// dictionary in a potential pool item pooler class
    /// </summary>
    /// <returns></returns>
    public int ReturnIdKey()
    {
        return idKey;
    }

    public int GetAmount()
    {
        return pool.Count;
    }

}
