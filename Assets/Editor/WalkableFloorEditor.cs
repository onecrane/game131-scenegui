using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WalkableFloor))]
public class WalkableFloorEditor : Editor
{

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
            case EventType.KeyDown:
                Debug.Log("Key down: " + evt.keyCode);
                break;
            case EventType.KeyUp:
                Debug.Log("Key up: " + evt.keyCode);
                break;
        }
    }

    bool HandleMouseDown(Vector2 mousePosition)
    {
        // Did I get the path or a waypoint?
        // Create first.

        // Find what you clicked on:
        RaycastHit info;
        if (Physics.Raycast(HandleUtility.GUIPointToWorldRay(mousePosition),out info ))
        {
            if (info.collider.gameObject.GetComponent<WalkableFloor>() == this.target as WalkableFloor)
            {
                // Hit the floor; create a Waypoint
                GameObject waypointTemplate = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Waypoint.prefab", typeof(GameObject)) as GameObject;
                GameObject newWaypoint = GameObject.Instantiate(waypointTemplate);
                newWaypoint.transform.position = info.point;

                return true;
            }

            if (info.transform.parent != null && info.transform.parent.gameObject.tag == "Waypoint")
            {
                Debug.Log("Clicked a waypoint");
                // Select that waypoint
                Selection.activeObject = info.transform.parent.gameObject;
                return true;
            }
            Debug.Log("Clicked something with tag " + info.collider.gameObject.tag);

        }
        return false;

    }

}
