using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductInformation : MonoBehaviour
{

    // Set the content of the product information window
    // Function is called by InventoryManager which writes the current product into the window
    public void setContent(string action, string id, string name, int quantity, int location)
    {
        // Save information in a list
        string[] features = {action, id, name, quantity.ToString(), location.ToString()};
        for (int i = 5; i < transform.childCount; i++)
        {
            // Replace the content of the table with the information in the list
            transform.GetChild(i).GetComponentInChildren<UnityEngine.UI.Text>().text = features[i-5];
        }
    }
}