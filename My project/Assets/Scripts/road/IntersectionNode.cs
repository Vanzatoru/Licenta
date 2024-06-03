using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntersectionNode : MonoBehaviour
{
    public bool NorthToNorth,
                NorthToSouth,
                NorthToWest,
                NorthToEast,
                SouthToSouth,
                SouthToNorth,
                SouthToWest,
                SouthToEast,
                WestToWest,
                WestToNorth,
                WestToEast,
                WestToSouth,
                EastToEast,
                EastToNorth,
                EastToWest,
                EastToSouth;


    public void SetBoolToTrue(bool value)
    {
        value = true;
    }
    public void SetBoolToFalse(bool value)
    {
        value = false;
    }

}