using System;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(KeyInputManager))]
public class KeyInputManagerEditor : Editor {
	private ReorderableList list;

	private void OnEnable() {
		list = new ReorderableList(serializedObject, 
			serializedObject.FindProperty("keyCodes"), 
			true, true, true, true);
		
		list.drawElementCallback =  
			(Rect rect, int index, bool isActive, bool isFocused) => {
			var element = list.serializedProperty.GetArrayElementAtIndex(index);
			rect.y += 2;
			EditorGUI.PropertyField(
				new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight),
				element, GUIContent.none);
		};

		list.drawHeaderCallback = (Rect rect) => {  
			EditorGUI.LabelField(rect, "Key Codes");
		};

		list.onAddDropdownCallback = (Rect buttonRect, ReorderableList l) => {  
			var menu = new GenericMenu();
			string[] keyCodes = Enum.GetNames(typeof(KeyCode));

			for (int i = 0; i < keyCodes.Length; i++) {
				menu.AddItem(new GUIContent(keyCodes[i]), false, clickHandler, i);
			}
			foreach (var item in Enum.GetValues(typeof(KeyCode))) {

			}

			menu.ShowAsContext();
		};
	}

	public override void OnInspectorGUI() {
		serializedObject.Update();
		list.DoLayoutList();
		serializedObject.ApplyModifiedProperties();
	}

	private void clickHandler(object target) {  
		var data = (int)target;
		var index = list.serializedProperty.arraySize;
		list.serializedProperty.arraySize++;
		list.index = index;
		var element = list.serializedProperty.GetArrayElementAtIndex(index);
		element.enumValueIndex = data;
		serializedObject.ApplyModifiedProperties();
	}
}
