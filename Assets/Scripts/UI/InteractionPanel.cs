using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractionPanel : MonoBehaviour
{
    public TextMeshProUGUI keyText;
    public TextMeshProUGUI interactText;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void Setup(string key, string interact)
    {
        keyText.text = string.Format("[{0}]", key);
        interactText.text = interact;

        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
