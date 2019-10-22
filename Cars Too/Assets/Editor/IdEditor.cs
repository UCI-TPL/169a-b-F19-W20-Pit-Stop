using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Id),true)]
[CanEditMultipleObjects]
public class IdEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if(GUILayout.Button("Assign IDS"))
        {
            Id[] idobjs = GameObject.FindObjectsOfType<Id>();
            for(int i= 0; i < idobjs.Length; i++)
            {
                idobjs[i].SetID(i);
            }
        }
    }

}
