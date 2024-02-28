using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public static event Action<GameState> OnBeforeStateChanged;
    public static event Action<GameState> OnAfterStateChanged;

    public GameState State { get; private set; }

    public GameObject SelectedTile = null;

    [SerializeField]
    private GameObject player;

    public int EnemyLevel = 1;

    void Start() {
        ChangeState(GameState.Starting);
    }

    public void HandleSpawnCycleComplete()
    {
        Debug.Log("Spawn cycle complete. Spawning enemies...");
    }

    public void ChangeState(GameState newState)
    {
        if (State == newState) {
            return;
        }

        OnBeforeStateChanged?.Invoke(newState);

        State = newState;
        switch (newState) {
            case GameState.Starting:
                HandleStarting();
                break;

            case GameState.Adventuring:
                HandleAdventuring();
                break;

            case GameState.Fighting:
                HandleFighting();
                break;

            case GameState.PlacingCard:
                HandlePlacingCard();
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnAfterStateChanged?.Invoke(newState);

        Debug.Log($"New game state: {newState}");
    }

    private void HandleStarting()
    {
        Debug.Log("Starting the game...");

        // Set up stuff, run cinematics, set up environment, whatever

        ChangeState(GameState.Adventuring);
    }

    private void HandleAdventuring()
    {
        // TODO allow the player to move between waypoints
        player.GetComponent<PlayerAI>().ResumeMovement();
        Debug.Log("Player is now adventuring!");
    }

    private void HandleFighting()
    {
        player.GetComponent<PlayerAI>().PauseMovement();
        Debug.Log("Player is now fighting!");
    }

    private void HandlePlacingCard()
    {
        player.GetComponent<PlayerAI>().PauseMovement();
        Debug.Log("Player is placing a card");
    }
}

[Serializable]
public enum GameState
{
    Null = 0,
    Starting = 1,
    Adventuring = 2,
    Fighting = 3,
    PlacingCard = 4
}
