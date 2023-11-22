using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoRectangle : MonoBehaviour
{
    public float width = 10f;
    public float length = 20f;
    public bool carAround = false;

    private void Update()
    {
        CheckForNearbyCars();
    }

    private void OnDrawGizmos()
    {
        if (carAround)
            Gizmos.color = Color.red;
        else
            Gizmos.color = Color.green;
        // Draw the wireframe rectangle
        DrawWireRectangle(transform.position, width, length, transform.rotation);
    }

    // Helper method to draw a wire rectangle
    private void DrawWireRectangle(Vector3 center, float width, float length, Quaternion rotation)
    {
        Vector3 halfExtents = new Vector3(width / 2f, 0f, length / 2f);

        // Calculate the four corner points of the rotated rectangle
        Vector3 localTopLeft = new Vector3(-halfExtents.x, 0f, halfExtents.z);
        Vector3 localTopRight = new Vector3(halfExtents.x, 0f, halfExtents.z);
        Vector3 localBottomLeft = new Vector3(-halfExtents.x, 0f, -halfExtents.z);
        Vector3 localBottomRight = new Vector3(halfExtents.x, 0f, -halfExtents.z);

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
        Collider[] colliders = Physics.OverlapBox(transform.position, new Vector3(width / 2f, 0f, length / 2f), transform.rotation);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Car") && collider.gameObject != this.gameObject)
            {
                carAround = true;
                // Debug.Log("Merge boss");

                return; // If at least one car is found, set carAround to true and exit the loop
            }

        }

        // If no car is found, set carAround to false
        carAround = false;

    }
}
