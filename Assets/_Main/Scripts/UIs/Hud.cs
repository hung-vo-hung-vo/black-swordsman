using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    [SerializeField] Slider _healthBar;
    [SerializeField] Slider _manaBar;

    public void Init(IHudable hudable)
    {
        hudable.OnHealthChanged().AddListener((hp) => _healthBar.value = hp);
        hudable.OnManaChanged().AddListener((mp) => _manaBar.value = mp);
    }
}