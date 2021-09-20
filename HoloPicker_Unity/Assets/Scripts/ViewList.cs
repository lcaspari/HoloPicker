using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewList : MonoBehaviour
{

    public InventoryManager inventoryManager;
    public GameObject text;
    private GameObject track;
    // Start is called before the first frame update


    // Get the order item list from inventory manager
    List<InventoryManager.OrderItem> items = new List<InventoryManager.OrderItem>();
    // Create a list of strings from the order items
    List<string> attributes = new List<string>();

    void Start()
    {

        createList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void createList()
    {
        items.Clear();
        items = inventoryManager.order.orderItem;
        // Add first line of table
        attributes.Add("ORDER");
        attributes.Add("QUANTITY");
        attributes.Add("ID");
        attributes.Add("NAME");
        attributes.Add("CATEGORY");
        attributes.Add("LOCATION");

        // Fill List with the items
        foreach (InventoryManager.OrderItem item in items)
        {
            attributes.Add(item.order);
            attributes.Add(item.quantity.ToString());
            attributes.Add(item.id);
            attributes.Add(item.name);
            attributes.Add(item.category);
            attributes.Add(item.location.ToString());
        }

        // Instantiate text objects with attribute as text
        foreach (string attribute in attributes)
        {
            track = Instantiate(text, transform);
            track.GetComponentInChildren<UnityEngine.UI.Text>().text = attribute;
            Debug.Log("NAME: " + track.GetComponentInChildren<UnityEngine.UI.Text>().text);
        }
    }
}
