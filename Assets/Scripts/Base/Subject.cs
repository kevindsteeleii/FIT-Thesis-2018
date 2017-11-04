using System.Collections.Generic;
using UnityEngine;
using System.Collections;

/// <summary>
/// Subject is the Class/Component listened to by the Observer
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class Subject : MonoBehaviour
{
    protected List<Observer> listObservers = new List<Observer>();
    

    /// <summary>
    /// Adds specific observer to subscription list.
    /// </summary>
    /// <param name="observer"></param>
    protected void AddObserver(Observer observer)
    {
        listObservers.Add(observer);
    }

    /// <summary>
    /// Removes specific observer from subscription list.
    /// </summary>
    /// <param name="observer"></param>
    protected void RemoveObserver(Observer observer)
    {
        listObservers.Remove(observer);
    }

    /// <summary>
    /// Removes all Observers from subscription list of Subject
    /// </summary>
    protected void RemoveAll()
    {
        foreach (Observer item in listObservers)
        {
            listObservers.Remove(item);
        }
    }


    public abstract void Notify();
}
