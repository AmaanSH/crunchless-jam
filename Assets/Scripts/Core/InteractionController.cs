using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    public LayerMask mask;
    public float interactionDistance;
    public Transform checkFrom;
    public InteractionPanel interactionPanel;

    private CoreInteraction _interaction;

    public void CheckForInteractableObjects()
    {
        bool inRange = Physics.Raycast(checkFrom.position, checkFrom.forward, out var hit, interactionDistance, mask);
        if (inRange)
        {
            if (!_interaction)
            {
                CoreInteraction interaction = hit.collider.gameObject.GetComponent<CoreInteraction>();
                if (interaction != null && !interaction.disabled)
                {
                    _interaction = interaction;

                    SetupInteractionPrompt(interaction);
                }
            }
        }
        else
        {
            ClearInteraction();
        }
    }

    void SetupInteractionPrompt(CoreInteraction interaction)
    {
        if (interactionPanel)
        {
            interactionPanel.Setup(KeyCode.E.ToString(), interaction.GetInteractionString());
        }
    }

    void ClearInteraction()
    {
        if (interactionPanel)
        {
            _interaction = null;

            interactionPanel.Hide();
        }
    }

    void HandleInteraction()
    {
        if (_interaction)
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
