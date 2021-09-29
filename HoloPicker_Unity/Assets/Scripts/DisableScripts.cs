using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableScripts : MonoBehaviour
{
    public MonoBehaviour[] disableScripts;

    // deactivates the components that enable interactibility of the frames
    public void ToggleScripts()
    {      
       
        foreach (var script in disableScripts)
        {
            script.enabled = false;
                    
        }
        
      
    }

}
