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

    public void AddEntry(int entryAmount, GameObject entry)
    {
        //checks if entry has copies within pool and
        //adds to their number either way as a new entry or
        //adding to what's there
        int idNum = entry.GetInstanceID();
        //GameObject local = entry;
        if (!objectsPooled.ContainsKey(idNum))
        {
            objectsPooled.Add(idNum, new Queue<GameObject>());
        }

        for (int i = 0; i < entryAmount; i++)
        {
            GameObject tempObj = Instantiate (entry) as GameObject;
            Put(tempObj, hidden, Quaternion.identity);
        }
    }

    /// <summary>
    /// Returns the GameObject that's not active in hierarchy.
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <returns></returns>
    public GameObject Get(GameObject obj, Vector3 position, Quaternion rotation)
    {
        GameObject tempObj;

        int idKey = obj.GetInstanceID();
        if (objectsPooled.ContainsKey(idKey) && !obj.activeInHierarchy)
        {
            tempObj = objectsPooled [idKey].Dequeue();
            tempObj.SetActive(true);
            tempObj.transform.position = position;
            tempObj.transform.rotation = rotation;
            return tempObj;
        }

        else
            return null;
    }

    //puts object into corresponding queue as identified by the key
    public void Put(GameObject obj, Vector3 position, Quaternion rotation)
    {
        int idKey = obj.GetInstanceID();
        if (objectsPooled.ContainsKey(idKey) && obj.activeInHierarchy)
        {
            obj.SetActive(false);
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            objectsPooled [idKey].Enqueue(obj);
        }
    }
}