using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    public WayPoint previous;
    public WayPoint next;
    public WayPoint branch1;
    public WayPoint branch2;
    public IntersectionPoint intersectionpoint;
    [SerializeField]
    [Range(0f, 5f)]
    public float width = 1f;



    public Vector3 GetPosition()
    {
        Vector3 minBound = transform.position + transform.right * width / 2f;
        Vector3 maxBound = transform.position - transform.right * width / 2f;
        return Vector3.Lerp(minBound, maxBound, Random.Range(0f, 1f));
    }
}
