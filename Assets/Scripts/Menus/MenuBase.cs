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
    [SerializeField]
    protected GameManager gameManager;

    // Internal variables
    [SerializeField, Tooltip("Set a size for columns then drag and drop menu Text objects that the user can scroll through.")]
    protected TextArrayContainer[] menuColumns;
    protected int col = 0;
    protected int row = 0;
    protected KeyCode heldKey;
    protected delegate void toggleMenuDelegate();

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

    protected virtual void Update()
    {
        GetMenuInput();
    }

    protected virtual void OnEnable()
    {
        Blackboard.Player.CanMove = false;
    }

    protected void GetMenuInput()
    {
        if (Input.GetKeyDown(Blackboard.ControlMap.North))
        {
            toggleMenuUp();
            toggleMenuDelegate del = toggleMenuUp;
            StartCoroutine(heldKeyStart(Blackboard.ControlMap.North, del));
        }
        if (Input.GetKeyDown(Blackboard.ControlMap.South))
        {
            toggleMenuDown();
            toggleMenuDelegate del = toggleMenuDown;
            StartCoroutine(heldKeyStart(Blackboard.ControlMap.South, del));
        }
        if (Input.GetKeyDown(Blackboard.ControlMap.East))
        {
            toggleMenuRight();
        }
        if (Input.GetKeyDown(Blackboard.ControlMap.West))
        {
            toggleMenuLeft();
        }
        if (Input.GetKeyDown(Blackboard.ControlMap.Interact))
        {
            selectText();
        }
        if (Input.GetKeyDown(Blackboard.ControlMap.Menu))
        {
            closeMenu();
        }
    }

    protected virtual void toggleMenuUp()
    {
        if (row > 0)
        {
            currentText.color = unselectedColor;
            row--;
            currentText.color = selectedColor;
            gameManager.PlayAudio(menuToggle);
        }
        else
        {
            gameManager.PlayAudio(menuError);
        }
    }

    protected virtual void toggleMenuDown()
    {
        if (row < (menuColumns[col].innerList.Count - 1))
        {
            currentText.color = unselectedColor;
            row++;
            currentText.color = selectedColor;
            gameManager.PlayAudio(menuToggle);
        }
        else
        {
            gameManager.PlayAudio(menuError);
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
            gameManager.PlayAudio(menuToggle);
        }
        else
        {
            gameManager.PlayAudio(menuError);
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
            gameManager.PlayAudio(menuToggle);
        }
        else
        {
            gameManager.PlayAudio(menuError);
        }
    }

    protected virtual void closeMenu()
    {
        resetAllTextColor();
        gameObject.SetActive(false);
        Blackboard.Player.CanMove = true;
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

    /// <summary>
    /// While a menu toggle key is held down, wait 1 second before quick-scrolling
    /// </summary>
    /// <returns></returns>
    protected IEnumerator heldKeyStart(KeyCode key, toggleMenuDelegate toggleFunction)
    {
        float startTime = Time.unscaledTime;
        heldKey = key;
        // Wait one second before quick-scrolling
        while (Input.GetKey(key))
        {
            // wait 1 second before quick-toggling
            if (Time.unscaledTime - startTime < 0.5f)
            {
                yield return null;
            }
            else
            {
                // continue toggling 5 times per second
                toggleFunction();
                yield return new WaitForSecondsRealtime(quickScrollDelay);
            }
        }
    }
}