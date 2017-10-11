using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;

//T is generic type that will be used later 
public class Pool<T> : Singleton<Pool<T>> where T: MonoBehaviour
{
    public int poolStartSize  = 12;
    public readonly Vector3 hidden = new Vector3(1000, 1000, 1000);
    protected List<T> pool = new List<T>();
    protected Func<T> create;

    public T pull
    {
        get
        {
            T toReturn = pool.FirstOrDefault(x => x.transform.position == hidden);

            if (!toReturn)
            {
                //create a new instance of T
                toReturn = create();
                //add the new instance of T to your pool 
                pool.Add(toReturn);
            }

            //change the queryable value so that in multiple searches
            //this time doesn't show up more than once
            toReturn.transform.position = Vector3.zero;
            // set this  item to be active...
            toReturn.gameObject.SetActive(true);
            //return the item to user
            return toReturn;
        }
    }

    public void Put (T item)
    {
        //disable item
        item.gameObject.SetActive(false);

        //set the queryable value  to be found again in the 'get' search
        item.transform.position = hidden;
    }

    public void Fill ( Func<T> create)
    {
        this.create = create;
        for (int i = 0; i < poolStartSize; i++)
        {
            T item = create();
            item.gameObject.SetActive(false);
            item.transform.position = hidden;
            pool.Add(item);
        }
    }
}