using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomPropertyDrawer(typeof(MinMaxCustomSlider))]
public class MinMaxCustomSliderDrawer : PropertyDrawer
{
	public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
	{
		SerializedProperty showGroup = property.FindPropertyRelative("ShowGroup");
		if(!showGroup.boolValue)
			return 16;
		
		SerializedProperty CompareMethod = property.FindPropertyRelative("CompareMethod");
		if(CompareMethod.enumValueIndex != 2) //!= ExactAngle
		{
			MinMaxCustomSlider slider = (MinMaxCustomSlider)attribute;

			if(slider.PropertyWidth < 250) //use minimalist method
				return 128;
			else
				return 96;
		}
		else
			return 64f;
	}
	
	public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
	{
		EditorGUI.BeginProperty(position, label, property);
		int indent = EditorGUI.indentLevel;

		//positions
		Rect groupPos = new Rect(position.x, position.y, position.width, 16);
		Rect namePos = new Rect(position.x, position.y + 16, position.width, 16);
		Rect comparePos = new Rect(position.x, position.y + 32, position.width, 16);

		//variables
		SerializedProperty PropertyName = property.FindPropertyRelative("Name");
		SerializedProperty MinValue = property.FindPropertyRelative("MinValue");
		SerializedProperty MaxValue = property.FindPropertyRelative("MaxValue");

		SerializedProperty MinLimit = property.FindPropertyRelative("MinLimit");
		SerializedProperty MaxLimit = property.FindPropertyRelative("MaxLimit");

		SerializedProperty ExactValue = property.FindPropertyRelative("ExactValue");

		SerializedProperty CompareMethod = property.FindPropertyRelative("CompareMethod");

		SerializedProperty showGroup = property.FindPropertyRelative("ShowGroup");

		MinMaxCustomSlider slider = (MinMaxCustomSlider)attribute;
		slider.PropertyWidth = position.width;

		//default values
		SetupDefaultValues(MinValue, MaxValue, MinLimit, MaxLimit, PropertyName, ExactValue, showGroup);

		//controls
		showGroup.boolValue = EditorGUI.Foldout(groupPos, showGroup.boolValue, PropertyName.stringValue);

		//works good
//		if(property.isExpanded)
//			showGroup.boolValue = EditorGUI.Foldout(groupPos, showGroup.boolValue, PropertyName.stringValue);
//		else
//			EditorGUI.LabelField(groupPos, PropertyName.stringValue);

		if(showGroup.boolValue)
		{
			EditorGUI.indentLevel++;
			
			PropertyName.stringValue = EditorGUI.TextField(namePos, "Name", PropertyName.stringValue);

//			EditorGUI.HandlePrefixLabel(new Rect(comparePos.x, comparePos.y, 92, comparePos.height), 
//			                            new Rect(comparePos.x, comparePos.y, 92, comparePos.height), 
//			                            new GUIContent("Custom Range:"));
//			EditorGUI.PropertyField(new Rect(comparePos.x + 95, comparePos.y, comparePos.width, comparePos.height), CompareMethod, GUIContent.none);

			EditorGUI.PropertyField(comparePos, CompareMethod);

			if(CompareMethod.enumValueIndex == 0 //ValueRangeCompareMethod.ValueInsideRange)
				|| CompareMethod.enumValueIndex == 1)//ValueRangeCompareMethod.ValueOutsideRange)
			{
				if(position.width < 250)
				{
					//positions
					Rect slidePos = new Rect(position.x, position.y + 48, position.width, 16);

					Rect minPos = new Rect(position.x, position.y + 64, position.width, 16);
					Rect maxPos = new Rect(position.x, position.y + 80, position.width, 16);
					
					Rect minLimitPos = new Rect(position.x, position.y + 96, position.width, 16);
					Rect maxLimitPos = new Rect(position.x, position.y + 112, position.width, 16);

					//controls
					MinValue.floatValue = EditorGUI.FloatField(minPos, new GUIContent("MinValue"), MinValue.floatValue);
					MaxValue.floatValue = EditorGUI.FloatField(maxPos, new GUIContent("MaxValue"), MaxValue.floatValue);

					MinLimit.floatValue = EditorGUI.FloatField(minLimitPos, new GUIContent("MinLimit"), MinLimit.floatValue);
					MaxLimit.floatValue = EditorGUI.FloatField(maxLimitPos, new GUIContent("MaxLimit"), MaxLimit.floatValue);
					
					float minVal = MinValue.floatValue;
					float maxVal = MaxValue.floatValue;
					
					EditorGUI.MinMaxSlider(new GUIContent("Range"),
					                       slidePos,
					                       ref minVal, ref maxVal,
					                       MinLimit.floatValue, MaxLimit.floatValue);
					
					MinValue.floatValue = minVal;
					MaxValue.floatValue = maxVal;
				}
				else
				{
					Rect slidePos = new Rect(position.x + 48, position.y + 48, position.width - 96, 16);

					//positions
					Rect minLimitPos = new Rect(position.x, position.y + 48, 72, 16);
					Rect maxLimitPos = new Rect(position.x + position.width - 72, position.y + 48, 72, 16);

					Rect minPos = new Rect(position.x, position.y + 64 , position.width, 16);
					Rect maxPos = new Rect(position.x, position.y + 80, position.width, 16);

					if(EditorGUI.indentLevel < 2)
					{
						minLimitPos = new Rect(position.x, position.y + 48, 57, 16);
						maxLimitPos = new Rect(position.x + position.width - 57, position.y + 48, 57, 16);
					}
					
					//controls
					MinValue.floatValue = EditorGUI.FloatField(minPos, new GUIContent("MinValue"), MinValue.floatValue);
					MaxValue.floatValue = EditorGUI.FloatField(maxPos, new GUIContent("MaxValue"), MaxValue.floatValue);
					
					MinLimit.floatValue = EditorGUI.FloatField(minLimitPos, GUIContent.none, MinLimit.floatValue);
					MaxLimit.floatValue = EditorGUI.FloatField(maxLimitPos, GUIContent.none, MaxLimit.floatValue);
					
					float minVal = MinValue.floatValue;
					float maxVal = MaxValue.floatValue;
					
					EditorGUI.MinMaxSlider(GUIContent.none,
					                       slidePos,
					                       ref minVal, ref maxVal,
					                       MinLimit.floatValue, MaxLimit.floatValue);
					
					MinValue.floatValue = minVal;
					MaxValue.floatValue = maxVal;
				}
			}
			else //ExactAngle
			{
				Rect exactValPos = new Rect(position.x, position.y + 48, position.width, 16);
				EditorGUI.PropertyField(exactValPos , ExactValue);
			}
		}

		EditorGUI.indentLevel = indent;
		EditorGUI.EndProperty();
	}

	void SetupDefaultValues(SerializedProperty MinValue, SerializedProperty MaxValue, SerializedProperty MinLimit, SerializedProperty MaxLimit, 
	                        SerializedProperty PropertyName, SerializedProperty ExactValue, SerializedProperty showGroup)
	{
		if(MinValue.floatValue == 0 && MaxValue.floatValue == 0
		   && MinLimit.floatValue == 0 && MaxLimit.floatValue == 0)
		{			
			MinValue.floatValue = -45;
			MaxValue.floatValue = 45;
			
			MinLimit.floatValue = -90;
			MaxLimit.floatValue = 90;

			ExactValue.floatValue = 45;

			PropertyName.stringValue = "Value Range";

			showGroup.boolValue = true;
		}
	}

}
