using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(EnemyBulletTest))]
public class EnemyBulletEditorTest : Editor {

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        EditorGUILayout.BeginHorizontal();
        EnemyBulletTest myTarget = (EnemyBulletTest)target;
        EditorGUILayout.LabelField("Damage",GUILayout.MaxWidth(200),GUILayout.MinWidth(125));
        myTarget.hitPoint = EditorGUILayout.IntSlider(myTarget.hitPoint, 1, 15,GUILayout.MinWidth(125));
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Random Bullet Type: ");
        myTarget.random = EditorGUILayout.Toggle(myTarget.random);
        EditorGUILayout.EndHorizontal();

       if (!myTarget.random)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Toggle for grabbable ammo");
            myTarget.isGrabby = EditorGUILayout.Toggle(myTarget.isGrabby);
            EditorGUILayout.EndHorizontal();
        }

        string message = "";
        message = (myTarget.isGrabby) ? "Ammo is Grabbable" : "Ammo is Harmful only";
        EditorGUILayout.HelpBox(message, MessageType.Info);
    }
}
