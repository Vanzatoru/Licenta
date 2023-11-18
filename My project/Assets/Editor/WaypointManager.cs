using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WayPointManager : EditorWindow
{
    [MenuItem("Tools/Waypoint Editor")]
    public static void Open()
    {
        GetWindow<WayPointManager>();
    }

    [SerializeField]
    private Transform waypointRoot;

    private void OnGUI()
    {
        SerializedObject obj = new SerializedObject(this);
        EditorGUILayout.PropertyField(obj.FindProperty("waypointRoot"));

        if (waypointRoot == null)
        {
            EditorGUILayout.HelpBox("Root transform must be selected", MessageType.Warning);
        }
        else
        {
            EditorGUILayout.BeginVertical("box");
            DrawButtons();
            EditorGUILayout.EndVertical();

        }

        obj.ApplyModifiedProperties();
    }

    void DrawButtons()
    {
        if (GUILayout.Button("Create Waypoint"))
            CreateWaypoint();
        if(Selection.activeGameObject !=null && Selection.activeGameObject.GetComponent<WayPoint>())
        {
            if(GUILayout.Button("Create Waypoint before"))
            {
                CreateWaypointAfter();
            }
            if (GUILayout.Button("Create Waypoint after"))
            {
                CreateWaypointbefore();
            }
            if (GUILayout.Button("Remove Waypoint"))
            {
                RemoveWaypoint();
            }
            if (GUILayout.Button("Create Exit"))
            {
                CreateExit();
            }
        }
    }
    
    void CreateWaypointAfter()
    {
        GameObject waypointObject = new GameObject("WayPoint" + waypointRoot.childCount, typeof(WayPoint));
        waypointObject.transform.SetParent(waypointRoot, false);

        WayPoint newWaypoint = waypointObject.GetComponent<WayPoint>();

        WayPoint selectedWayPoint =Selection.activeGameObject.GetComponent<WayPoint>();

        waypointObject.transform.position=selectedWayPoint.transform.position;
        waypointObject.transform.forward = selectedWayPoint.transform.forward;
        if (selectedWayPoint.next != null)
        {
            selectedWayPoint.next.previous = newWaypoint;
            newWaypoint.next = selectedWayPoint.next;
           
        }
        selectedWayPoint.next = newWaypoint;

        newWaypoint.transform.SetSiblingIndex(selectedWayPoint.transform.GetSiblingIndex());
        Selection.activeGameObject = newWaypoint.gameObject;
    }
    void CreateWaypointbefore()
    {
        GameObject waypointObject = new GameObject("WayPoint" + waypointRoot.childCount, typeof(WayPoint));
        waypointObject.transform.SetParent(waypointRoot, false);

        WayPoint newWaypoint = waypointObject.GetComponent<WayPoint>();

        WayPoint selectedWayPoint = Selection.activeGameObject.GetComponent<WayPoint>();

        waypointObject.transform.position = selectedWayPoint.transform.position;
        waypointObject.transform.forward = selectedWayPoint.transform.forward;
        if (selectedWayPoint.previous != null)
        {
            newWaypoint.previous = selectedWayPoint.previous;
            selectedWayPoint.previous.next = newWaypoint;
        }
        newWaypoint.next = selectedWayPoint;
        selectedWayPoint.previous = newWaypoint;

        newWaypoint.transform.SetSiblingIndex(selectedWayPoint.transform.GetSiblingIndex());
        Selection.activeGameObject = newWaypoint.gameObject;
    }
    void RemoveWaypoint()
    {
        WayPoint selectedWaypoint = Selection.activeGameObject.GetComponent<WayPoint>();
        if(selectedWaypoint.next != null)
        {
            selectedWaypoint.next.previous = selectedWaypoint.previous;
        }
        if(selectedWaypoint.previous != null)
        {
            selectedWaypoint.previous.next = selectedWaypoint.next;
            Selection.activeGameObject=selectedWaypoint.previous.gameObject;
        }
        DestroyImmediate(selectedWaypoint.gameObject);
    }

    void CreateExit()
    {
        GameObject waypointObject = new GameObject("WayPointExit", typeof(WayPoint));
        waypointObject.transform.SetParent(waypointRoot, false);

        WayPoint newWaypoint = waypointObject.GetComponent<WayPoint>();

        WayPoint selectedWayPoint = Selection.activeGameObject.GetComponent<WayPoint>();

        waypointObject.transform.position = selectedWayPoint.transform.position;
        waypointObject.transform.forward = selectedWayPoint.transform.forward;
       
        newWaypoint.previous = selectedWayPoint.previous;
        newWaypoint.next = selectedWayPoint.next;

        newWaypoint.transform.SetSiblingIndex(selectedWayPoint.transform.GetSiblingIndex());
        Selection.activeGameObject = newWaypoint.gameObject;
    }
    
    void CreateWaypoint()
    {
        GameObject waypointObject = new GameObject("WayPoint" + waypointRoot.childCount, typeof(WayPoint));
        waypointObject.transform.SetParent(waypointRoot, false);
        WayPoint wayPoint = waypointObject.GetComponent<WayPoint>();
        if (waypointRoot.childCount > 1)
        {
            wayPoint.previous = waypointRoot.GetChild(waypointRoot.childCount - 2).GetComponent<WayPoint>();
            wayPoint.previous.next = wayPoint;
            //place the waypoint at the last position
            wayPoint.transform.position = wayPoint.previous.transform.position;
            wayPoint.transform.forward = wayPoint.previous.transform.forward;
        }
        Selection.activeObject = wayPoint.gameObject;
    }
}
