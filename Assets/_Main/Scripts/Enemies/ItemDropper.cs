using System.Collections.Generic;
using UnityEngine;

public class ItemDropper : MonoBehaviour
{
    [SerializeField] List<ItemSO> _items = new List<ItemSO>();
    [SerializeField] int maximumDrop = 3;
    [SerializeField] Item _itemPrefab;

    Transform _dropPoint;

    public void SetDropPoint(Transform dropPoint)
    {
        _dropPoint = dropPoint;
    }

    public void DropItem()
    {
        if (_items.Count == 0)
        {
            return;
        }

        var data = _items[Random.Range(0, _items.Count)];

        var cnt = Random.Range(1, maximumDrop + 1);
        for (int i = 0; i < cnt; i++)
        {
            var item = Instantiate(_itemPrefab, _dropPoint.position, Quaternion.identity);
            item.SetItem(data);
        }
    }
}