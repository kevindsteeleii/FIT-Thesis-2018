using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Makes the enemy stationary when inside of an appropriate trigger collider
/// </summary>
public class TrashCannon_StationaryLogic : MonoBehaviour {
    public EnemyVisionDetection stationaryDetector;
    public event Action<int> On_StationaryAlert_Sent;   //makes enemy stationary pending certain conditions
	// Use this for initialization
	void Start () {
        if (stationaryDetector != null)
        {
            return;
        }
        else
        {
            stationaryDetector = gameObject.transform.parent.gameObject.GetComponentInChildren<EnemyVisionDetection>();
        }

        On_StationaryAlert_Sent += stationaryDetector.On_ProximityAlert_Received;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(String.Format("{0} tagged object's collider has been entered",other.gameObject.tag));
        if (other.gameObject.tag == "Stationary")
        {
            On_StationaryAlert_Sent(0);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Stationary")
        {
            On_StationaryAlert_Sent(0);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Stationary")
        {
            On_StationaryAlert_Sent(1);
        }
    }
}