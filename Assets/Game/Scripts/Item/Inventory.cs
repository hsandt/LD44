using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using CommonsHelper;

public class Inventory : MonoBehaviour
{
    public delegate void ChangeHandler(Item[] newItems);
    public event ChangeHandler ChangeEvent;
        
    [SerializeField, ReadOnlyField, Tooltip("Array of all items that can be possessed (some may have quantity 0)")]
    private Item[] items = null;
    
    void Awake()
    {
        Init();
    }

    private void Init()
    {
        // load all item data
        ItemData[] itemDataArr = Resources.LoadAll<ItemData>("Item");
        
        // initialize all items with item data, with default state (quantity 0, etc.)
        items = itemDataArr.Select(itemData => new Item(itemData)).ToArray();
        
        OnChanged();
    }

    void Start()
    {

    }

    void Setup()
    {

    }

    private void OnChanged()
    {
        ChangeEvent?.Invoke(items);
    }
}
