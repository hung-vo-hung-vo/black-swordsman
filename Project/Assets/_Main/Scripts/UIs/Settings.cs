using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] Slider _volume, _difficulty;

    private void Start()
    {
        _volume.value = GameManager.Instance.Audio.volume;
        _difficulty.value = GameManager.Instance.HardMode ? 1f : 0f;
    }

    public void SetVolume(float volume)
    {
        var vol = _volume.value;
        GameManager.Instance.Audio.volume = vol;
    }

    public void SetDifficulty(float hard)
    {
        GameManager.Instance.SetDifficulty(_difficulty.value >= 0.5f);
    }
}