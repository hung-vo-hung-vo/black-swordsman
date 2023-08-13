using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    [SerializeField] Slider _healthBar;
    [SerializeField] Slider _manaBar;

    public void Init(IHudable hudable)
    {
        hudable.OnHealthChanged().AddListener((hpPercent) => _healthBar.value = hpPercent);
        hudable.OnManaChanged().AddListener((mpPercent) => _manaBar.value = mpPercent);
    }
}