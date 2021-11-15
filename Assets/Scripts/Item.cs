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

    public override void OnEvent(params object[] data)
    {
        DoorState state = (DoorState)data[0];

        switch(state)
        {
            case DoorState.Closed:
            case DoorState.Locked:
                disabled = true;
                break;
            default:
                disabled = false;
                break;
        }
    }
}
