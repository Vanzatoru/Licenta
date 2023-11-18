using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointNavigator : MonoBehaviour
{
    public WayPoint waypoint;
    private CharacterNavigationController characterController;
    void Start()
    {
        characterController = GetComponent<CharacterNavigationController>();
        if (characterController == null)
        {
            Debug.LogError("CharacterNavigationController component not found on the GameObject.");
        }
    }

    void Update()
    {
        characterController.SetDestination(waypoint.transform.position);

    }

    public void Setwaypoint(WayPoint waypoint)
    {
        this.waypoint = waypoint;
    }
}
