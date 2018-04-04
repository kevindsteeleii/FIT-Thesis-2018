using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// this class is never in and of itself used, it is a Manger
/// used by others, any class using this object Pooler
/// needs a public or serializable variable type GameObject
/// </summary>
public class ObjectPooler : Singleton<ObjectPooler>
{
    /*Creates implementation of object pool 
    using a dictionary of instanceID as int and Queue pair */

    Dictionary<int, Queue<GameObject>> objectsPooled =
    new Dictionary<int, Queue<GameObject>>();

    Vector3 hidden = new Vector3(1000f, 1000f, 1000f);

    /// <summary>
    /// Initial load and instantiation of object pooled items
    /// </summary>
    /// <param name="entryAmount"></param>
    /// <param name="entry"></param>
    public void AddEntry(int entryAmount, GameObject entry)
    {
        //checks if entry has copies within pool and
        //adds to their number either way as a new entry or
        //adding to what's there
        int idNum = entry.GetInstanceID();
 
        if (!objectsPooled.ContainsKey(idNum))
        {
            objectsPooled.Add(idNum, new Queue<GameObject>());
        }

        for (int i = 0; i < entryAmount; i++)
        {
            GameObject tempObj = Instantiate (entry) as GameObject;
            tempObj.transform.position = hidden;
            tempObj.SetActive(false);
            Debug.Log("Loaded " + (i + 1) + " bullets");
        }
    }

    /// <summary>
    /// Returns the GameObject that's not active in hierarchy.
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <returns></returns>
    public GameObject Get(GameObject obj, Vector3 position)
    {
        int idKey = obj.GetInstanceID();
        if (objectsPooled.ContainsKey(idKey))
        {
            foreach (var item in objectsPooled[idKey]) //(int i = 0; i < objectsPooled[idKey].Count; i++)
            {
                if (!item.activeInHierarchy)
                {
                    item.SetActive(true);
                    item.transform.position = position;
                    return item;
                }
            }
        }
            return null;
    }

    //puts object into corresponding queue as identified by the key
    public void Put(GameObject obj, Vector3 position, Quaternion rotation)
    {
        Debug.Log("Putting away hidden bullets");
        int idKey = obj.GetInstanceID();
        if (objectsPooled.ContainsKey(idKey) && obj.activeInHierarchy)
        {
            obj.SetActive(false);
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            objectsPooled [idKey].Enqueue(obj);
        }
    }

    /// <summary>
    /// Simply puts the gameobject that is active in hierarchy into hiding and sets it inactive
    /// </summary>
    /// <param name="obj"></param>
    public void Put(GameObject obj)
    {
        int idKey = obj.GetInstanceID();
        if (objectsPooled.ContainsKey(idKey) && obj.activeInHierarchy)
        {
            obj.SetActive(false);
            obj.transform.position = hidden;
            objectsPooled[idKey].Enqueue(obj);
        }
    }
}