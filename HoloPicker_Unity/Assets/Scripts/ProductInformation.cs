using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductInformation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setContent(string action, string id, string name, int quantity, int location)
    {
        string[] features = {action, id, name, quantity.ToString(), location.ToString()};
        for (int i = 5; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponentInChildren<UnityEngine.UI.Text>().text = features[i-5];
        }
    }
}