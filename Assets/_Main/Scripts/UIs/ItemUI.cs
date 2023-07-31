using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    [SerializeField] Image _icon;
    [SerializeField] TextMeshProUGUI _number;

    ItemSO _data;

    public void SetItem(ItemSO data, int number)
    {
        _data = data;
        _icon.sprite = _data == null ? null : _data.Icon;
        _number.text = _data == null ? string.Empty : number.ToString();
    }
}