using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceOnSpace : MonoBehaviour
{

    public GameObject frame;
    // to store last instantiated gameObject
    GameObject track;
    public GameObject target;
    public Text _frameName;
    public GameObject _frameNameObject;
    public GameObject _keyboardCheck;
    // to name the object
    TouchScreenKeyboard keyboard;
    public static string keyboardText = "";
    private string input;



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
        keyboard = TouchScreenKeyboard.Open("text to edit", TouchScreenKeyboardType.NumberPad, false, false, false, false);
        keyboardText = keyboard.text;
        _frameName.text = keyboard.text;
        _frameNameObject.SetActive(true);
        _keyboardCheck.SetActive(true);

        track.name = number.ToString();
        number++;
    }

    GameObject[] frames;
    GameObject obj;
    GameObject[] frameColours;
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
    public void ActivateFrame(int name, string order)
    {
        keyboard.active = false;
        _frame = transform.GetChild(name).gameObject;
        _frame.SetActive(true);

        target.GetComponent<Target>().CallPosition(_frame.transform.position);
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
    public void DeactivateFrame(int name)
    {
        _oldFrame = transform.GetChild(name).gameObject;
        _oldFrame.SetActive(false);
    }

    


}
