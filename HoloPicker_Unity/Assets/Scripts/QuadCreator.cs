using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadCreator : MonoBehaviour
{
    //qr codes of two diagonal corners
    [SerializeField] private GameObject _imageTarget1;
    [SerializeField] private GameObject _imageTarget2;

    [SerializeField] private GameObject _quadPrefab;

    private bool QuadInstantiated = false;
    
    private Vector3 _lowerLeft;
    private Vector3 _lowerRight;
    private Vector3 _upperLeft;
    private Vector3 _upperRight;

    private float _img1X;
    private float _img1Y;
    private float _img1Z;
    private float _img2X;
    private float _img2Y;

    private void Start()
    {
        //x,y and z coordinates of the qr codes
        _img1X = _imageTarget1.transform.position.x;
        _img1Y = _imageTarget1.transform.position.y;
        _img1Z = _imageTarget1.transform.position.z;
        _img2X = _imageTarget2.transform.position.x;
        _img2Y = _imageTarget2.transform.position.z;
        
        //sets the positions of the corners to match the qr code positions
        _upperLeft = new Vector3(_img1X, _img1Y, _img1Z);
        _lowerRight = new Vector3(_img2X, _img2Y, _img1Z);
        _lowerLeft = new Vector3(_img1X, _img2Y, _img1Z);
        _upperRight = new Vector3(_img2X, _img1Y, _img1Z);
    }

    //checks each frame if both qr codes have been tracked, then instantiates a quad and  adjusts the position
    //only create one quad per frame
    void Update()
    {
        if (_imageTarget1.GetComponent<ImageTarget>().GetWasTracked()
            && _imageTarget2.GetComponent<ImageTarget>().GetWasTracked()
            && !QuadInstantiated)
        {
            QuadInstantiated = true;
            Instantiate(_quadPrefab, (_imageTarget1.transform.position + _imageTarget2.transform.position) / 2, Quaternion.Euler(90,0,0));
            _quadPrefab.GetComponent<Quad>().AdjustVertices(_lowerLeft,_lowerRight, _upperLeft, _upperRight);
        }
    }
}
