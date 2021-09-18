using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quad : MonoBehaviour
{
    [SerializeField] private Material _transGreen;
    [SerializeField] private Material _transRed;

    //true if frame is next on the list
    private bool _isCurrent = false;
    
    
    //adjusts the quad so that it matches the qr codes
    public void AdjustVertices(Vector3 startPos1, Vector3 startPos2, Vector3 endPos1, Vector3 endPos2)
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
        vertices[0] = startPos1;
        vertices[1] = startPos2;
        vertices[2] = endPos1;
        vertices[3] = endPos2;
        mesh.vertices = vertices;
    }

    //event that is called when frame is the next on the list
    //current frame turns green and arrow points in its direction
    public void SwitchToCurrent()
    {
        _isCurrent = true;
        GetComponent<Renderer>().material = _transGreen;
    }
    

    //event 1: hand collides with current frame --> turn to default color
    
    //event 2: hand collides with not current frame --> turn red
    
}
