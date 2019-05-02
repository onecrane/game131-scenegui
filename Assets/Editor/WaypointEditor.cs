using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Waypoint))]
public class WaypointEditor : Editor
{

    private void OnSceneGUI()
    {
        Event evt = Event.current;

        switch (evt.type)
        {
            case EventType.MouseDrag:
                if (evt.button == 0)
                {
                    if (HandleDrag(evt)) evt.Use();
                }
                break;
            case EventType.MouseUp:
                if (evt.button == 0)
                {
                    if (HandleMouseUp(evt)) evt.Use();
                }
                break;
            case EventType.MouseDown:
                if (evt.button == 0)
                {
                    if (HandleMouseDown(evt)) evt.Use();
                }
                break;
        }
    }

    private bool HandleDrag(Event evt)
    {
        // Move with the mouse:
        // Move to the current mouse position if the current mouse position is on a suitable area. Borrow from previous click handler.
        // Bear in mind that we only want to know about the walkable floor.
        // Probably the simplest way we can implement that is with layers.

        RaycastHit info;
        if (Physics.Raycast(HandleUtility.GUIPointToWorldRay(evt.mousePosition), out info, float.MaxValue, 1 << LayerMask.NameToLayer("WalkableFloor")))
        {
            // Hit the floor; move the selected object (this) to this location
            (target as Waypoint).transform.position = info.point;
            return true;
        }
        return false;
    }

    private bool HandleMouseUp(Event evt)
    {
        return false;
    }

    private bool HandleMouseDown(Event evt)
    {
        return false;
    }

}
