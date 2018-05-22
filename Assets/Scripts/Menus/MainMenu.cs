using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MenuBase
{
    // References
    [SerializeField] private RectTransform clockHourHand;
    [SerializeField] private RectTransform clockMinuteHand;
    [SerializeField] private Text dateText;
    [SerializeField] private Text clockDigital;
    [SerializeField] private Text uRTitle;
    [SerializeField] private GameObject notePaper;
    [SerializeField] private Text noteText;
    [SerializeField] private Image itemSprite;
    [SerializeField] private Text itemDescription;
    [SerializeField] private ScrollRect scrollView;
    [SerializeField] private GameObject menuTextPrefab;
    [SerializeField] private List<Text> menuItemQtys = new List<Text>();
    [SerializeField] private List<MenuButtonData> SaveEvents = new List<MenuButtonData>();
    [SerializeField] private List<MenuButtonData> OptionsEvents = new List<MenuButtonData>();
    [SerializeField] private List<MenuButtonData> QuitEvents = new List<MenuButtonData>();

    private int menuListLength;
    private int categoryIndex = 0;

    public void ClickedCategory(int clickedRow)
    {
        currentText.color = unselectedColor;
        col = 0;
        row = clickedRow;
        categoryIndex = clickedRow;
        clearListText(menuColumns[1].innerList);
        clearQty();
    }

    public void ToggleLeft()
    {
        toggleMenuLeft();
    }

    public void ToggleRight()
    {
        toggleMenuRight();
    }

    public void ItemOnClick(Item clickedItem)
    {
        clearItemDisplay();
        if (clickedItem.IsNote)
        {
            notePaper.SetActive(true);
            noteText.text = clickedItem.Description;
        }
        else
        {
            itemSprite.gameObject.SetActive(true);
            itemSprite.sprite = clickedItem.Sprite;
            itemDescription.text = clickedItem.Description;
        }
        audioManager.PlayAudio(menuSelect);
    }

    protected override void Awake()
    {
        base.Awake();
        Blackboard.Menu = this;
        menuListLength = currentList.Count;
        clearItemDisplay();
        gameObject.SetActive(false);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        resetScrollPosition();
        clearListText(menuColumns[1].innerList);
        clearQty();
        currentText.color = selectedColor;
        audioManager.PlayAudio(menuOpen);
    }

     protected void Update()
    {
        updateClockHands();
        updateClockDigital();
    }

    private void updateClockHands()
    {
        clockHourHand.localRotation = Quaternion.Euler(0, 0, -360 * TimeSystem.Hour / 12);
        clockMinuteHand.localRotation = Quaternion.Euler(0, 0, -360 * TimeSystem.Minute / 60);
    }

    private void updateClockDigital()
    {
        dateText.text = TimeSystem.Month.ToString() + '\n' + TimeSystem.Day.ToString();
        clockDigital.text = TimeSystem.Hour + ":" 
            + (TimeSystem.Minute < 10 ? "0" + TimeSystem.Minute : TimeSystem.Minute.ToString()) 
            + ((TimeSystem.isMorning) ? "am" : "pm");
    }

    protected override void toggleMenuDown()
    {
        if (row < (menuListLength - 1))
        {
            currentText.color = unselectedColor;
            row++;
            currentText.color = selectedColor;
            audioManager.PlayAudio(menuToggle);
        }
        else
        {
            audioManager.PlayAudio(menuError);
        }
    }

    protected override void toggleMenuLeft()
    {
        if (col > 0)
        {
            resetScrollPosition();
            clearQty();
            clearListText(currentList);
            currentText.color = unselectedColor;
            col--;
            row = categoryIndex;
            foreach (Text t in menuColumns[col].innerList)
            {
                t.color = unselectedColor;
            }
            currentText.color = selectedColor;
            menuListLength = currentList.Count;
            clearItemDisplay();
            audioManager.PlayAudio(menuToggle);
        }
        else
        {
            audioManager.PlayAudio(menuError);
        }
    }

    protected override void toggleMenuRight()
    {
        if (col < (menuColumns.Length - 1) && (menuColumns[col + 1].innerList.Count > 0))
        {

            categoryIndex = row;
            uRTitle.text = currentText.text;
            if (!subMenus)
            {
                currentText.color = unselectedColor;
            }
            col++;
            row = 0;
            currentText.color = selectedColor;
            displaySubmenu();
            audioManager.PlayAudio(menuSelect);
        }
        else
        {
            audioManager.PlayAudio(menuError);
        }
    }

    protected override void closeMenu()
    {
        resetScrollPosition();
        clearItemDisplay();
        audioManager.PlayAudio(menuClose);
        base.closeMenu();
    }

    private void clearListText(List<Text> innerList)
    {
        foreach (Text t in innerList)
        {
            t.text = "";
        }
    }

    private void clearQty()
    {
        uRTitle.text = "";
        clearListText(menuItemQtys);
    }

    private void displaySubmenu()
    {
        switch (categoryIndex)
        {
            case 0: // Inventory
                displayInventory();                
                break;
            case 1: // Save
                fillTextFields(SaveEvents);
                break;
            case 2: // Options
                fillTextFields(OptionsEvents);
                break;
            case 3: // Quit
                fillTextFields(QuitEvents);
                break;
            default:
                Debug.LogWarning("Menu category "+categoryIndex+"not recognized.");
                break;
        }
    }

    /// <summary>
    /// Fills the current column with items in inventory and sets the dynamic menuItemLengh.
    /// </summary>
    private void displayInventory()
    {
        clearListText(currentList);
        clearListText(menuItemQtys);

        Dictionary<Item, uint> items = Blackboard.Player.GetComponent<Inventory>().Items;

        if (items.Count == 0)
        {
            displayEmpty();
        }
        else
        {
            // If there are not enough Text objects in the menu for all items, instantiate more
            while (currentList.Count < items.Count || menuItemQtys.Count < items.Count)
            {
                GameObject newItemText = Instantiate(menuTextPrefab, scrollView.content);
                currentList.Add(newItemText.transform.GetChild(0).GetComponent<Text>());
                menuItemQtys.Add(newItemText.transform.GetChild(1).GetComponent<Text>());
            }
            // display the items and update their button
            int index = 0;
            foreach (KeyValuePair<Item, uint> entry in items)
            {
                currentList[index].text = entry.Key.name;
                menuItemQtys[index].text = entry.Value.ToString();
                // Add the function to call when this tiem's button is clicked
                currentList[index].GetComponent<Button>().onClick.AddListener(() => ItemOnClick(entry.Key));
                index++;
            }
            menuListLength = Blackboard.Player.GetComponent<Inventory>().Items.Count;
        }
    }

    private void fillTextFields(List<MenuButtonData> events)
    {
        if (events.Count == 0)
        {
            displayEmpty();
        }
        else
        {
            // If there are not enough Text objects in the menu for all items, instantiate more
            while (currentList.Count < events.Count)
            {
                GameObject newItemText = Instantiate(menuTextPrefab, scrollView.content);
                currentList.Add(newItemText.transform.GetChild(0).GetComponent<Text>());
            }
            // display all items and update their button with passed-in events
            int eventsCount = events.Count;
            for (int i = 0; i < eventsCount; ++i)
            {
                currentList[i].text = events[i].label;
                currentList[i].GetComponent<Button>().onClick.RemoveAllListeners();
                currentList[i].GetComponent<Button>().onClick = events[i].MyEvent;
            }
            menuListLength = events.Count;
        }
    }

    private void resetScrollPosition()
    {
        scrollView.viewport.anchorMin = Vector2.zero;
        scrollView.viewport.anchorMax = Vector2.one;
        scrollView.content.anchoredPosition = new Vector2(3, 0);
    }

    private void displayEmpty()
    {
        // Make sure there is at least one text box to display "empty"
        if (currentList.Count == 0)
        {
            GameObject newItemText = Instantiate(menuTextPrefab, scrollView.content);
            currentList.Add(newItemText.transform.GetChild(0).GetComponent<Text>());
        }
        currentList[0].text = "(empty)";
        menuListLength = 1;
    }

    private void clearItemDisplay()
    {
        itemSprite.gameObject.SetActive(false);
        itemDescription.text = "";
        notePaper.SetActive(false);
        noteText.text = "";
    }
}