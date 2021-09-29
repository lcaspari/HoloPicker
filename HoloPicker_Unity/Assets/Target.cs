using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 tempPos;
    Quaternion tempRot;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
