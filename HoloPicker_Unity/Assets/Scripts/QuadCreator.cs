using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadCreator : MonoBehaviour
{
    public float width = 1;
    public float height = 1;

    [SerializeField] private GameObject _imageTarget1;
    [SerializeField] private GameObject _imageTarget2;
    [SerializeField] private GameObject _imageTarget3;
    [SerializeField] private GameObject _imageTarget4;

    public void Start()
    {
    }

    private void Update()
    {
        if (_imageTarget1.GetComponent<ImageTarget>() == true &&
            _imageTarget2.GetComponent<ImageTarget>() == true &&
            _imageTarget3.GetComponent<ImageTarget>() == true &&
            _imageTarget4.GetComponent<ImageTarget>() == true)
        {
            this.gameObject.SetActive(true);
        }
    }
}
