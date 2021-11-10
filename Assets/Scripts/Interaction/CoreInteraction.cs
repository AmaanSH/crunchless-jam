using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractionType
{
    Open = 1,
    RequireItem = 2,
    RequireEvent = 3,
    Pickup = 4
}

public class CoreInteraction : MonoBehaviour
{
    public string id;
    public string friendlyName;

    public InteractionType interactionType;
    public string interactString;

    public virtual void Interact()
    {
   
    }

    public virtual bool CanInteract()
    {
        return true;
    }

    public string GetInteractionString()
    {
        return string.Format("{0} {1}", interactString, friendlyName);
    }
}
