using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: generalise name

public enum DoorState
{
    Locked,
    Unlocked,
    Open,
    Closed
}

public class DoorInteraction : CoreInteraction
{
    public string requiredItem;
    public Animator doorAnimator;

    [HideInInspector]
    public DoorState state;

    private void Start()
    {
        if (string.IsNullOrEmpty(requiredItem))
        {
            state = DoorState.Unlocked;
        }
    }

    public override void Interact()
    {
        switch(state)
        {
            case DoorState.Locked:
                if (!string.IsNullOrEmpty(requiredItem))
                {
                    if (InventoryManager.GetItem(requiredItem))
                    {
                        Animate();
                    }
                }
                break;
            default:
                Animate();
                break;
        }

        base.Interact();
    }

    public override void OnEvent(params object[] data)
    {
        disabled = false;

        EventManager.Unsubscribe(registerEvent, OnEvent);
    }

    public override bool CanInteract()
    {
        if (state != DoorState.Locked)
        {
            return true;
        }
        else if (!string.IsNullOrEmpty(requiredItem))
        {
            if (InventoryManager.GetItem(requiredItem))
            {
                return true;
            }
        }

        return false;
    }

    public override string GetInteractionString()
    {
        if (!CanInteract())
        {
            return (!string.IsNullOrEmpty(requiredItem)) ? string.Format("<color=\"red\">Need {0} to {1}</color>", requiredItem, interactString.ToLower()) : "Locked";
        }
        else
        {
            return (state == DoorState.Closed || state == DoorState.Unlocked) ? "Open" : "Close";
        }
    }

    public override void Animate()
    {
        if (doorAnimator != null)
        {
            if (state != DoorState.Locked)
            {
                switch (state)
                {
                    case DoorState.Unlocked:
                    case DoorState.Closed:
                        doorAnimator.SetTrigger("open");
                        state = DoorState.Open;
                        break;
                    default:
                        doorAnimator.SetTrigger("close");
                        state = DoorState.Closed;
                        break;
                }
            }

            TriggerEvent(state);
        }
    }
}
