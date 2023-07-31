using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    Dictionary<ItemSO, int> _items = new Dictionary<ItemSO, int>();

    [SerializeField] ItemSO _debugHP, _debugMP;
    [SerializeField] int _debugAmount;

    private void Awake()
    {
        if (GameManager.IsDebug())
        {
            _items[_debugHP] = _debugAmount;
            _items[_debugMP] = _debugAmount;
        }
    }

    public ItemSO GetItemByAction(ItemAction action)
    {
        return _items.Keys.Where(item => item.Action == action && _items[item] > 0).FirstOrDefault();
    }

    public int GetItemNumberByAction(ItemAction action)
    {
        var number = 0;
        foreach (var p in _items)
        {
            if (p.Key.Action == action && p.Value > 0)
            {
                number += p.Value;
            }
        }

        return number;
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