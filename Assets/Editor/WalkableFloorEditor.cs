using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PathControl))]
public class WalkableFloorEditor : Editor
{
    private static Tool lastTool = Tool.None;

    public static void StoreTool()
    {
        lastTool = Tools.current;
        Tools.current = Tool.View;  // Only one we can eat left mouse drag with
    }

    public static void RestoreTool()
    {
        Tools.current = lastTool;
        lastTool = Tool.None;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.LabelField("Hello");
    }

    private void OnSceneGUI()
    {
        // Handle mouse clicks
        Event evt = Event.current;

        switch (evt.type)
        {
            case EventType.MouseDown:
                if (evt.button == 0)
                {
                    if (HandleMouseDown(evt.mousePosition)) evt.Use();
                }
                break;
            case EventType.MouseUp:
                if (evt.button == 0)
                {
                    RestoreTool();
                }
                break;
        }
    }

    bool HandleMouseDown(Vector2 mousePosition)
    {
        // Did I get the path or a waypoint?
        // Create first.

        // Find what you clicked on:
        RaycastHit info;
        if (Physics.Raycast(HandleUtility.GUIPointToWorldRay(mousePosition), out info ))
        {
            if (info.collider.gameObject.GetComponent<WalkableFloor>() != null)
            {
                // Hit the floor; create a Waypoint
                GameObject waypointTemplate = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Waypoint.prefab", typeof(GameObject)) as GameObject;                
                GameObject newWaypoint = PrefabUtility.InstantiatePrefab(waypointTemplate) as GameObject;
                newWaypoint.transform.position = info.point;
                newWaypoint.transform.parent = (this.target as PathControl).transform;
                (this.target as PathControl).ResetWaypointNames();
                

                // Cheat!
                StoreTool();

                GUIUtility.hotControl = GUIUtility.GetControlID(FocusType.Passive);

                // Select it right away
                Selection.activeObject = newWaypoint;

                return true;
            }

            if (info.transform.parent != null && info.transform.parent.gameObject.tag == "Waypoint")
            {
                // Select that waypoint
                Selection.activeObject = info.transform.parent.gameObject;
                // Cheat!
                StoreTool();
                return true;
            }

        }
        return false;

    }

}
