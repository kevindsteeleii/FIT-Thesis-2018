using System;
using System.Collections.Generic;
using UnityEngine;

public class KillabeeAnimatorScript : MonoBehaviour {
    public event Action On_Killabee_Shot;

    void KillabeeFire()
    {
        Debug.Log("Killabee Firing");
        if (On_Killabee_Shot != null)
        {
            On_Killabee_Shot();
        }
    }
}
