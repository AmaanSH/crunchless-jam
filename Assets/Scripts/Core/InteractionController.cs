using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    public LayerMask mask;
    public float interactionDistance;
    public Transform checkFrom;
    public InteractionPanel interactionPanel;

    private bool _canInteract = false;

    public KeyCode interactionKey;

    public void CheckForInteractableObjects()
    {
        bool inRange = Physics.Raycast(checkFrom.position, checkFrom.forward, out var hit, interactionDistance, mask);
        if (inRange)
        {
            if (!_canInteract)
            {
                CoreInteraction interaction = hit.collider.gameObject.GetComponent<CoreInteraction>();
                if (interaction != null && interaction.CanInteract())
                {
                    Debug.LogFormat("Interaction in range: {0}", interaction.friendlyName);
                    _canInteract = true;

                    SetupInteractionPrompt(interaction);
                }
            }
        }
        else
        {
            _canInteract = false;
            ClearInteraction();
        }
    }

    void SetupInteractionPrompt(CoreInteraction interaction)
    {
        if (interactionPanel)
        {
            interactionPanel.Setup(interactionKey.ToString(), interaction.GetInteractionString());
        }
    }

    void ClearInteraction()
    {
        if (interactionPanel)
        {
            interactionPanel.Hide();
        }
    }
}
