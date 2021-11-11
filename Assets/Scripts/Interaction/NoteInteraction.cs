using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteInteraction : CoreInteraction
{
    public string noteText;

    private bool viewing = false;
    public override void Interact()
    {
        UIController.SetupUI(UIType.Note, noteText);
    }
}
