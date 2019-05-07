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

    private void OnDrawGizmosSelected()
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

            // Detect if there's anything in the way
            RaycastHit hitInfo;
            if (Physics.Raycast(start + Vector3.up * 0.2f, end - start, out hitInfo))
            {
                // Green if we hit where we expect
                if (hitInfo.collider.transform.parent != null && hitInfo.collider.transform.parent.position == end)
                    Gizmos.color = Color.green;
                else
                {
                    Gizmos.color = Color.red;
                }
            }

            Gizmos.DrawLine(start, end);
        }
        Gizmos.color = original;
    }
}
