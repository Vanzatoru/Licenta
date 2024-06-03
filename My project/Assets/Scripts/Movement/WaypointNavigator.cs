using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointNavigator : MonoBehaviour
{
    public WayPoint waypoint;
    
    private CharacterNavigationController characterController;
    private Vector3 randomDestination= new Vector3 (0, 0, 0);
   
    void Start()
    {
        randomLR();
        characterController = GetComponent<CharacterNavigationController>();
        if (characterController == null)
        {
            Debug.LogError("CharacterNavigationController component not found on the GameObject.");
        }
        
    }

    void Update()
    {
        //characterController.SetDestination(waypoint.transform.position);
        characterController.SetDestination(randomDestination);
        
    }

    public void Setwaypoint(WayPoint waypoint)
    {
        this.waypoint = waypoint;
    }

    public void randomLR()
    {
        
        float randomFloat = GetRandomFloat(-waypoint.width/2, waypoint.width/2);
        randomDestination = waypoint.transform.position;
        randomDestination.x = randomDestination.x + randomFloat;
      //  Debug.Log(waypoint.transform.position);
       // Debug.Log(randomDestination);
       // Debug.Log(randomFloat);

    }
    float GetRandomFloat(float minValue, float maxValue)
    {
        return Random.Range(minValue, maxValue);
    }
}
