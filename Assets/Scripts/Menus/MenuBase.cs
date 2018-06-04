using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This menu base allows you to toggle through the columns in your menu.
/// Simply drag and drop the Text items into the lists of columns. A column that comes
/// after another column, will be considered a submenu of the previous column and therefore
/// the previous column will remain highlighted to indicate which submenu you are looking at.
/// To turn this feature off, please set "subMenus" to false;
/// To invoke actions on your menu items, please attach a Button or a MenuAction component to them.
/// By default, it scrolls through all items in the list. You should change the length of the list
/// dynamically when adding and removing items from the list such as inventory.
/// </summary>
public abstract class MenuBase : MonoBehaviour
{
    // Adjustable settings
    public bool canOpen = true;
    public bool subMenus = true;
    [SerializeField]
    protected float quickScrollDelay = 0.15f;
    [SerializeField]
    protected Color unselectedColor = Color.white;
    [SerializeField]
    protected Color selectedColor = Color.yellow;
    [SerializeField]
    protected AudioClip menuToggle;
    [SerializeField]
    protected AudioClip menuError;
    [SerializeField]
    protected AudioClip menuSelect;
    [SerializeField]
    protected AudioClip menuOpen;
    [SerializeField]
    protected AudioClip menuClose;

    // Internal variables
    [SerializeField, Tooltip("Set a size for columns then drag and drop menu Text objects that the user can scroll through.")]
    protected TextArrayContainer[] menuColumns;
    protected int col = 0;
    protected int row = 0;
    protected IEnumerator routine;

    protected List<Text> currentList
    {
        get
        {
            return menuColumns[col].innerList;
        }
    }

    protected Text currentText
    {
        get
        {
            return menuColumns[col].innerList[row];
        }
        set
        {
            menuColumns[col].innerList[row] = value;
        }
    }

    protected virtual void Awake()
    {
        if (menuColumns.Length == 0)
        {
            Debug.LogWarning("Menu Column Text objects were not set in the inspector");
        }
        else
        {
            foreach (TextArrayContainer cont in menuColumns)
            {
                if (cont.innerList.Count == 0)
                {
                    Debug.LogWarning("Inner menu Text objects were not set in the inspector");
                }
            }
        }
    }

    protected virtual void OnEnable()
    {
        row = 0;
        col = 0;
        SetMenuInput();
    }

    public void SetMenuInput()
    {
        Blackboard.Player.PlayerMovement.Up = () =>
        {
            if(routine == null || !routine.MoveNext())
            {
                toggleMenuUp();
                routine = PlayerMovement.HeldKeyStart(Blackboard.ControlMap.North, toggleMenuUp, quickScrollDelay);
                StartCoroutine(routine);
            }

            return Vector2.zero;
        };

        Blackboard.Player.PlayerMovement.Down = () =>
        {
            if (routine == null || !routine.MoveNext())
            {
                toggleMenuDown();
                routine = PlayerMovement.HeldKeyStart(Blackboard.ControlMap.South, toggleMenuDown, quickScrollDelay);
                StartCoroutine(routine);
            }

            return Vector2.zero;
        };

        Blackboard.Player.PlayerMovement.Right = () =>
        {
            if (routine == null || !routine.MoveNext())
            {
                toggleMenuRight();
                routine = PlayerMovement.HeldKeyStart(Blackboard.ControlMap.East, toggleMenuRight, quickScrollDelay);
                StartCoroutine(routine);
            }

            return Vector2.zero;
        };

        Blackboard.Player.PlayerMovement.Left = () =>
        {
            if (routine == null || !routine.MoveNext())
            {
                toggleMenuLeft();
                routine = PlayerMovement.HeldKeyStart(Blackboard.ControlMap.West, toggleMenuLeft, quickScrollDelay);
                StartCoroutine(routine);
            }

            return Vector2.zero;
        };

        Blackboard.Player.PlayerMovement.Interact = selectText;
        Blackboard.Player.PlayerMovement.Menu = closeMenu;
    }

    protected virtual void toggleMenuUp()
    {
        if (row > 0)
        {
            currentText.color = unselectedColor;
            row--;
            currentText.color = selectedColor;
            Blackboard.Audio.PlayAudio(menuToggle);
        }
        else
        {
            Blackboard.Audio.PlayAudio(menuError);
        }
    }

    protected virtual void toggleMenuDown()
    {
        if (row < (menuColumns[col].innerList.Count - 1))
        {
            currentText.color = unselectedColor;
            row++;
            currentText.color = selectedColor;
            Blackboard.Audio.PlayAudio(menuToggle);
        }
        else
        {
            Blackboard.Audio.PlayAudio(menuError);
        }
    }

    protected virtual void toggleMenuRight()
    {
        if (col < (menuColumns.Length - 1) && (menuColumns[col+1].innerList.Count > 0))
        {
            if (!subMenus)
            {
                currentText.color = unselectedColor;
            }
            col++;
            row = 0;
            currentText.color = selectedColor;
            Blackboard.Audio.PlayAudio(menuToggle);
        }
        else
        {
            Blackboard.Audio.PlayAudio(menuError);
        }
    }

    protected virtual void toggleMenuLeft()
    {
        if (col > 0)
        {
            currentText.color = unselectedColor;
            col--;
            row = 0;
            foreach (Text t in menuColumns[col].innerList)
            {
                t.color = unselectedColor;
            }
            currentText.color = selectedColor;
            Blackboard.Audio.PlayAudio(menuToggle);
        }
        else
        {
            Blackboard.Audio.PlayAudio(menuError);
        }
    }

    public virtual void closeMenu()
    {
        row = 0;
        col = 0;
        resetAllTextColor();
        gameObject.SetActive(false);
        Blackboard.Player.PlayerMovement.ResetMovementKeys();
    }

    public void Quit()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }

    /// <summary>
    /// Attempts to find a Button component on the Text object currently highlighted.
    /// If there is one, invoke it.
    /// </summary>
    protected void selectText()
    {
        if (currentText.GetComponent<Button>())
        {
            currentText.GetComponent<Button>().onClick.Invoke();
        }
        else
        {
            Debug.Log("The currently selected menu Text does not have a Button to invoke.");
        }
    }

    protected void resetAllTextColor()
    {
        foreach (TextArrayContainer column in menuColumns)
        {
            foreach (Text t in column.innerList)
            {
                t.color = unselectedColor;
            }
        }
    }
}