using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceOnSpace : MonoBehaviour
{
    // Update is called once per frame
    public GameObject frame;


    void Update()
    {

    }

    public void InstantiateFrame()
    {
        Instantiate(frame);
    }
}
