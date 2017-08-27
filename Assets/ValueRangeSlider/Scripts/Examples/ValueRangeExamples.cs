using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ValueRangeExamples : MonoBehaviour
{
	//different usage examples	
	
	[Header("Visibility example")]
	public float currentViewAngleOffset = 0;	
	
	[MinMaxCustomSlider]
	public ValueRange VisibilityZone;
	
	[Header("Valid attack angle example")]
	public float currentTargetAngleOffset = 0;
	
	[MinMaxCustomSlider]
	public List<ValueRange> ValidAttackAngles;


	[Header("Change Gear Zones example")]
	public float currentSpeed = 120;
	
	[MinMaxCustomSlider]
	public ValueRange[] GearChangeThresholds;
	
	void OnEnable()
	{
		bool validRange = false;
	
		//visibility zone example
		if(VisibilityZone.IsValidAngle(currentViewAngleOffset))
			Debug.Log("Character can see the target!");
		else
			Debug.Log("Character cannot see the target.");

		//angle range example
		foreach (ValueRange valueRange in ValidAttackAngles)
		{
			if(valueRange.IsValidAngle(currentTargetAngleOffset))
			{
				Debug.Log("Valid character attack angle to target!");
				validRange = true;
				break;
			}
		}
		
		if(validRange == false)
			Debug.Log("Character is not in valid attack angle to target.");
			
		validRange = false;
		
		//min max range example
		foreach (ValueRange valueRange in GearChangeThresholds)
		{
			if(valueRange.IsValid(currentSpeed))
			{
				Debug.Log("Perfect Gear change!");
				validRange = true;
				break;
			}
		}
		
		if(validRange == false)
			Debug.Log("Gear change was made outside perfect zone.");
	}
}
