using UnityEngine;

public class Settings : MonoBehaviour
{
    public void SetVolume(float volume)
    {
        GameManager.Instance.Audio.volume = volume;
    }

    public void SetDifficulty(float hard)
    {
        GameManager.Instance.SetDifficulty(hard >= 0.5f);
    }
}