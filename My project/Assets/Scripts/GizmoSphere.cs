using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoSphere : MonoBehaviour
{
    public float height = 10f;
    public float radius = 5f;
    public  bool carAround = false;
    private void OnDrawGizmos()
    {
        if(carAround)
            Gizmos.color = Color.red;
        else   
            Gizmos.color = Color.green;

        // Draw the top circle of the cylinder
        DrawCircle(transform.position + Vector3.up * (height / 2f));

        // Draw the bottom circle of the cylinder
        DrawCircle(transform.position - Vector3.up * (height / 2f));

        // Draw the vertical lines connecting the circles to form the cylinder
        Gizmos.DrawLine(transform.position + Vector3.up * (height / 2f), transform.position - Vector3.up * (height / 2f));
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Car") && collider.gameObject != this.gameObject)
            {
                Debug.Log("Car nearby");
                carAround = true;
                break;

            }
            else
            {
                carAround = false;
            }
        }
    }

    // Helper method to draw a circle in the Gizmos
    private void DrawCircle(Vector3 center)
    {
        int segments = 32; // Adjust the number of segments for smoother circles
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
}
