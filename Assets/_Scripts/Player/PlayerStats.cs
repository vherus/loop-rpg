using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public PlayerData data;
    public Stats stats;

    void Awake()
    {
        stats = data.BaseStats;
    }

    public void TakeDamage(int damage)
    {
        
    }
}
