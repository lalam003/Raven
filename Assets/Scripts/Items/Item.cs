using UnityEngine;

[CreateAssetMenu(menuName = "New Item")]
public class Item : ScriptableObject
{
    [SerializeField]
    private string itemKey = "";
    public string ItemKey
    {
        get
        {
            return itemKey;
        }
    }

    [SerializeField]
    private bool isNote = false;
    public bool IsNote
    {
        get
        {
            return isNote;
        }
    }

    [SerializeField]
    private Sprite sprite;
    public Sprite Sprite
    {
        get
        {
            return sprite;
        }
    }

    [SerializeField, Range(1, 999)]
    private int maxAmount = 1;
    public int MaxAmount
    {
        get
        {
            return maxAmount;
        }
    }

    [SerializeField, Multiline]
    private string description = "";
    public string Description
    {
        get
        {
            return description;
        }
    }
}