using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoSphere : MonoBehaviour
{
    public float height = 10f;
    public float radius = 5f;
    public float maxDistance = 1f;
    public  bool pedestrianInSight = false;
    


    private void Update()
    {
        CheckForPedestrians();
        //CheckForPedestriansForward();
    }

    private void OnDrawGizmos()
    {
        if(pedestrianInSight)
            Gizmos.color = Color.red;
        else   
            Gizmos.color = Color.green;

        DrawCircle(transform.position + Vector3.up * (height / 2f));

        DrawCircle(transform.position - Vector3.up * (height / 2f));

        Gizmos.DrawLine(transform.position + Vector3.up * (height / 2f), transform.position - Vector3.up * (height / 2f));
        CheckForPedestrians();
       // CheckForPedestriansForward();
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
            if (collider.CompareTag("Pedestrian") && collider.gameObject )
            {
                Debug.Log("in sight");
                CharacterNavigationController characterNavigationController = collider.GetComponent<CharacterNavigationController>();
                if (characterNavigationController.passing)
                {
                    pedestrianInSight = true;
                }
                return;
            }
            else
            {
                pedestrianInSight = false;
            }
        }
    }
   

}
