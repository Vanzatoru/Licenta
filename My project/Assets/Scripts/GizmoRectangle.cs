using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoRectangle : MonoBehaviour
{
    public float width = 10f;
    public float length = 20f;
    public float height = 0f;
    public float maxDistance = 10f;
    public bool carClose = false;
    public bool carForward = false;

    private void Update()
    {
        CheckForNearbyCars();
        CheckForCarsForward();
    }

    private void OnDrawGizmos()
    {
        if (carClose)
            Gizmos.color = Color.red;
        else
            Gizmos.color = Color.green;
        // Draw the wireframe rectangle
        DrawWireRectangle(transform.position, width, length, transform.rotation);
       CheckForNearbyCars();
        CheckForCarsForward();
    }

    // Helper method to draw a wire rectangle
    private void DrawWireRectangle(Vector3 center, float width, float length, Quaternion rotation)
    {
        Vector3 halfExtents = new Vector3(width / 2f, height, length / 2f);

        // Calculate the four corner points of the rotated rectangle
        Vector3 localTopLeft = new Vector3(-halfExtents.x, height, halfExtents.z);
        Vector3 localTopRight = new Vector3(halfExtents.x, height, halfExtents.z);
        Vector3 localBottomLeft = new Vector3(-halfExtents.x, height, -halfExtents.z);
        Vector3 localBottomRight = new Vector3(halfExtents.x, height, -halfExtents.z);

        Vector3 rotatedTopLeft = rotation * localTopLeft + center;
        Vector3 rotatedTopRight = rotation * localTopRight + center;
        Vector3 rotatedBottomLeft = rotation * localBottomLeft + center;
        Vector3 rotatedBottomRight = rotation * localBottomRight + center;

        // Draw the lines connecting the corners to form the rotated rectangle
        Gizmos.DrawLine(rotatedTopLeft, rotatedTopRight);
        Gizmos.DrawLine(rotatedTopRight, rotatedBottomRight);
        Gizmos.DrawLine(rotatedBottomRight, rotatedBottomLeft);
        Gizmos.DrawLine(rotatedBottomLeft, rotatedTopLeft);
    }

    private void CheckForNearbyCars()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position, new Vector3(width / 2f, height, length / 2f), transform.rotation);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Car") && collider.gameObject != this.gameObject)
            {
                carClose = true;
                // Debug.Log("Merge boss");

                return; // If at least one car is found, set carAround to true and exit the loop
            }

        }

        carClose = false;

    }

    private void CheckForCarsForward()
    {
        GameObject[] cars = GameObject.FindGameObjectsWithTag("Car");

        foreach (GameObject car in cars)
        {
            // Get the vector from your object to the car
            Vector3 toCar = car.transform.position - transform.position;

            // Check if the car is within the maximum distance and in front of your object
            if (toCar.sqrMagnitude <= maxDistance * maxDistance && Vector3.Dot(transform.forward, toCar.normalized) > 0f)
            {
                carForward = true;
                return;
            }
           
        }
        carForward = false;
    }
}
