using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public GameObject player;
    PlayerAttack playerAttack;
    PlayerMovement playerMovement;

    private void Awake()
    {
        // Singleton pattern: ensure only one instance exists
        if (instance == null)
        {
            instance = this;
            // DontDestroyOnLoad(gameObject); // Optionally keep it across scenes
        }
        else
        {
            // Destroy(gameObject); // Ensure only one instance exists
        }
    }

    void Start()
    {
        if (player != null)
        {
            playerAttack = player.GetComponent<PlayerAttack>();
            playerMovement = player.GetComponent<PlayerMovement>();
        }
        else
        {
            Debug.LogError("Player instance not assigned in PlayerManager.");
        }
    }

    public void NotifyPlayerOfCurrentPlatform(DeviceManager.PlatformType currentPlatform)
    {
        if (playerAttack != null)
        {
            playerAttack.SetCurrentPlatform(currentPlatform);
        }
    }
}
