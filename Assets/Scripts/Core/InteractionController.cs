using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    public LayerMask mask;
    public float interactionDistance;
    public Transform checkFrom;

    private CoreInteraction _interaction;

    public void CheckForInteractableObjects()
    {
        bool inRange = Physics.Raycast(checkFrom.position, checkFrom.forward, out var hit, interactionDistance, mask);
        if (inRange)
        {
            CoreInteraction interaction = hit.collider.transform.root.GetComponent<CoreInteraction>();
            if (interaction == null)
            {
                interaction = (hit.collider.transform.parent.GetComponent<CoreInteraction>() != null) ? hit.collider.transform.parent.GetComponent<CoreInteraction>() : hit.collider.transform.GetComponent<CoreInteraction>();
            }

            if (interaction != null && !interaction.disabled)
            {
                ClearInteraction();

                _interaction = interaction;

                SetupInteractionPrompt(interaction);
            }
        }
        else
        {
            ClearInteraction();
        }
    }

    void SetupInteractionPrompt(CoreInteraction interaction)
    {
        UIController.SetupUI(UIType.Interaction, KeyCode.E.ToString(), interaction.GetInteractionString());

        if (!interaction.CanInteract())
        {
            interaction.SetOutlineColor(Color.red);
        }
        else
        {
            interaction.SetOutlineColor(Color.white);
        }

        interaction.Highlight(true);
    }

    void ClearInteraction()
    {
        if (_interaction != null)
        {
            UIController.HideUI(UIType.Interaction);

            _interaction.Highlight(false);

            _interaction = null;
        }
    }

    void HandleInteraction()
    {
        if (_interaction && _interaction.CanInteract())
        {
            _interaction.Interact();

            ClearInteraction();
        }
    }

    public void OnInteraction()
    {
        Debug.Log("User pressed interact key");

        HandleInteraction();
    }
}
