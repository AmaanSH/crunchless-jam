using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractionPanel : CoreUI
{
    public TextMeshProUGUI keyText;
    public TextMeshProUGUI interactText;

    public override void Setup(params string[] data)
    {
        string key = data[0];
        string interact = data[1];

        keyText.text = string.Format("[{0}]", key);
        interactText.text = interact;

        gameObject.SetActive(true);
    }
}
