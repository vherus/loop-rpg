using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private List<GameObject> enemies;

    private GameObject target;

    private bool isFighting = false;

    public void SetEnemies(List<GameObject> enemyList)
    {
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
                transform.position = Vector3.Lerp(transform.position, target.transform.position, 1f * Time.deltaTime);
                yield return null;
            } else {
                // Attack
                Debug.Log($"Attacking!");
                if (target.GetComponent<Enemy>().TakeDamage(2)) {
                    yield return null;
                } else {
                    yield return new WaitForSeconds(2.5f);
                }
            }
        }
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
