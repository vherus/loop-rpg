using System.Collections;
using System.Collections.Generic;
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
        if (SpawnedEnemies.Count < 4 && Random.Range(0f, 100.0f) <= SpawnPercentChance) {
            GameObject enemy = Instantiate(PotentialEnemies[Random.Range(0, PotentialEnemies.Count - 1)], transform);
            Transform pos = SpawnPoints[SpawnedEnemies.Count];
            enemy.transform.position = pos.position;
            SpawnedEnemies.Add(enemy);

            Debug.Log(gameObject.name + " has spawned a " + enemy.GetComponent<Enemy>().DisplayName + "!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (SpawnedEnemies.Count > 0) {
            Debug.Log("Tile has enemies, initiate combat!");
            GameManager.Instance.ChangeState(GameState.Fighting);
        }
    }
}
