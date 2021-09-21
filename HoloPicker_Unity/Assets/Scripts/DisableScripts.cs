using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableScripts : MonoBehaviour
{
    public MonoBehaviour[] disableScripts;

    public void ToggleScripts(GameObject this_frame)
    {
        Debug.Log("disable scripts is being called");
        
        
        foreach (var script in disableScripts)
        {
            script.enabled = false;
                    
        }
        
      
    }

    public void CallToggle()
    {
        foreach (GameObject a_frame in GameObject.FindGameObjectsWithTag("Frame"))
        {
            ToggleScripts(a_frame);
        }
    }
}
