using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableScripts : MonoBehaviour
{
    public MonoBehaviour[] disableScripts;

    public void ToggleScripts()
    {
        Debug.Log("disable scripts is being called");
        
        
        foreach (var script in disableScripts)
        {
            script.enabled = false;
                    
        }
        
      
    }

}
