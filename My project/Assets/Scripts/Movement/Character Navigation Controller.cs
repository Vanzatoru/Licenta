using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class CharacterNavigationController : MonoBehaviour
{

    public Vector3 destination;
    public bool reachedDestination;
    public float stopDistance=1f;
    public float rotationSpeed =1f;
    public float movementSpeed =1f;
    private WaypointNavigator waypointNavigator;
    System.Random random=new System.Random();
    double BranchChance = 0;
    int Directie;
    private void Start()
    {
        waypointNavigator=GetComponent<WaypointNavigator>();
        Directie =random.Next(2);
        
        Debug.Log(Directie);
    }


    void Update()
    {
        
        if (transform.position != destination)
        {
            Vector3 destinationDirection=destination-transform.position;
            destinationDirection.y = 0;
            float destinationDistance=destinationDirection.magnitude;

            if(destinationDistance >= stopDistance )
            {
                reachedDestination = false;
                Quaternion targetRotation=Quaternion.LookRotation( destinationDirection );
                transform.rotation=Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed* Time.deltaTime );
                transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
            }
            else
            {
               reachedDestination = true;
              // Debug.Log("Destinatie atinsa");
              // Debug.Log(transform.position);
               
           }

            if (reachedDestination)
            {
                
                if (waypointNavigator.waypoint.branch1 != null)
                {
                    
                    BranchChance =random.NextDouble();
                    Debug.Log(BranchChance);
                    if (BranchChance>=0.5)
                    {
                        Debug.Log("Am intrat pe branch");
                        if(Directie==1) 
                            waypointNavigator.waypoint = waypointNavigator.waypoint.branch1; 
                        else
                            waypointNavigator.waypoint = waypointNavigator.waypoint.branch2;

                    }
                    else
                    {
                        if(Directie==1)
                            waypointNavigator.waypoint = waypointNavigator.waypoint.next;
                        else
                            waypointNavigator.waypoint = waypointNavigator.waypoint.previous;


                    }
                }
                else
                {
                    if (Directie == 1)
                        waypointNavigator.waypoint = waypointNavigator.waypoint.next;
                    else
                        waypointNavigator.waypoint = waypointNavigator.waypoint.previous;
                }
                
            }
        }
    }


    public void SetDestination(Vector3 destination)
    {
        this.destination = destination;
    }
}
