using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleNavigator : MonoBehaviour
{
    private Rigidbody rb;
    private GizmoRectangle gizmoRectangle;
    private GizmoSphere gizmoSphere;
    public Vector3 destination;
    public WayPoint waypoint;
    public IntersectionPoint intersectionPoint;
    public bool reachedDestination;
    public float stopDistance = 1f;
    public float rotationSpeed = 1f;
    public float movementSpeed = 1f;
    System.Random random = new System.Random();
    int state;
    short pas=0;
    bool stopjoc = true;

    private void Start()
    {
        gizmoRectangle=GetComponent<GizmoRectangle>();
        rb = GetComponent<Rigidbody>();
        state = random.Next(4);
    }

    private void Update()
    {
        if (waypoint.intersectionpoint != null)
        {
            if (stopjoc) { intersectionPoint = waypoint.intersectionpoint; };
            SetDestination(intersectionPoint.transform.position);
        }
        else
            SetDestination(waypoint.transform.position);

        if (gizmoRectangle.carForward && gizmoRectangle.carClose)
        {

        }
        else
        {
            if (transform.position != destination)
            {
                Vector3 destinationDirection = destination - transform.position;
                destinationDirection.y = 0;
                float destinationDistance = destinationDirection.magnitude;
                if (destinationDistance >= stopDistance)
                {
                    reachedDestination = false;
                    Quaternion targetRotation = Quaternion.LookRotation(destinationDirection);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                    // transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
                    rb.velocity = transform.forward * movementSpeed * Time.deltaTime;

                }
                else
                {
                    if (waypoint.intersectionpoint != null)
                    {
                        pas++;
                    }
                    reachedDestination = true;
                }

                if (reachedDestination)
                {
                    if (waypoint.intersectionpoint != null)
                    {
                        switch (state)
                        {
                            case 0:

                                if (pas == 1)
                                {
                                    SetIntersection(intersectionPoint.Forward);
                                    stopjoc = false;
                                    SetDestination(intersectionPoint.transform.position);
                                }
                                if (pas == 2)
                                {
                                    pas2();
                                }

                                break;
                            case 1:

                                if (pas == 1)
                                {
                                    SetIntersection(intersectionPoint.Left);
                                    stopjoc = false;
                                    SetDestination(intersectionPoint.transform.position);
                                }
                                if (pas == 2)
                                {
                                    pas2();
                                }
                                break;
                            case 2:

                                if (pas == 1)
                                {

                                    SetIntersection(intersectionPoint.Rotate);
                                    stopjoc = false;
                                    SetDestination(intersectionPoint.transform.position);


                                }
                                if (pas == 2)
                                {

                                    pas2();
                                }
                                break;
                            case 3:
                                waypoint = intersectionPoint.ExitPoint;
                                pas = 0;
                                state = random.Next(4);
                                // Debug.Log(state);
                                break;
                        }

                    }
                    else
                    {
                        waypoint = waypoint.next;
                    }
                }
            }
        }
    }
    public void SetDestination(Vector3 destination)
    {
        this.destination = destination;
    }
    public void Setwaypoint(WayPoint waypoint)
    {
        this.waypoint = waypoint;
    }

    public void SetIntersection(IntersectionPoint intersectionPoint)
    {
        this.intersectionPoint= intersectionPoint;
    }
    
    
    public void pas2()
    {
        waypoint = intersectionPoint.ExitPoint;
        pas = 0;
        stopjoc = true;
        state = random.Next(4);
        //Debug.Log(state);
    }
}
