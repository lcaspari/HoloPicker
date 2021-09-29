using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    Vector3 tempPos;
    Quaternion tempRot;

    // Function is called to adjust the position and rotation of the product information window according to the currently shown frame
    public void CallPosition(Vector3 posTarget, Quaternion rotation)
    {
        // change position to current frame
        tempPos = transform.position;
        tempPos = posTarget;
        transform.position = tempPos;

        // change rotation according to current frame
        tempRot = transform.rotation;
        tempRot = rotation;
        transform.rotation = tempRot;
    }
}
