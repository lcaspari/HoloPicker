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
        //gameObject.GetComponentInChildren.TapToPlace();


    }
    private GameObject _frame;
    private GameObject _oldFrame;
    public void ActivateFrame(int name)
    {
        Debug.LogWarning(name);

        _frame = transform.GetChild(name).gameObject;
        Debug.LogWarning(name);
        _frame.SetActive(true);

        //frame.GetComponent<TapToPlace>().enabled = false;
        //transform.GetChild(name - 1).GetComponent<BoundsControl>().enabled = false;

    }
    public void DeactivateFrame(int name)
    {
        Debug.LogWarning(name);

        _oldFrame = transform.GetChild(name).gameObject;
        Debug.LogWarning("Deactivate Previous");

        _oldFrame.SetActive(false);

    }

    public void DeactivateComponents()
    {
        //transform.GetChild(0).GetComponent<>().enabled = false;

    }


}
