using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public PlayerData Data;
    public Stats Stats;

    void Awake()
    {
        Stats = Data.BaseStats;
    }

    public void TakeDamage(int damage)
    {
        
    }
}
