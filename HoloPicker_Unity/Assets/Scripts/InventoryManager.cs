using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// For Writing into JSON Files
using System.IO;
// For communicating with json bin
using System.Net;

public class InventoryManager : MonoBehaviour
{

    public TextAsset orderList;
    public TextAsset inventoryList;

    public string url = "https://api.jsonbin.io/b/613cc1c5aa02be1d4446600e";

    // saves one item
    [System.Serializable]
    public class OrderItem
    {
        public string order;
        public int quantity;
        public string id;
        public string name;
        public string category;
        public int[] location;
    }

    [System.Serializable]
    public class InventoryItem
    {
        public string id;
        public string name;
        public string category;
        public int[] location;
    }

    [System.Serializable]
    // Stores the items in an array
    // to process the data of inventory and order list
    public class ItemListOrder
    {
        public List<OrderItem> orderItem;
    }

    [System.Serializable]
    public class ItemListInventory
    {
        public List<InventoryItem> inventoryItem;
    }

    public ItemListOrder order = new ItemListOrder();
    public ItemListInventory inventory = new ItemListInventory();

    // Start is called before the first frame update
    void Start()
    {
        // Read local order list
        // string orderStr = orderList.text;
        // Read web order list
        string orderStr = readJSONfromURL(url);
        // read json files and store them in the lists
        order = JsonUtility.FromJson<ItemListOrder>(orderStr);
        // Storage file in local Json file
        string curOrderList = JsonUtility.ToJson(order, true);
        File.WriteAllText(Application.dataPath + "/Database/order_list.json", curOrderList);
        // Read inventory from local database
        inventory = JsonUtility.FromJson<ItemListInventory>(inventoryList.text);
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log("ITEM: " + order.item[0].id);
        // User finishes one order item
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (order.orderItem.Count > 0)
            {
                OrderItem curItem = order.orderItem[0];
                // Convert order item into inventory Item by adopting the values of the current item
                InventoryItem newItem = new InventoryItem();
                newItem.id = curItem.id; newItem.name = curItem.name; newItem.category = curItem.category; newItem.location = curItem.location;
                // update the orderlist and the inventory
                updateDatabase(curItem, newItem);
            } else
            {
                Debug.Log("Order List does not contain items");
            }
            
        }
    }

    string readJSONfromURL(string url)
    {
        return new WebClient().DownloadString(url);
    }

    void writeJSONtoURL(string url, string data)
    {
        WebClient client = new WebClient();
        //string json = Newtonsoft.Json.JsonConvert.SerializeObject(items);
        //client.Headers[HttpRequestHeader.ContentType] = "application/json";
        //client.UploadValues("/api/example", "POST", json);

        var dataString = Newtonsoft.Json.JsonConvert.SerializeObject(data);
        client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
        client.UploadString(url, "POST", data);

    }

    void updateDatabase(OrderItem curItem, InventoryItem newItem)
    {
        // Pick Process
        if (curItem.order == "pick")
        {
            // Repeat quantity times
            for (int i = 0; i < curItem.quantity; i++)
            {
                // Search for element in enventory list with same id and location as current item in order list
                InventoryItem result = inventory.inventoryItem.Find(x => (
                x.id == newItem.id &&
                x.location[0] == newItem.location[0] && x.location[1] == newItem.location[1] && x.location[2] == newItem.location[2])
                );
                // Delete Item from inventory
                inventory.inventoryItem.Remove(result);
            }
        }

        // Place Process
        else if (curItem.order == "place")
        {
            // Repeat for quantity times
            for (int i = 0; i < curItem.quantity; i++)
            {
                // Add item to inventory list
                inventory.inventoryItem.Add(newItem);
            }
        }

        // Delete Item from order list
        order.orderItem.Remove(curItem);

        // Write updated information to json files
        string newInventory = JsonUtility.ToJson(inventory, true);
        File.WriteAllText(Application.dataPath + "/Database/inventory.json", newInventory);
        string newOrderList = JsonUtility.ToJson(order, true);
        File.WriteAllText(Application.dataPath + "/Database/order_list.json", newOrderList);
        // Upload also to the internet (DOES NOT WORK)
        //writeJSONtoURL(url, newOrderList);
    }
}