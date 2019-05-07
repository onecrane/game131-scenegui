using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Waypoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmosSelected()
    {
        // Draw gizmos to all waypoints in order;
        // that means we need something that controls the order of the waypoints, yes yes?
        // So we do need to create a path.
        // 
        // Draw from the parent

        transform.parent.GetComponent<PathControl>().DrawPathGizmos();
    }

    private void OnDestroy()
    {
        transform.parent.GetComponent<PathControl>().ResetWaypointNames();
    }
}
