using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private string displayName;

    [SerializeField]
    private float health;

    public string DisplayName { get { return displayName; } }
    public float Health { get { return health; } }

    public bool TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0f) {
            Destroy(gameObject);
            return true;
        }

        return false;
    }

    public void UpdateHealth()
    {
        health = 9 * GameManager.Instance.EnemyLevel;
    }
}
