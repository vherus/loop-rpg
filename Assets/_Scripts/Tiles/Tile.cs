using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField]
    private float SpawnPercentChance = 20f;

    public List<GameObject> PotentialEnemies;

    public List<Transform> SpawnPoints;

    public List<GameObject> SpawnedEnemies = new List<GameObject>();

    public void Spawn()
    {
        RemoveDestroyedEnemies();
        
        if (SpawnedEnemies.Count < 4 && Random.Range(0f, 100.0f) <= SpawnPercentChance) {
            GameObject enemy = Instantiate(PotentialEnemies[Random.Range(0, PotentialEnemies.Count - 1)], transform);
            enemy.GetComponent<Enemy>().UpdateHealth();
            Transform pos = SpawnPoints[SpawnedEnemies.Count];
            enemy.transform.position = pos.position;
            SpawnedEnemies.Add(enemy);

            Debug.Log(gameObject.name + " has spawned a " + enemy.GetComponent<Enemy>().DisplayName + "!");
        }
    }

    private void RemoveDestroyedEnemies()
    {
        foreach (GameObject enemy in SpawnedEnemies.ToList()) {
            if (!enemy || enemy.IsDestroyed()) {
                SpawnedEnemies.Remove(enemy);
            }
        }
    }

    private void OnCombatStarted(GameState state)
    {
        PlayerCombat pCombat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>();
        pCombat.SetEnemies(SpawnedEnemies);

        GameManager.OnAfterStateChanged -= OnCombatStarted;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (SpawnedEnemies.Count > 0) {
            Debug.Log("Tile has enemies, initiate combat!");
            GameManager.OnAfterStateChanged += OnCombatStarted;
            GameManager.Instance.ChangeState(GameState.Fighting);
        }
    }
}
