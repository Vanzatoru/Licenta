using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointNavigator : MonoBehaviour
{
    public WayPoint waypoint;
    private float randomFloat;
    private CharacterNavigationController characterController;
    private Vector3 randomDestination= new Vector3 (0, 0, 0);
   
    void Start()
    {
        randomFloat = GetRandomFloat(-waypoint.width / 2, waypoint.width / 2);
        randomLR();
        characterController = GetComponent<CharacterNavigationController>();
        if (characterController == null)
        {
            Debug.LogError("CharacterNavigationController component not found on the GameObject.");
        }
    }

    void Update()
    {
        characterController.SetDestination(randomDestination);
    }

    public void randomLR()
    {
        randomDestination = waypoint.transform.position;
        randomDestination.x = randomDestination.x + randomFloat;

    }
    float GetRandomFloat(float minValue, float maxValue)
    {
        return Random.Range(minValue, maxValue);
    }
}
