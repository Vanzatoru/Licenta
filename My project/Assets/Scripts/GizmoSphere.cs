using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class GizmoSphere : MonoBehaviour
{
    public float height = 10f;
    public float radius = 5f;
    public float maxDistance = 1f;
    public bool pedestrianInSight = false;
    public bool carBlockingPath = false;
    private VehicleNavigator thisCar;

    private void Start()
    {
        radius = 10;
        thisCar = GetComponent<VehicleNavigator>();
    }

    private void Update()
    {
        CheckForPedestrians();
        CheckForCarBlockingPath();
        
    }

    private void OnDrawGizmos()
    {
        if (pedestrianInSight)
            Gizmos.color = Color.red;
        else
            Gizmos.color = Color.green;

        DrawCircle(transform.position + Vector3.up * (height / 2f));
        DrawCircle(transform.position - Vector3.up * (height / 2f));
        Gizmos.DrawLine(transform.position + Vector3.up * (height / 2f), transform.position - Vector3.up * (height / 2f));
        CheckForPedestrians();
        CheckForCarBlockingPath();
        

    }

    private void DrawCircle(Vector3 center)
    {
        int segments = 32;
        float angleIncrement = 360f / segments;

        Vector3 prevPoint = Vector3.zero;
        for (int i = 0; i <= segments; i++)
        {
            float angle = i * angleIncrement;
            float x = center.x + radius * Mathf.Cos(Mathf.Deg2Rad * angle);
            float z = center.z + radius * Mathf.Sin(Mathf.Deg2Rad * angle);
            Vector3 currentPoint = new Vector3(x, center.y, z);

            if (i > 0)
            {
                Gizmos.DrawLine(prevPoint, currentPoint);
            }

            prevPoint = currentPoint;
        }
    }

    private void CheckForPedestrians()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Pedestrian"))
            {
                Vector3 directionToCollider = (collider.transform.position - transform.position).normalized;
                float angle = Vector3.Angle(transform.forward, directionToCollider);

                // Check if the collider is in the front half (angle <= 90 degrees)
                if (angle <= 50f)
                {
                    
                    CharacterNavigationController characterNavigationController = collider.GetComponent<CharacterNavigationController>();
                    if (characterNavigationController != null && characterNavigationController.passing)
                    {
                        pedestrianInSight = true;
                    }
                    return;
                }
            }
        }
        pedestrianInSight = false;
    }
    
    private void CheckForCarBlockingPath()
    {
        carBlockingPath = false;
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Car"))
            {
                Vector3 directionToCollider = (collider.transform.position - transform.position).normalized;
                float angle = Vector3.Angle(transform.forward, directionToCollider);

                if (angle <= 50f)
                {
                    VehicleNavigator Car = collider.GetComponent<VehicleNavigator>();
                    thisCar = GetComponent<VehicleNavigator>();
                    switch (thisCar.direction)
                    {
                        case Directions.NorthToEast:

                            if (Car.direction == Directions.SouthToEast)
                            {
                                carBlockingPath = true;
                                return;
                            }
                            break;
                        case Directions.SouthToWest:
                            if (Car.direction == Directions.NorthToWest)
                            {
                                carBlockingPath = true;
                                return;
                            } 
                            break;
                        case Directions.WestToNorth:
                            if (Car.direction == Directions.EastToNorth)
                            {
                                carBlockingPath = true;
                                return;
                            }
                            break;
                        case Directions.EastToSouth:
                            if (Car.direction == Directions.WestToSouth)
                            {
                                carBlockingPath = true;
                                return;
                            }
                            break;
                        case Directions.NorthToNorth:
                            if(Car.direction == Directions.WestToSouth)
                            {
                                carBlockingPath= true;
                                return;
                            }
                            break;
                        case Directions.SouthToSouth:
                            if(Car.direction == Directions.EastToNorth)
                            {
                                carBlockingPath = true;
                                return;
                            }
                            break;
                        case Directions.WestToWest:
                            if (Car.direction == Directions.SouthToEast)
                            {
                                carBlockingPath = true;
                                return;
                            }
                            break;
                        case Directions.EastToEast:
                            if (Car.direction == Directions.NorthToWest)
                            {
                                carBlockingPath = true;
                                return;
                            }
                            break;

                    }

                }
            }
        }
        
    }
    
}
