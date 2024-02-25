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
                float dmg = GenerateDamage(3, 8);
                Debug.Log($"Attacking for {dmg}!");
                if (target.GetComponent<Enemy>().TakeDamage(dmg)) {
                    yield return null;
                } else {
                    yield return new WaitForSeconds(0.5f);
                }
            }
        }
    }

    private float GenerateDamage(int dmgMin, int dmgMax)
    {
        EnemyStats eStats = target.GetComponent<EnemyStats>();
        System.Random random = new System.Random();
        
        int weaponDamage = random.Next(dmgMin, dmgMax + 1);
        float pStr = playerStats.Stats.Strength.Value;
        float eStr = (pStr - eStats.Stats.Vitality.Value + 20) / 4;
        float enemyArmor = 10f;
        float pDef = (enemyArmor + (eStats.Stats.Vitality.Value / 2)) / 100;
        float bDmg = weaponDamage + eStr;

        return bDmg - (bDmg * pDef);
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
