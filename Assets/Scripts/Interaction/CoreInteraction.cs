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
    public bool disableOnInteract;

    public InteractionType interactionType;
    public string interactString;

    [HideInInspector] public bool interactable;
    [HideInInspector] public bool disabled;

    public virtual void Interact()
    {
        if (disableOnInteract)
        {
            gameObject.SetActive(false);
        }
    }

    public virtual bool CanInteract()
    {
        return interactable;
    }

    public virtual string GetInteractionString()
    {
        return string.Format("{0} {1}", interactString, friendlyName);
    }
}
