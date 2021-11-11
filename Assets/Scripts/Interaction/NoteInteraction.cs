using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteInteraction : CoreInteraction
{
    public string noteText;
    public override void Interact()
    {
        UIController.SetupUI(UIType.Note, noteText);
    }
}
