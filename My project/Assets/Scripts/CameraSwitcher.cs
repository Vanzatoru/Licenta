using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera[] cameras;

    void Start()
    {
   
        if (cameras.Length > 0)
        {
   
            for (int i = 0; i < cameras.Length; i++)
            {
                cameras[i].enabled = (i == 0);
            }
        }
        else
        {
            Debug.LogWarning("No cameras assigned to the CameraSwitcher script.");
        }
    }

    void Update()
    {
        // Loop through number keys to check if one is pressed
        for (int i = 0; i < cameras.Length; i++)
        {
            if (Input.GetKeyDown((KeyCode)(KeyCode.Alpha1 + i)))
            {
                SwitchToCamera(i);
            }
        }
    }

    void SwitchToCamera(int index)
    {
        if (index >= 0 && index < cameras.Length)
        {
            for (int i = 0; i < cameras.Length; i++)
            {
                cameras[i].enabled = (i == index);
            }
        }
        else
        {
            Debug.LogWarning("Camera index out of bounds: " + index);
        }
    }
}