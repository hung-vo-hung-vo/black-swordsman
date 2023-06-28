using UnityEngine;

[System.Serializable]
public struct SkillData
{
    [Range(0, 2)] public int skillNumber;
    public float damage;
    public float delayTime;
}