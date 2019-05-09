using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkPath : MonoBehaviour
{

    public PathControl pathToWalk;
    private int waypointIndex = 0;
    private KinematicSeek kinematicSeek;

    void Start()
    {
        kinematicSeek = GetComponent<KinematicSeek>();

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
}
