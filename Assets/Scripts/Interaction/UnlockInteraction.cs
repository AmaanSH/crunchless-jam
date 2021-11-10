using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockInteraction : CoreInteraction
{
    public string requiredItem;

    // TODO: setup something to happen?

    public override void Interact()
    {
        Item item = InventoryManager.GetItem(requiredItem);
        if (item)
        {
            Debug.Log("Unlocking");
            InventoryManager.RemoveItem(requiredItem);

            disabled = true;
        }
    }

    public override bool CanInteract()
    {
        Item item = InventoryManager.GetItem(requiredItem);
        if (item)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override string GetInteractionString()
    {
        if (!CanInteract())
        {
            return string.Format("<color=\"red\">Need {0}</color>", requiredItem);
        }
        else
        {
            return base.GetInteractionString();
        }
    }
}
