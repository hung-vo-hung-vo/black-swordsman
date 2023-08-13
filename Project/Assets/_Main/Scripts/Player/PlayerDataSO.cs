using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/Player", order = 0)]
public class PlayerDataSO : ScriptableObject
{
    [field: SerializeField] public float MaxHealthPoint { get; private set; }

    [field: SerializeField] public float MaxManaPoint { get; private set; }
    [field: SerializeField] public float ManaAutoHealDelay { get; private set; }
    [field: SerializeField] public float ManaAutoHeal { get; private set; }

    [field: SerializeField] public float RunSpeed { get; private set; }

    [field: SerializeField] public float JumpForce { get; private set; }
    [field: SerializeField] public float DoubleJumpWaitingTime { get; private set; }
    [field: SerializeField] public float DoubleJumpManaCost { get; private set; }

    [field: SerializeField] public float ShockTime { get; private set; } = 0.5f;
    [field: SerializeField] public float ShockForce { get; private set; } = 7f;

    [field: SerializeField] public AudioClip RunSound { get; private set; }
    [field: SerializeField] public AudioClip JumpSound { get; private set; }
    [field: SerializeField] public AudioClip TakeHitSound { get; private set; }
    [field: SerializeField] public AudioClip AttackSound { get; private set; }

    [field: SerializeField] public SkillData[] Skills { get; private set; }

    public Dictionary<int, SkillData> GetSkills()
    {
        var skillDict = new Dictionary<int, SkillData>();
        foreach (var skill in Skills)
        {
            skillDict[skill.skillNumber] = skill;
        }

        return skillDict;
    }
}