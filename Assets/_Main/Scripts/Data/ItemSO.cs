using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Data/Item", order = 0)]
public class ItemSO : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public float Number { get; private set; }
    [field: SerializeField] public ItemAction Action { get; private set; }
}