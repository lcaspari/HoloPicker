using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlaceOnSpace : MonoBehaviour
{

    public GameObject frame;
    GameObject[] frames;
    GameObject obj;
    GameObject[] frameColours;
    // to store last instantiated gameObject
    GameObject track;
    // instantiate gameobject for the target of the arrow
    public GameObject target;
    
    // instantiates a new frame and activates it to make it visible
    public void InstantiateFrame()
    {
        track = Instantiate(frame, transform);
        track.SetActive(true);
    }


    // names the frames that are being instantiated
    int number = 1;
    public void NameFrame()
    {
        track.name = number.ToString();
        number++;
    }

    //deactivates all frames when the order is processed
    public void ProcessOrderStarted()
    {

        frames = GameObject.FindGameObjectsWithTag("Frame");

        foreach (GameObject obj in frames)
        {
            obj.SetActive(false);
        }

    }
    private GameObject _frame;
    private GameObject _oldFrame;

    // activates the current frame and gives it a colour depending on the action
    public void ActivateFrame(int name, string order)
    {
        _frame = transform.GetChild(name).gameObject;
        _frame.SetActive(true);

        // position of the current frame is also given to the target
        target.GetComponent<Target>().CallPosition(_frame.transform.position, _frame.transform.rotation);
        // disable scripts is called to disable the scalability component of the frames
        _frame.GetComponent<DisableScripts>().ToggleScripts();
 
        if (order == "pick")
        {
            frameColours = GameObject.FindGameObjectsWithTag("FrameMaterial");

            foreach (GameObject obj in frameColours)
            {
                obj.GetComponent<Renderer>().material.color = Color.green;
            }
        }
        else
        {
            frameColours = GameObject.FindGameObjectsWithTag("FrameMaterial");

            foreach (GameObject obj in frameColours)
            {
                obj.GetComponent<Renderer>().material.color = Color.blue;
            }
        }
    }

    // deactivates the frame 
    public void DeactivateFrame(int name)
    {
        _oldFrame = transform.GetChild(name).gameObject;
        _oldFrame.SetActive(false);
    }

    


}
