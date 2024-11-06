using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeGazeController : MonoBehaviour
{
    public GameObject ray;
    OVREyeGaze eyeGaze;

    void Start()
    {
        eyeGaze = GetComponent<OVREyeGaze>();
    }

    void Update()
    {
        if (eyeGaze == null) return;

        if (eyeGaze.EyeTrackingEnabled)
        {
            ray.transform.rotation = eyeGaze.transform.rotation;
        }
    }
}
