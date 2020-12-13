using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
[CustomEditor(typeof(TrackPathCreator))]
public class TrackPathCreatorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        TrackPathCreator tpCreator = (TrackPathCreator)target;
        if(GUILayout.Button("Create Track Path"))
        {
            tpCreator.CreateAiTrackPath();
        }
    }
}
#endif
