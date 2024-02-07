using UnityEngine;

public class GameManagers : MonoBehaviour
{
    // Singleton instance
    public static GameManager Instance;

    // Reference to player data
    public PlayerData playerData;

    // Event for when the game is started
    public event System.Action GameStarted;

    // Event for when the game is reset
    public event System.Action GameReset;

    // Event for when the player data is updated
    public event System.Action PlayerDataUpdated;

    // Flag to indicate if the game is currently running
    private bool isGameRunning;

    private void Awake()
    {
        // Ensure there is only one instance of this script
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Set the instance to not be destroyed on scene load
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // Start the game
        StartGame();
    }

    public void StartGame()
    {
        isGameRunning = true;

        // Invoke the GameStarted event
        GameStarted?.Invoke();
    }

    public void ResetGame()
    {
        isGameRunning = false;

        // Reset the player data
        playerData.ResetData();

        // Invoke the GameReset and PlayerDataUpdated events
        GameReset?.Invoke();
        PlayerDataUpdated?.Invoke();

        // Start the game again
        StartGame();
    }

    public bool IsGameRunning()
    {
        return isGameRunning;
    }

    public void UpdatePlayerData()
    {
        // Invoke the PlayerDataUpdated event
        PlayerDataUpdated?.Invoke();
    }
}
