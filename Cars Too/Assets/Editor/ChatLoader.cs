using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Chatlist))]
public class ChatLoader : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Chatlist cl = (Chatlist)target;

        if (GUILayout.Button("Load Chats"))
        {
            cl.LoadChats();
        }
    }
}
