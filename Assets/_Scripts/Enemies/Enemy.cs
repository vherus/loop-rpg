using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private string displayName;

    [SerializeField]
    private int health;

    public string DisplayName { get { return displayName; } }
    public int Health { get { return health; } }

    public bool TakeDamage(int damage)
    {
        health -= damage;

        if (health < 1) {
            Destroy(gameObject);
            return true;
        }

        return false;
    }
}
