using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceOnSpace : MonoBehaviour
{
    
    public GameObject frame;
    // to store last instantiated gameObject
    GameObject track;
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
    public GameObject _frameManager;
    public void ActivateFrame(string name)
    {
        _frameManager.transform.Find(name).gameObject.SetActive(true);
        
        
    }

     
}
