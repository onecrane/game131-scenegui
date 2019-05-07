using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetWaypointNames()
    {
        int ix = 0;
        foreach (Transform waypoint in transform)
        {
            if (waypoint.name.StartsWith("Waypoint"))
            {
                waypoint.name = "Waypoint_" + ix.ToString("0000");
                ix++;
            }
        }
    }

    private void OnDrawGizmos()
    {
        DrawPathGizmos();
    }

    public void DrawPathGizmos()
    {
        Color original = Gizmos.color;
        Gizmos.color = Color.green;
        List<Transform> waypoints = new List<Transform>();
        foreach (Transform waypoint in transform)
        {
            if (waypoint.name.StartsWith("Waypoint"))
            {
                waypoints.Add(waypoint);
            }
        }

        // Now draw paths
        for (int i = 0; i < waypoints.Count; i++)
        {
            Vector3 start = waypoints[i].position;
            Vector3 end = waypoints[(i + 1) < waypoints.Count ? i + 1 : 0].position;

            Gizmos.DrawLine(start, end);
        }
        Gizmos.color = original;
    }
}
