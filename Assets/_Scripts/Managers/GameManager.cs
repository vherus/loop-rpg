using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameObject SelectedTile = null;

    public void HandleSpawnCycleComplete()
    {
        Debug.Log("Spawn cycle complete. Spawning enemies...");
    }
}
