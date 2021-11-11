using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NotePanel : CoreUI
{
    public TextMeshProUGUI text;

    public override void Setup(params string[] data)
    {
        text.text = data[0];

        gameObject.SetActive(true);
    }

    public void Close()
    {
        UIController.HideUI(UIType.Note);
    }
}
