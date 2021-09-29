using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewList_Text : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyClone()
    {
        /*GameObject[] clones = GameObject.FindGameObjectsWithTag("TextInList");
        Debug.Log(clones.Length);
        foreach (GameObject clone in clones)
        {
            Destroy(clone);
        }*/
        // Destroy Function does not destroy object
        Destroy(gameObject);
    }
}
