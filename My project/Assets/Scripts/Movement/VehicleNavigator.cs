using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class VehicleNavigator : MonoBehaviour
{
    private Rigidbody rb;
    private GizmoRectangle gizmoRectangle;
    private GizmoSphere gizmoSphere;
    public Vector3 destination;
    public WayPoint waypoint;
    public IntersectionPoint intersectionPoint;
    public IntersectionNode CurrentIntersection;
    public bool reachedDestination;
    public float stopDistance = 1f;
    public float rotationSpeed = 1f;
    public float movementSpeed = 1f;
    System.Random random = new System.Random();
    public int state;
    short pas=0;
    bool stopjoc = true;
    bool in_intersection = false;
    public bool clear=true;
    public Directions direction;
    public RoadWay roadWay;
    float initialspeed;

    private void Awake()
    {
        state = random.Next(4);
       // Debug.Log(state);
        roadWay = waypoint.GetComponentInParent<RoadWay>();
       // Debug.Log(roadWay.cardinals);
        SetDirection();
       // Debug.Log(direction);
        
    }

    private void Start()
    {
        initialspeed = movementSpeed;
        gizmoRectangle =GetComponent<GizmoRectangle>();
        gizmoSphere = GetComponent<GizmoSphere>();
        rb = GetComponent<Rigidbody>();
        
    }

    private void Update()
    {
        

        if (waypoint.intersectionpoint != null)
        {

            CurrentIntersection = waypoint.intersectionpoint.GetComponentInParent<IntersectionNode>();
            if (stopjoc) { intersectionPoint = waypoint.intersectionpoint; };
            SetDestination(intersectionPoint.transform.position);

            
            if(clear)
                movementSpeed = 5f;
            else 
                movementSpeed = 0f;
        }
        else
        {
            SetDestination(waypoint.transform.position);
            
        }
        
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
                    rb.velocity = transform.forward * movementSpeed;

                }
                else
                {
                    if (waypoint.intersectionpoint != null)
                    {
                        in_intersection = true;
                        pas++;
                    }
                    reachedDestination = true;
                }

                if (waypoint.intersectionpoint != null && !in_intersection) 
                {
                    //Debug.Log("check");
                    switch (state)
                    {
                        case 0:
                            CheckForForward();
                            break;
                        case 1:
                            CheckForLeft();
                            break;
                        case 2:
                            CheckForRotate();
                            break;
                        default: break;
                    }
                }

                if (reachedDestination)
                {//0 fowr 1 left 2 rot 3 right
                    if (waypoint.changepoint) {
                        roadWay = waypoint.GetComponentInParent<RoadWay>();
                        if (roadWay.cardinals == Cardinals.South)
                        {
                            if (state == 1)
                                state = 0;
                            if (state == 2)
                                state = 3;
                        }
                        if(roadWay.cardinals == Cardinals.North)
                        {
                            if (state == 1)
                                state = 0;
                            if (state == 2)
                                state = 3;
                        }
                        if(roadWay.cardinals==Cardinals.West)
                        {
                            if (state == 1)
                                state = 0;
                            if (state == 2)
                                state = 3;
                        }
                        if (roadWay.cardinals == Cardinals.East)
                        {
                            if (state == 1)
                                state = 0;
                            if (state == 2)
                                state = 3;
                        }
                        SetDirection();
                    }
                    if (waypoint.changepoint3)
                    {
                        roadWay = waypoint.GetComponentInParent<RoadWay>();
                        if (roadWay.cardinals == Cardinals.South)
                        {
                            if (state == 3)
                                state = 0;
                            if (state == 2)
                                state = 1;
                        }
                        if (roadWay.cardinals == Cardinals.North)
                        {
                            if (state == 3)
                                state = 0;
                            if (state == 2)
                                state = 1;
                            
                            
                        }
                        if (roadWay.cardinals == Cardinals.West)
                        {
                            if (state == 3)
                                state = 0;
                            if (state == 2)
                                state = 1;
                        }
                        if (roadWay.cardinals == Cardinals.East)
                        {
                            if (state == 3)
                                state = 0;
                            if (state == 2)
                                state = 1;
                        }
                        SetDirection();
                    }
                    if (waypoint.changepoint2)
                    {
                        roadWay = waypoint.GetComponentInParent<RoadWay>();
                        if (state == 0)
                        {
                            state = 1;
                        }
                            
                        if (state==2)
                        {
                            state = 3;
                        }

                        SetDirection();
                    }

                    if (in_intersection && waypoint.intersectionpoint ==null)
                    {
                       
                        in_intersection = false;
                        UnlockPath();
                        roadWay = waypoint.GetComponentInParent<RoadWay>();
                        SetDirection();
                        state = random.Next(4);

                    }

                    

                    if (waypoint.intersectionpoint != null)
                    {
                        

                        if (clear == false)
                        {
                            movementSpeed = 0f;
                        }
                        else
                        {
                            movementSpeed = 5f;
                            
                            LockPath();
                            switch (state)
                            {
                                case 0:

                                    waypoint = intersectionPoint.Forward;
                                    pas = 0;

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
                                        waypoint = intersectionPoint.ExitPoint;
                                        pas = 0;
                                        stopjoc = true;
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
                                        waypoint = intersectionPoint.ExitPoint;
                                        pas = 0;
                                        stopjoc = true;
                                    }
                                    break;
                                case 3:
                                    waypoint = intersectionPoint.ExitPoint;
                                    pas = 0;
;
                                    break;
                            }

                        }
                    }
                    else
                    {
                        CurrentIntersection = null;
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
    
    
    

    public void SetDirection()//0 forward 1 left 2 rotate 3 right
    {
       switch (state) { 
            case 0:
                switch (roadWay.cardinals)
                {
                    case Cardinals.North:
                        direction = Directions.NorthToNorth;
                        break;
                    case Cardinals.South:
                        direction = Directions.SouthToSouth;
                        break;
                    case Cardinals.West:
                        direction = Directions.WestToWest;
                        break;
                    case Cardinals.East:
                        direction = Directions.EastToEast;
                        break;
                }
                break;
            case 1:
                switch (roadWay.cardinals)
                {
                    case Cardinals.North:
                        direction = Directions.NorthToWest;
                        break;
                    case Cardinals.South:
                        direction = Directions.SouthToEast;
                        break;
                    case Cardinals.West:
                        direction = Directions.WestToSouth;
                        break;
                    case Cardinals.East:
                        direction = Directions.EastToNorth;
                        break;
                }
                break;
            case 2:
                switch (roadWay.cardinals)
                {
                    case Cardinals.North:
                        direction = Directions.NorthToSouth;
                        break;
                    case Cardinals.South:
                        direction = Directions.SouthToNorth;
                        break;
                    case Cardinals.West:
                        direction = Directions.WestToEast;
                        break;
                    case Cardinals.East:
                        direction = Directions.EastToWest;
                        break;
                }
                break;
            case 3:
                switch (roadWay.cardinals)
                {
                    case Cardinals.North:
                        direction = Directions.NorthToEast;
                        break;
                    case Cardinals.South:
                        direction = Directions.SouthToWest;
                        break; 
                    case Cardinals.West:
                        direction = Directions.WestToNorth;
                        break;
                    case Cardinals.East:
                        direction = Directions.EastToSouth;
                        break;
                }
                break;
        }
    }

    public void LockPath()
    {
        switch (direction)
        {
            case Directions.NorthToNorth:
               CurrentIntersection.NorthToNorth = true;
                break;

            case Directions.NorthToSouth:
                CurrentIntersection.NorthToSouth = true;
                break;

            case Directions.NorthToWest:
                CurrentIntersection.NorthToWest = true;
                break;

            case Directions.NorthToEast:
                CurrentIntersection.NorthToEast = true;
                break;

            case Directions.SouthToSouth:
                CurrentIntersection.SouthToSouth = true;
                break;

            case Directions.SouthToNorth:
                CurrentIntersection.SouthToNorth = true;
                break;

            case Directions.SouthToWest:
                CurrentIntersection.SouthToWest = true;
                break;

            case Directions.SouthToEast:
                CurrentIntersection.SouthToEast = true;
                break;

            case Directions.WestToWest:
                CurrentIntersection.WestToWest = true;
                break;

            case Directions.WestToNorth:
                CurrentIntersection.WestToNorth = true;
                break;

            case Directions.WestToEast:
                CurrentIntersection.WestToEast = true;
                break;

            case Directions.WestToSouth:
                CurrentIntersection.WestToSouth = true;
                break;

            case Directions.EastToEast:
                CurrentIntersection.EastToSouth = true;
                break;

            case Directions.EastToNorth:
                CurrentIntersection.EastToNorth = true;
                break;

            case Directions.EastToWest:
                CurrentIntersection.EastToWest = true;
                break;

            case Directions.EastToSouth:
                CurrentIntersection.EastToSouth = true;
                break;

            default:
                Console.WriteLine("Invalid direction");
                break;
        }
    }
    public void UnlockPath()
    {
        
        switch (direction)
        {
            case Directions.NorthToNorth:
                CurrentIntersection.NorthToNorth = false;
                break;

            case Directions.NorthToSouth:
                CurrentIntersection.NorthToSouth = false;
                break;

            case Directions.NorthToWest:
                CurrentIntersection.NorthToWest = false;
                break;

            case Directions.NorthToEast:
                CurrentIntersection.NorthToEast = false;
                break;

            case Directions.SouthToSouth:
                CurrentIntersection.SouthToSouth = false;
                break;

            case Directions.SouthToNorth:
                CurrentIntersection.SouthToNorth = false;
                break;

            case Directions.SouthToWest:
                CurrentIntersection.SouthToWest = false;
                break;

            case Directions.SouthToEast:
                CurrentIntersection.SouthToEast = false;
                break;

            case Directions.WestToWest:
                CurrentIntersection.WestToWest = false;
                break;

            case Directions.WestToNorth:
                CurrentIntersection.WestToNorth = false;
                break;

            case Directions.WestToEast:
                CurrentIntersection.WestToEast = false;
                break;

            case Directions.WestToSouth:
                CurrentIntersection.WestToSouth = false;
                break;

            case Directions.EastToEast:
                CurrentIntersection.EastToSouth = false;
                break;

            case Directions.EastToNorth:
                CurrentIntersection.EastToNorth = false;
                break;

            case Directions.EastToWest:
                CurrentIntersection.EastToWest = false;
                break;

            case Directions.EastToSouth:
                CurrentIntersection.EastToSouth = false;
                break;

            default:
                Console.WriteLine("Invalid direction");
                break;
        }
    }

    
    public void CheckForLeft()
    {
        switch (direction)
        {
            case Directions.NorthToWest:
                if(CurrentIntersection.WestToWest==false && CurrentIntersection.WestToSouth == false && CurrentIntersection.WestToEast == false && CurrentIntersection.SouthToSouth == false && CurrentIntersection.SouthToWest == false)
                {
                    clear=true;
                }
                else
                    clear=false;
                break;
            case Directions.SouthToEast:
                if(CurrentIntersection.EastToEast ==false && CurrentIntersection.EastToNorth == false && CurrentIntersection.EastToWest ==false && CurrentIntersection.NorthToNorth==false && CurrentIntersection.NorthToEast==false)
                {
                    clear = true;
                }
                else
                    clear=false;
                break;
            case Directions.WestToSouth:
                if (CurrentIntersection.EastToEast == false && CurrentIntersection.EastToSouth == false && CurrentIntersection.SouthToSouth == false && CurrentIntersection.SouthToEast == false && CurrentIntersection.SouthToNorth == false)
                {
                    clear = true;
                }
                else
                    clear = false;
                break;
            case Directions.EastToNorth:
                if (CurrentIntersection.WestToWest == false && CurrentIntersection.WestToNorth == false && CurrentIntersection.NorthToNorth == false && CurrentIntersection.NorthToWest == false && CurrentIntersection.NorthToSouth  == false)
                {
                    clear = true;
                }
                else
                    clear = false;
                break;
        }
    }
    //CurrentIntersection.WestToWest == false && CurrentIntersection.WestToNorth == false && CurrentIntersection.WestToSouth && CurrentIntersection.WestToEast
    public void CheckForForward()
    {
        switch (direction)
        {
            case Directions.NorthToNorth:
               
                if (CurrentIntersection.WestToWest == false && CurrentIntersection.WestToNorth == false && CurrentIntersection.WestToSouth == false && CurrentIntersection.WestToEast == false)
                {
                    clear = true;
                }
                else
                    clear = false;
                break;
            case Directions.SouthToSouth:
                
                if (CurrentIntersection.EastToEast ==false && CurrentIntersection.EastToNorth == false && CurrentIntersection.EastToWest == false && CurrentIntersection.EastToSouth==false)
                {
                    clear= true;
                }
                else
                    clear= false;
                break;
            case Directions.WestToWest:
              
                if (CurrentIntersection.SouthToSouth == false && CurrentIntersection.SouthToWest ==false && CurrentIntersection.SouthToNorth == false && CurrentIntersection.SouthToEast==false)
                {
                    clear = true;
                }
                else
                    clear = false;
                break;
            case Directions.EastToEast:
               
                if (CurrentIntersection.NorthToEast == false && CurrentIntersection.NorthToNorth == false && CurrentIntersection.NorthToSouth == false && CurrentIntersection.NorthToWest == false)
                {
                    clear = true;
                }
                else
                    clear=false;
                break;
        }
    }
    public void CheckForRotate()
    {
        switch (direction)
        {
            case Directions.NorthToSouth:
                if (CurrentIntersection.SouthToSouth == false && CurrentIntersection.WestToSouth == false)
                    clear = true;
                else
                    clear = false;
                
                break;
            case Directions.SouthToNorth:
                if(CurrentIntersection.NorthToNorth == false && CurrentIntersection.EastToNorth == false)
                {
                    clear=true;
                }
                else
                    clear = false;
               
                break;
            case Directions.WestToEast:
                if(CurrentIntersection.EastToEast ==false && CurrentIntersection.SouthToEast == false)
                {
                    clear = true;
                }
                else
                    clear = false;  
                break;
            case Directions.EastToWest:
                if(CurrentIntersection.WestToWest== false && CurrentIntersection.NorthToWest == false)
                {
                    clear = true;
                }
                else
                    clear = false;
                break;
        }
    }
    /*
    private float Accelerate(int speedlimit)
    {
        float ret;
        

        return ret;
    }
    */
}
