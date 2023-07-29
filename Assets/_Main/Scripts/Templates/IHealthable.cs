using UnityEngine.Events;

public interface IHealthable
{
    UnityEvent OnDie();
    UnityEvent OnTakeDamage();
}