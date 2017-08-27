using UnityEngine;
using System.Collections;

public enum ValueRangeCompareMethod
{
	ValueInsideRange,
	ValueOutsideRange,
	ExactValue
}

[System.Serializable]
public class ValueRange : System.Object
{
	public string Name = "Value Range";
	
	public float MinValue = -45;
	public float MaxValue = 45;
	
	public float MinLimit = -90;
	public float MaxLimit = 90;
	
	public float ExactValue = 45;
	
	public ValueRangeCompareMethod CompareMethod;
	
	//We need to keep individual track of expanded groups since attributes holds it for the entire list
	[HideInInspector]
	public bool ShowGroup = true;
	
	public bool IsValid(float value)
	{
		if(CompareMethod == ValueRangeCompareMethod.ValueInsideRange)
		{
			return MinValue <= value && value <= MaxValue;
		}
		else if(CompareMethod == ValueRangeCompareMethod.ValueOutsideRange)
		{
			return value < MinValue || MaxValue < value;				 
		}
		else
		{
			return value == ExactValue;
		}
	}
	
	public bool IsValidAngle(float value)
	{
		if(CompareMethod == ValueRangeCompareMethod.ValueInsideRange)
		{
			value = Mathf.DeltaAngle(0, value);
		
			return MinValue <= value && value <= MaxValue;
		}
		else if(CompareMethod == ValueRangeCompareMethod.ValueOutsideRange)
		{
			value = Mathf.DeltaAngle(0, value);
		
			return value < MinValue || MaxValue < value;				 
		}
		else
		{
			return value == ExactValue;
		}
	}
}