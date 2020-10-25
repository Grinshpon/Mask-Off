using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item")]
public class Item : ScriptableObject
{
  public enum ItemType
  {
    Throwable,
    Consumable,
    Usable,
    Stash,
  }
  public string itemName = "item";
  public ItemType itemType = ItemType.Stash;

  public void UseItem()
  {
    switch (itemType)
    {
      case ItemType.Throwable:
        Debug.Log("Threw " + itemName);
        break;
      case ItemType.Consumable:
        Debug.Log("Consumed " + itemName);
        break;
      case ItemType.Usable:
        Debug.Log("Used " + itemName);
        break;
      case ItemType.Stash: default:
        break;
    }
  }
}
