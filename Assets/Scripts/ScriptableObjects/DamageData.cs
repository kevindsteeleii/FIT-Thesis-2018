using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Currently empty class to contain just the values for different types of dammage
/// </summary>
public class DamageData : MonoBehaviour {
    //damage type for copy-pasting different types of damage
    [Tooltip("Damage taken from")]
    [Range(0, 100)]
    public int _Damage;
	
}
