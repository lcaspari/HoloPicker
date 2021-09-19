using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// For Writing into JSON Files
using System.IO;
// For communicating with json bin
using System.Net;

public class InventoryManager : MonoBehaviour
{

    // Variables for the Order process
    public bool done = false;

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
        public int location;
    }

    [System.Serializable]
    public class InventoryItem
    {
        public string id;
        public string name;
        public string category;
        public int location;
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

    }

    // This operation takes the next item from the list and operates on the database and the frames to make a pick/place process
    // Is called when the user has started the order Process or finished the last item (by the OrderMenu)


    bool first = true;
    public void processItem()
    {


        if (order.orderItem.Count > 0 && first == false)
        {

            OrderItem curItem = order.orderItem[0];
            // Convert order item into inventory Item by adopting the values of the current item
            InventoryItem newItem = new InventoryItem();
            newItem.id = curItem.id; newItem.name = curItem.name; newItem.category = curItem.category; newItem.location = curItem.location;
            // update the orderlist and the inventory
            updateDatabase(curItem, newItem);

            GameObject.Find(curItem.location.ToString()).SetActive(true);

        }
        else if (order.orderItem.Count > 0 && first == true)
        {
            OrderItem curItem = order.orderItem[0];
            GameObject.Find(curItem.location.ToString()).SetActive(true);
            first = false;
        }
        else
        {
            Debug.Log("Order List does not contain items");
        }
    }

    string readJSONfromURL(string url)
    {
        // Download latest version of the string
        return new WebClient().DownloadString(url + "/latest");
    }

    void writeJSONtoURL(string url, string data)
    {
        WebClient client = new WebClient();
        // Add the content type and the key to the head, so webclient knows which bin to use
        client.Headers.Add("Content-Type", "application/json");
        client.Headers.Add("X-Master-Key", "$2b$10$7XBSMFNLrINX/pZ7qH1J3evt.HcS.47jSOr.pzIVqZEFnPzYfBCEa");
        // Upload new data
        client.UploadString(url, "PUT", data);
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
                x.location == newItem.location)
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
        // Upload also to the internet
        writeJSONtoURL(url, newOrderList);
    }
}