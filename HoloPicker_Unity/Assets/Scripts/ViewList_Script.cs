using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewList_Script : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public GameObject text;
    private GameObject track;
    public ViewList_Text viewList_text;

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
        // Destroy all children, so old list does not show up anymore
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<ViewList_Text>().DestroyClone();
        }

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
            track.tag = "TextInList";
            track.GetComponentInChildren<UnityEngine.UI.Text>().text = attribute;
        }
    }
}
