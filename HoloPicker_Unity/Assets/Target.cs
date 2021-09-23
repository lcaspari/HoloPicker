using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 tempPos;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CallPosition(Vector3 posTarget)
    {
        tempPos = transform.position;
        tempPos = posTarget;
        transform.position = tempPos;
    }
}
