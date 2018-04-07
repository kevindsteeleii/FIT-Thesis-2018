using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// A singleton that automatically gathers pool items on creation
/// </summary>
/// 
[Serializable]
public class PoolManager : Singleton<PoolManager> {

    public Dictionary<int, PoolItem> ManagerOfPools = new Dictionary<int, PoolItem>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// Method that listens for the pool item to be created
    /// </summary>
    /// <param name="key"></param>
    /// <param name="poolItem"></param>
    //public void On_PoolItemAttempt_Received(int key, PoolItem poolItem )
    //{
    //    Debug.Log("poolItemAttempt receieved");
    //        if (ManagerOfPools.ContainsKey(key))
    //    {
    //        ManagerOfPools[key].SetParentObject(ManagerOfPools[key].GetParent());
    //        ManagerOfPools[key].AddItems(poolItem.inititalAmount);
    //    }
    //    else
    //    {
    //        string name = poolItem.gameObject.name +"_Pool Manager";
    //        GameObject ParentObj = new GameObject(name);
    //        poolItem.SetParentObject(ParentObj);
    //        ManagerOfPools.Add(key, poolItem);
    //    }
    //}

    //public void DoesPoolItemExist(PoolItem item, int amount)
    //{
    //    Debug.Log("DoesPoolItemExist?");
    //    int key = item.GetInstanceID();
    //    string parentName = item.name + " Pooler";  //name to be assigned to the child of poolManager to house the poolitems

    //    if (ManagerOfPools.ContainsKey(key))
    //    {
    //        ManagerOfPools[key].AddItems(amount);
    //    }
    //    else
    //    {
    //        ManagerOfPools.Add(key, item);
    //        //item.SetParentObject(gameObject);
    //    }
    //}

    /// <summary>
    /// Used to provide the gameobject the singleton belongs to
    /// </summary>
    /// <returns></returns>
    public GameObject ParentOf()
    {
        return gameObject;
    }
}
#region TODO list, refactoring etc
/****************************************TODO************************************************
  1-Create logic that runs at start that determines where to put different items based on 
  object instance ID
  2- Create logic/implement a sub GameObject named GameObjectName+ "poolManger" or something
  like that
  3- Assign sub GameObject as parent of matching pooled items
  4- add logic that checks against new items being formed
  5-
  6-
****************************************TODO************************************************/
#endregion