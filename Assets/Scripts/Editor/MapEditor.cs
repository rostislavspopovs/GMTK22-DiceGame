using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MapEditor : EditorWindow
{

    //Level currentLevel;
    //int currentFloor = 0;

    //[MenuItem("Editors/Map Editor")]
    //public static void ShowWindow()
    //{
    //    EditorWindow.GetWindow(typeof(MapEditor));
    //}

    //private void OnGUI()
    //{
    //    GUILayout.BeginHorizontal();
    //    GUILayout.Label("Level Scriptable Object");
    //    currentLevel = EditorGUILayout.ObjectField("", currentLevel, typeof(Level),true) as Level;

    //    GUILayout.Label("Floor");
    //    currentFloor = EditorGUILayout.IntField(currentFloor);

    //    if(GUILayout.Button("Load Floor"))
    //    {
    //        LoadLevel();
    //    }
    //    GUILayout.EndHorizontal();
    //}

    //private void LoadLevel()
    //{
    //    //GUILayout.Box("Floor " + currentFloor);

    //    Level.Floor floor = new Level.Floor(currentLevel.mapDepth, currentLevel.mapWidth);
    //    if (currentLevel.floors.Count < currentFloor)
    //    {
    //        currentLevel.floors.Add(floor);
    //    }

    //    var off = 2f;
    //    var px = 20f;
    //    var py = 20f;
    //    var size = 10f;

    //    int y = 0;
    //    foreach (int[] row in floor.rows)
    //    {
    //        int x = 0;
    //        GUILayout.BeginHorizontal();
    //        foreach (int b in row)
    //        {
    //            Debug.Log(y + " " + x);
    //            GUILayout.Button(b.ToString());
    //            x++;
    //        }
    //        GUILayout.EndHorizontal();
    //        y++;
    //    }
    //    GUI.EndGroup();
    //}
}
