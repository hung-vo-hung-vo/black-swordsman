using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] ItemUI _itemPrefab;

    List<ItemUI> _items;

    private void Awake()
    {
        _items = GetComponentsInChildren<ItemUI>().ToList();
    }
}