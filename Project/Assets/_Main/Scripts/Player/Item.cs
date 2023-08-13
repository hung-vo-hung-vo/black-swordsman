using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] SpriteRenderer _renderer;

    public ItemSO ItemData => _data;

    ItemSO _data;

    public void SetItem(ItemSO data)
    {
        _data = data;
        _renderer.sprite = _data.Icon;
    }
}