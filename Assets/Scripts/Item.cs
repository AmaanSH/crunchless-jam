using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Item : CoreInteraction
{
    public bool pickupMultiple;
    public Sprite sprite;

    public override void Interact()
    {
        if (InventoryManager.instance)
        {
            InventoryManager.PickupItem(this);

            disabled = true;

            base.Interact();

            TriggerEvent(triggerEvent);
        }
    }
}
