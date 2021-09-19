using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewList : MonoBehaviour
{

    public GameObject gridObjectCollection; 
    public InventoryManager inventoryManager;
    public GameObject text;
    private GameObject track;
    // Start is called before the first frame update
    
    void Start()
    {
        // Get the order item list from inventory manager
        List<InventoryManager.OrderItem> items = new List<InventoryManager.OrderItem>();
        items = inventoryManager.order.orderItem;
        // Create a list of strings from the order items
        List<string> attributes = new List<string>();
        Debug.Log("ANZAHL ITEMS: " + items.Count);
        foreach (InventoryManager.OrderItem item in items)
        {
            attributes.Add(item.order);
            attributes.Add(item.quantity.ToString());
            attributes.Add(item.id);
            attributes.Add(item.name);
            attributes.Add(item.category);
            attributes.Add(item.location.ToString());
        }

        foreach (string attribute in attributes)
        {
            track = Instantiate(text, transform);
            track.GetComponentInChildren<UnityEngine.UI.Text>().text = attribute;
            Debug.Log("NAME: " + track.GetComponentInChildren<UnityEngine.UI.Text>().text);
        }

 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
