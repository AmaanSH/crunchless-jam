using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    private List<Item> items = new List<Item>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public static Item GetItem(string id)
    {
        if (instance)
        {
            return instance.items.Find(x => x.id == id);
        }

        return null;
    }

    public static void PickupItem(Item item)
    {
        if (instance)
        {
            int currentPickedUp = instance.items.FindAll(x => x.id == item.id).Count;
            if (currentPickedUp > 0)
            {
                if (item.pickupMultiple)
                {
                    Debug.LogFormat("User picked up {0}", item.friendlyName);
                    instance.items.Add(item);
                }
            }
            else
            {
                Debug.LogFormat("User picked up {0}", item.friendlyName);
                instance.items.Add(item);
            }
        }
    }

    public static void RemoveItem(string id)
    {
        if (instance)
        {
            if (instance.items.Count > 0)
            {
                Item item = instance.items.Find(x => x.id == id);
                if (item != null)
                {
                    Debug.LogFormat("Removing {0} from user", item.friendlyName);

                    instance.items.Remove(item);
                }
            }
        }
    }
}
