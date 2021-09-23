using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewList_Script : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public GameObject text;
    private GameObject track;

    // Get the order item list from inventory manager
    List<InventoryManager.OrderItem> items = new List<InventoryManager.OrderItem>();
    


    void Start()
    {
        createList();
    }

    // Update is called once per frame
    void Update()
    {

    }
    List<string> createInternLists(List<InventoryManager.OrderItem> items)
    {
        // Create a list of strings from the order items
        List<string> attributes = new List<string>();

        // Add first line of table
        attributes.Add("ORDER");
        attributes.Add("QUANTITY");
        attributes.Add("ID");
        attributes.Add("NAME");
        attributes.Add("CATEGORY");
        attributes.Add("LOCATION");

        // borderControl limits the total number of displayed items, so they only appear in the blue field
        int borderControl = 0;
        // Fill List with the items
        foreach (InventoryManager.OrderItem item in items)
        {
            // only the first 5 items of the list are shown
            if (borderControl < 5)
            {
                attributes.Add(item.order);
                attributes.Add(item.quantity.ToString());
                attributes.Add(item.id);
                attributes.Add(item.name);
                attributes.Add(item.category);
                attributes.Add(item.location.ToString());
            }
            borderControl++;
        }

        return attributes;
    }

    public void createList()
    {
        // Create a list of strings from the order items
        List<string> attributes = new List<string>(); 

        attributes = createInternLists(inventoryManager.order.orderItem);

        // Instantiate text objects with attribute as text
        foreach (string attribute in attributes)
        {
            track = Instantiate(text, transform);
            track.tag = "TextInList";
            track.GetComponentInChildren<UnityEngine.UI.Text>().text = attribute;
        }
    }

    public void updateList()
    {
        // Create a list of strings from the order items
        List<string> attributes = new List<string>();

        attributes = createInternLists(inventoryManager.getOrderListFromJSON());

        // fill content of the table with the new list and then empty the rest by writing nothing in it
        for (int i = 0; i < transform.childCount; i++)
        {
            // borderControl makes sure, that only those orders are shown which can be placed within the blue frame
            if (i < attributes.Count)
            {
                transform.GetChild(i).GetComponentInChildren<UnityEngine.UI.Text>().text = attributes[i];
            }
            // "delete" unused table objects by erasing the text inside
            else
            {
                Debug.Log(i);
                transform.GetChild(i).GetComponentInChildren<UnityEngine.UI.Text>().text = "";
            }
            
        }
    }
}
