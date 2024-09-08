using UnityEngine;

public class DeviceManager : MonoBehaviour
{
    public static DeviceManager instance;
    public enum PlatformType { PC, Mobile }
    public PlatformType currentPlatform;

    private void Awake()    
    {
        // Singleton pattern: ensure only one instance exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Optionally keep it across scenes
        }
        else
        {
            Destroy(gameObject); // Ensure only one instance exists
        }
    }

    void Start()
    {
        DetectPlatform();
    }

    void DetectPlatform()
    {
        if (Application.isMobilePlatform)
        {
            currentPlatform = PlatformType.Mobile;
        }
        else
        {
            currentPlatform = PlatformType.PC;
        }

        NotifySystemsOfPlatformChange();
    }

    void NotifySystemsOfPlatformChange()
    {
        if (PlayerManager.instance != null)
        {
            PlayerManager.instance.NotifyPlayerOfCurrentPlatform(currentPlatform);
        }
        else
        {
            Debug.LogWarning("PlayerManager instance is not available.");
        }
    }
}
