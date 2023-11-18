using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad()]
public class WaypointEditor
{
    [DrawGizmo(GizmoType.NonSelected | GizmoType.Selected | GizmoType.Pickable)]
    public static void OnDrawSceneGizmo(WayPoint waypoint,GizmoType gizmoType)
    {
        if((gizmoType & GizmoType.Selected)!=0)
        {
            Gizmos.color = Color.yellow;
        }
        else
        {
            Gizmos.color = Color.yellow * 0.5f;
        }
        Gizmos.DrawSphere(waypoint.transform.position, 0.1f);

        Gizmos.color=Color.white;
        Gizmos.DrawLine(waypoint.transform.position + (waypoint.transform.right * waypoint.width / 2f),
            waypoint.transform.position - (waypoint.transform.right * waypoint.width / 2f));
        
        if(waypoint.previous != null)
        {
            Gizmos.color = Color.red;
            Vector3 offset = waypoint.transform.right * waypoint.width / 2f;
            Vector3 offsetTo = waypoint.previous.transform.right * waypoint.previous.width / 2f;

            Gizmos.DrawLine(waypoint.transform.position+offset,waypoint.previous.transform.position+offsetTo);
        }
        if (waypoint.next != null)
        {
            Gizmos.color = Color.green;
            Vector3 offset = waypoint.transform.right * -waypoint.width / 2f;
            Vector3 offsetTo = waypoint.next.transform.right * -waypoint.next.width / 2f;

            Gizmos.DrawLine(waypoint.transform.position + offset, waypoint.next.transform.position + offsetTo);
        }
    }
    
}
