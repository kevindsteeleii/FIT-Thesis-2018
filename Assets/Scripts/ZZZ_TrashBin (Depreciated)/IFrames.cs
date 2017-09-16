using System.Collections;
using UnityEngine;

/// <summary>
/// Component responsible for IFrames upon damage taken
/// </summary>
public class IFrames : MonoBehaviour {

    /*wait time is length of single wait b/n visible/invisible and duration 
    is total lenght of time*/

 //   static float myTime;
 //   bool invincible;



 //   StatsData data;



    

 //   void Awake ()
 //   {
 //       invincible = false;
 //       camera = this.GetComponent<Camera>();

	//}
	
	//// Update is called once per frame
	//void Update () {


	//}
    
 //   //culls the layer that holds player from game render view
 //   void CullOn()
 //   {
 //       camera.cullingMask = 2^9-1;
 //   }
 //   //reveals the layer that holds the player in game render view
 //   void CullOff() {
 //       camera.cullingMask = 2^10 - 1;
 //   }

 //   IEnumerator IFramez()
 //   {
 //       for (int i = 0; i < intervals; i++)
 //       {
 //           CullOn();
 //           Debug.Log("Culling");
 //           yield return new WaitForSeconds(data.waitTime);
 //           CullOff();
 //           yield return new WaitForSeconds(data.waitTime);
 //           Debug.Log("Not Culling");
 //       }
 //       yield return null;
 //   }
}
