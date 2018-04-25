using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that handles the logic of a a singular object pool to be used with
/// some sort of manager class that utilizes a management class to be added later
/// </summary>
public class PoolItem 
{
    public int inititalAmount = 6;
    GameObject pooledItem;
    int idKey; /*the integer id to be used as the key of a key,value pair 
    in the dictionary entry for the pool item party/manager*/
    List<GameObject> pool = new List<GameObject>();  //used to keep the pooled items in a manageable collection
    Vector3 hidden = new Vector3(99f, 99f, 99f);    // location to hide the inactive parts in hierarchy
    GameObject parentGameObject;

    /// <summary>
    /// Pools initial item in the amount specified
    /// </summary>
    /// <param name="amount"></param>
    /// <param name="obj"></param>
    public PoolItem(int amount, GameObject obj)
    {
        Debug.Log(String.Format("There are {0} of {1} being created", amount, obj));
        idKey = obj.GetInstanceID();
        inititalAmount = amount;
        pooledItem = obj;
        Populate();
    }

    public void Populate()
    {
        for (int i = 0; i < inititalAmount; i++)
        {
            GameObject obj = UnityEngine.Object.Instantiate(pooledItem, hidden, Quaternion.identity,PoolManager.instance.ParentOf().transform);
            obj.SetActive(false);
            pool.Add(obj);
        }

        parentGameObject = pool[0].gameObject.transform.parent.gameObject;
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

    public int Count()  //named the same as the version used for 
    {
        return pool.Count;
    }
    
    public bool IsEmpty()
    {
        bool empty;
        return empty = (Count() <= 0) ? true : false;
    }

    public List<GameObject> GetList()
    {
        return pool;
    }

    public GameObject GetAtIndex (int index)
    {
        return pool[index];
    }

    public GameObject GetParent()
    {
        return parentGameObject;
    }
}
#region TODO list, refactoring etc
/****************************************TODO************************************************
  1-comment out and then refactor what does not need to be in here
  2-implement this for other pooled game objects once the poolmanagers
  sub-gameObject generator logic works 
  3-
  4-
  5-
  6-
****************************************TODO************************************************/
#endregion