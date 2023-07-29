using UnityEngine.Events;

public interface IHudable
{
    UnityEvent<float> OnHealthChanged();
    UnityEvent<float> OnManaChanged();
}