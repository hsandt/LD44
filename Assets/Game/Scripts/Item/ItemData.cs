using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Data/Item Data", order = 1)]
public class ItemData : ScriptableObject
{
    public string itemName = "";
    public string description = "";
}
