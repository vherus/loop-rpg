using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private List<GameObject> enemies;

    private GameObject target;

    public void SetEnemies(List<GameObject> enemyList)
    {
        enemies = enemyList;
        target = GetClosestEnemy();
    }

    private void Fight()
    {
        // TODO Attack target until dead
        return;

        // TODO Select next enemy, repeat
        target = GetClosestEnemy();

        if (!target) {
            GameManager.Instance.ChangeState(GameState.Adventuring);
        }
    }

    private GameObject GetClosestEnemy()
    {
        GameObject bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (GameObject potentialTarget in enemies) {
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
