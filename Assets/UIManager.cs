using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance = null;
    public GameObject mobileUi;
    // public GameObject pcUi;
    private DeviceManager.PlatformType currentPlatform;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        // if(mobileUi == null)
        // {
        //     throw new System.Exception("Mobile UI not found");
        // }
        // else
        // {
        switch (currentPlatform)
        {
            case DeviceManager.PlatformType.PC:
                break;
            case DeviceManager.PlatformType.Mobile:
                mobileUi.SetActive(true);
                // pcUi.SetActive(false);
                break;

        }
        // }
    }

    public void SetCurrentPlatform(DeviceManager.PlatformType platform)
    {
        currentPlatform = platform;
    }


}
