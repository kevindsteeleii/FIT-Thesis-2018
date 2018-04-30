using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// class used to pass the Enemy script on the root level of any enemy type
/// </summary>
public class EnemyRecognition : MonoBehaviour {
    public Enemy myEnemy;
	
    public Enemy EnemyHere()
    {
        return myEnemy;
    }
}
