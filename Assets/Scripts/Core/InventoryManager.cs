using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private List<Item> items = new List<Item>(); // the current items a player has

    public Item GetItem(string id)
    {
        return items.Find(x => x.id == id);
    }

    public void PickupItem(Item item)
    {
        int currentPickedUp = items.FindAll(x => x.id == item.id).Count;
        if (currentPickedUp > 0)
        {
            if (item.pickupMultiple)
            {
                Debug.LogFormat("User picked up {0}", item.friendlyName);
                items.Add(item);
            }
        }
        else
        {
            Debug.LogFormat("User picked up {0}", item.friendlyName);
            items.Add(item);
        }
    }

    public void RemoveItem(string id)
    {
        if (items.Count > 0)
        {
            Item item = items.Find(x => x.id == id);
            if (item != null)
            {
                Debug.LogFormat("Removing {0} from user", item.friendlyName);

                items.Remove(item);
            }
        }
    }
}
