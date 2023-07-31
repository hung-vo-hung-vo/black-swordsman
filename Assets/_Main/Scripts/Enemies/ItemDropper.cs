using System.Collections.Generic;
using UnityEngine;

public class ItemDropper : MonoBehaviour
{
    [SerializeField] List<ItemSO> _items = new List<ItemSO>();
    [SerializeField] Item _itemPrefab;
    [SerializeField] Transform _dropPoint;

    public void DropItem()
    {
        if (_items.Count == 0)
        {
            return;
        }

        var data = _items[Random.Range(0, _items.Count)];

        var item = Instantiate(_itemPrefab, _dropPoint.position, Quaternion.identity);
        item.SetItem(data);
    }
}