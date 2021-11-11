using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreUI : MonoBehaviour
{
    public virtual void Setup(params string[] data)
    {

    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
}
