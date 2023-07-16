using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    Dictionary<ItemSO, int> _items = new Dictionary<ItemSO, int>();

    public ItemSO GetItemByAction(ItemAction action)
    {
        return _items.Keys.Where(item => item.Action == action && _items[item] > 0).FirstOrDefault();
    }

    public void AddItem(ItemSO item)
    {
        if (!_items.ContainsKey(item))
        {
            _items[item] = 1;
            return;
        }

        _items[item]++;
    }

    public void UseItem(ItemSO item)
    {
        _items[item]--;
        if (_items[item] <= 0)
        {
            _items.Remove(item);
        }
    }
}