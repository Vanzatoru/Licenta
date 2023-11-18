using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad()]
public class IntersectionpointEditor
{
    [DrawGizmo(GizmoType.NonSelected | GizmoType.Selected | GizmoType.Pickable)]
    public static void OnDrawSceneGizmo(IntersectionPoint intersectionPoint,   GizmoType gizmoType)
    {
        if ((gizmoType & GizmoType.Selected) != 0)
        {
            Gizmos.color = Color.blue;
        }
        else
        {
            Gizmos.color = Color.blue * 0.5f;
        }
        Gizmos.DrawSphere(intersectionPoint.transform.position, 0.1f);
        if (intersectionPoint.Forward != null)
        {
            if ((gizmoType & GizmoType.Selected) != 0)
            {
                Gizmos.color = Color.cyan;
            }
            else
            {
                Gizmos.color = Color.cyan * 0.5f;
            }
            Gizmos.DrawLine(intersectionPoint.transform.position, intersectionPoint.Forward.transform.position);
        }
        if (intersectionPoint.Left != null)
        {
            if ((gizmoType & GizmoType.Selected) != 0)
            {
                Gizmos.color = Color.cyan;
            }
            else
            {
                Gizmos.color = Color.cyan * 0.5f;
            }
            Gizmos.DrawLine(intersectionPoint.transform.position, intersectionPoint.Left.transform.position);
        }
        if (intersectionPoint.Rotate != null)
        {
            if ((gizmoType & GizmoType.Selected) != 0)
            {
                Gizmos.color = Color.cyan;
            }
            else
            {
                Gizmos.color = Color.cyan * 0.5f;
            }
            Gizmos.DrawLine(intersectionPoint.transform.position, intersectionPoint.Rotate.transform.position);
        }
        if (intersectionPoint.EntryPoint != null)
        {
            if ((gizmoType & GizmoType.Selected) != 0)
            {
                Gizmos.color = Color.magenta;
            }
            else
            {
                Gizmos.color = Color.magenta * 0.5f;
            }
            Gizmos.DrawLine(intersectionPoint.transform.position, intersectionPoint.EntryPoint.transform.position);
        }
        if (intersectionPoint.ExitPoint != null)
        {
            if ((gizmoType & GizmoType.Selected) != 0)
            {
                Gizmos.color = Color.magenta;
            }
            else
            {
                Gizmos.color = Color.magenta * 0.5f;
            }
            
            Gizmos.DrawLine(intersectionPoint.transform.position, intersectionPoint.ExitPoint.transform.position);
        }
    }
}
