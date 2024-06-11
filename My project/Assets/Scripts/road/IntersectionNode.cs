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

    public bool WestLight,EastLight,NorthLight,SouthLight;
    public bool TrafficLights=false;
    private float timer = 0.0f;
    private float greentime = 15.0f;

    public void SetBoolToTrue(bool value)
    {
        value = true;
    }
    public void SetBoolToFalse(bool value)
    {
        value = false;
    }

    private void Update()
    {
          timer += Time.deltaTime;
        if (timer<greentime)
        {
            SouthLight = true;
            EastLight = false;
            WestLight = false;
            NorthLight = false;
        }
        if (timer>=greentime && timer<greentime*2)
        {
            SouthLight = false;
            EastLight = true;
            WestLight = false;
            NorthLight = false;
        }
        if (timer >= greentime*2 && timer < greentime * 3)
        {
            SouthLight = false;
            EastLight = false;
            WestLight = true;
            NorthLight = false;
        }
        if (timer >= greentime*3 && timer < greentime * 4)
        {
            SouthLight = false;
            EastLight = false;
            WestLight = false;
            NorthLight = true;
        }
        if (timer > greentime * 4)
        {
            timer = 0.0f;
        }

    }

}