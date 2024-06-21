using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    public bool changepoint;
    public bool changepoint2;
    public bool changepoint3;
    public WayPoint previous;
    public WayPoint next;
    public WayPoint branch1;
    public WayPoint branch2;
    public IntersectionPoint intersectionpoint;
    [SerializeField]
    [Range(0f, 5f)]
    public float width = 3.1f;



}
