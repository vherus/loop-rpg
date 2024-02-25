using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public PlayerData Data;
    public Stats Stats;

    void Awake()
    {
        Stats = Data.BaseStats;
    }
}
