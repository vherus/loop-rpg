using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private List<GameObject> enemies;

    private GameObject target;

    private bool isFighting = false;
    private float movementSmoothing = 1f;
    private PlayerStats playerStats;

    [SerializeField]
    private int weaponMinDamage = 0;

    [SerializeField]
    private int weaponMaxDamage = 0;

    [SerializeField]
    private int armorPenetrationPercent = 1;

    public void SetEnemies(List<GameObject> enemyList)
    {
        if (!playerStats) {
            playerStats = GetComponent<PlayerStats>();
        }

        enemies = enemyList;
        isFighting = true;
        StartCoroutine(Fight());
    }

    private void EndFight()
    {
        Debug.Log("No enemies left, ending combat");
        isFighting = false;
        GameManager.Instance.ChangeState(GameState.Adventuring);
    }

    IEnumerator Fight()
    {
        while (isFighting) {
            if (!target || target.IsDestroyed()) {
                target = GetClosestEnemy();
                if (!target || target.IsDestroyed()) {
                    EndFight();
                    yield break;
                }
                Debug.Log("Targeted an enemy");
            }

            // Move to the enemy
            if (Vector3.Distance(transform.position, target.transform.position) > 2f) {
                transform.position = Vector3.Lerp(transform.position, target.transform.position, movementSmoothing * Time.deltaTime);
                yield return null;
            } else {
                // Attack
                int dmg = GenerateDamage();
                Debug.Log($"Attacking for {dmg}!");
                Enemy enemy = target.GetComponent<Enemy>();
                Debug.Log($"Enemy max health: {enemy.Health}");
                if (enemy.TakeDamage(dmg)) {
                    yield return null;
                } else {
                    yield return new WaitForSeconds(0.5f);
                }
            }
        }
    }

    private int GenerateDamage()
    {
        EnemyStats eStats = target.GetComponent<EnemyStats>();
        System.Random random = new System.Random();
        
        int weaponDamage = random.Next(weaponMinDamage, weaponMaxDamage + 1);

        float pStr = playerStats.Stats.Strength.Value;
        double eStr = ((pStr * 2.5) - eStats.Stats.Vitality.Value + 20) / 4;
        int enemyArmor = GameManager.Instance.EnemyLevel;
        int eACoefficient = 100;
        double pDef = (enemyArmor / eACoefficient / ((enemyArmor / eACoefficient) + (0.5 - (eStats.Stats.Vitality.Value / (2 * (eStats.Stats.Vitality.Value + eACoefficient)))))) - (armorPenetrationPercent / 100);
        double bDmg = weaponDamage + eStr;

        return (int) Math.Floor((1 - pDef) * bDmg);
    }

    private GameObject GetClosestEnemy()
    {
        GameObject bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (GameObject potentialTarget in enemies) {
            if (potentialTarget.IsDestroyed()) {
                continue;
            }

            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;

            if (dSqrToTarget < closestDistanceSqr) {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }
     
        return bestTarget;
    }
}
