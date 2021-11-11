using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum UIType
{
    Interaction,
    Note,
    Menu
}

public enum DisplayType
{
    Fullscreen,
    Interaction
}

[System.Serializable]
public class UI
{
    public CoreUI ui;
    public UIType type;
    public DisplayType displayType;
}

public class UIController : MonoBehaviour
{
    public static UIController instance;

    [SerializeField]
    private List<UI> gameUI = new List<UI>();

    private List<UI> stack = new List<UI>();
    private bool cursorLocked = true;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        for (int i = 0; i < gameUI.Count; i++)
        {
            UI ui = gameUI[i];
            ui.ui.Hide();
        }
    }

    public static bool BlockMovement()
    {
        if (instance)
        {
            UI ui = instance.GetActive();
            if (ui != null)
            {
                return ui.displayType == DisplayType.Fullscreen;
            }
        }

        return false;
    }

    public static void SetupUI(UIType type, params string[] data)
    {
        if (instance)
        {
            UI uiObj = instance.GetUIByType(type);
            if (uiObj != null)
            {
                uiObj.ui.Setup(data);

                if (uiObj.displayType == DisplayType.Fullscreen)
                {
                    instance.cursorLocked = false;
                    instance.SetCursorState(false);
                }
                else
                {
                    instance.cursorLocked = true;
                    instance.SetCursorState(true);
                }

                instance.stack.Add(uiObj);
            }
        }
    }

    public static void HideUI(UIType type)
    {
        if (instance && instance.stack.Count > 0)
        {
            UI activeUI = instance.stack.Find(x => x.type == type);
            if (activeUI != null)
            {
                activeUI.ui.Hide();

                instance.RemoveFromStack(activeUI);
            }
        }
    }

    public static void HideUI()
    {
        if (instance)
        {
            UI activeUI = instance.stack.Last();
            if (activeUI != null)
            {
                activeUI.ui.Hide();

                instance.RemoveFromStack(activeUI);
            }
        }
    }

    private UI GetUIByType(UIType type)
    {
        return gameUI.Find(x => x.type == type);
    }

    private void RemoveFromStack(UI ui)
    {
        if (ui.displayType == DisplayType.Fullscreen)
        {
            instance.SetCursorState(true);
        }

        stack.Remove(ui);
    }

    private UI GetActive()
    {
        if (stack.Count > 0)
        {
            return stack.Last();
        }

        return null;
    }

    private void SetCursorState(bool newState)
    {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        SetCursorState(cursorLocked);
    }
}
