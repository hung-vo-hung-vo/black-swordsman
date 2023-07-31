using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] ItemUI _itemPrefab;

    List<ItemUI> _items;
    Inventory _inventory;

    private void Awake()
    {
        _items = GetComponentsInChildren<ItemUI>().ToList();
    }

    public UnityAction SetInventory(Inventory inventory)
    {
        _inventory = inventory;
        UpdateUI();
        return UpdateUI;
    }

    void UpdateUI()
    {
        var actions = new ItemAction[] { ItemAction.HP, ItemAction.MP, ItemAction.Damage, ItemAction.JumpForce };
        NormalizeListItem(actions.Length);
        for (int i = 0; i < actions.Length; i++)
        {
            var item = _inventory.GetItemByAction(actions[i]);
            if (item == null)
            {
                _items[i].gameObject.SetActive(false);
                continue;
            }

            var number = _inventory.GetItemNumberByAction(actions[i]);

            _items[i].gameObject.SetActive(true);
            _items[i].SetItem(item, number);
        }
    }

    void NormalizeListItem(int actCount)
    {
        if (_items.Count < actCount)
        {
            var cnt = actCount - _items.Count;
            for (int i = 0; i < cnt; i++)
            {
                var item = Instantiate(_itemPrefab, transform);
                _items.Add(item);
            }
        }
        else if (_items.Count > actCount)
        {
            var cnt = _items.Count - actCount;
            for (int i = 0; i < cnt; i++)
            {
                var item = _items.Last();
                _items.Remove(item);
                Destroy(item.gameObject);
            }
        }
    }
}