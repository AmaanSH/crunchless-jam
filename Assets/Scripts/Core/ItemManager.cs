using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private List<CoreInteraction> gameItems = new List<CoreInteraction>();

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
        Debug.LogFormat("Found items {0}", items.Length);

        for (int i = 0; i < items.Length; i++)
        {
            CoreInteraction interaction = items[i].GetComponent<CoreInteraction>();
            if (interaction != null)
            {
                gameItems.Add(interaction);

                interaction.Init();
            }
        }
    }
}
