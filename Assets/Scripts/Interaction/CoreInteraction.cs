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

    public string registerEvent;
    public string triggerEvent;

    [HideInInspector] public bool interactable;
    [HideInInspector] public bool disabled;

    private Outline _outline;

    public void Init()
    {
        if (!string.IsNullOrEmpty(registerEvent))
        {
            EventManager.Subscribe(registerEvent, OnEvent);
            disabled = true;
        }

        _outline = GetComponent<Outline>();
        if (_outline != null)
        {
            _outline.enabled = false;
        }
    }

    private void OnDestroy()
    {
        if (!string.IsNullOrEmpty(registerEvent))
        {
            EventManager.Unsubscribe(registerEvent, OnEvent);
        }
    }

    public virtual void Interact()
    {
        if (disableOnInteract)
        {
            gameObject.SetActive(false);
        }

        if (!string.IsNullOrEmpty(registerEvent))
        {
            EventManager.Unsubscribe(registerEvent, OnEvent);
        }
    }

    public void Highlight(bool enabled)
    {
        if (_outline != null)
        {
            _outline.enabled = enabled;
        }
    }

    public void TriggerEvent(params object[] data)
    {
        if (!string.IsNullOrEmpty(triggerEvent))
        {
            EventManager.Trigger(triggerEvent, data);
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

    public virtual void OnEvent(params object[] data)
    {

    }

    public virtual void Animate()
    {

    }
}
