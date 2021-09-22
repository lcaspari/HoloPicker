using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceOnSpace : MonoBehaviour
{

    public GameObject frame;
    // to store last instantiated gameObject
    GameObject track;
    public GameObject target;
    // to name the object
    TouchScreenKeyboard keyboard;
    public static string keyboardText = "";



    // Update is called once per frame
    void Update()
    {
    }

    public void InstantiateFrame()
    {
        track = Instantiate(frame, transform);
        track.SetActive(true);
    }


    int number = 1;
    public void NameFrame()
    {
        //keyboard = TouchScreenKeyboard.Open("text to edit", TouchScreenKeyboardType.NumberPad);
        //keyboardText = keyboard.text;

        track.name = number.ToString();
        number++;
    }

    GameObject[] frames;
    GameObject obj;
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
    public void ActivateFrame(int name)
    {
        _frame = transform.GetChild(name).gameObject;
        _frame.SetActive(true);
        Debug.Log("target position: " + _frame.transform.position);
        target.GetComponent<Target>().CallPosition(_frame.transform.position);
        _frame.GetComponent<DisableScripts>().ToggleScripts();
 
    }
    public void DeactivateFrame(int name)
    {
        _oldFrame = transform.GetChild(name).gameObject;
        _oldFrame.SetActive(false);
    }

    


}
