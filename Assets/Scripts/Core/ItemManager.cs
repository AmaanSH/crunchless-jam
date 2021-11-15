using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private List<GameObject> gameItems = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
        Debug.LogFormat("Found items {0}", items.Length);

        gameItems.AddRange(items);

        for (int i = 0; i < gameItems.Count; i++)
        {
            CoreInteraction interaction = gameItems[i].GetComponent<CoreInteraction>();
            if (interaction != null)
            {
                interaction.Init();
            }
        }
    }
}
