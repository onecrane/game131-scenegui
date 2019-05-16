using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkPath : MonoBehaviour
{

    public PathControl pathToWalk;
    public bool traverseBackward = false;
    private int waypointIndex = 0;
    private KinematicSeek kinematicSeek;

    void Start()
    {
        kinematicSeek = GetComponent<KinematicSeek>();
        RefreshWaypointIndex();
    }

    void RefreshWaypointIndex()
    {
        List<Vector3> waypointLocations = pathToWalk.Waypoints;
        for (int i = 1; i < waypointLocations.Count; i++)
        {
            if (Vector3.Distance(transform.position, waypointLocations[i]) < Vector3.Distance(transform.position, waypointLocations[waypointIndex]))
            {
                waypointIndex = i;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (kinematicSeek.IsAtTarget) waypointIndex = (waypointIndex + 1) % pathToWalk.Waypoints.Count;
        kinematicSeek.destination = pathToWalk.Waypoints[waypointIndex];
    }

    private void OnDrawGizmosSelected()
    {
        if (pathToWalk == null) return;

        pathToWalk.DrawPathGizmos(traverseBackward);

        pathToWalk.RefreshWaypointLocations();
        RefreshWaypointIndex();

        Color lastColor = Gizmos.color;
        Gizmos.color = Color.blue;

        Gizmos.DrawWireSphere(pathToWalk.Waypoints[waypointIndex], 0.3f);

        //Gizmos.DrawWireMesh(pathToWalk.transform.Find("Waypoint_" + waypointIndex.ToString("0000")).GetChild(0).GetComponent<MeshFilter>().sharedMesh);

        Gizmos.color = lastColor;

    }
}
