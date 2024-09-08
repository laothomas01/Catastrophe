using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private FieldOfView fieldOfView;
    private DeviceManager.PlatformType currentPlatform;

    void Start()
    {
        fieldOfView = GetComponent<FieldOfView>();
    }

    void Update()
    {
        HandleCurrentPlatformControls();
    }

    void HandleCurrentPlatformControls()
    {
        switch(currentPlatform)
        {
            case DeviceManager.PlatformType.PC:
                HandlePcControls();
                break;
            case DeviceManager.PlatformType.Mobile:
                break;
            default:
                throw new System.Exception("Current device type not found");

        }
    }
    public void SetPlatformControls(DeviceManager.PlatformType platform)
    {
        currentPlatform = platform;
    }



    void HandlePcControls()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject target = fieldOfView.GetCurrentDetectedObject();
            if (target != null)
            {

                Furniture furniture = target.GetComponent<Furniture>();
                if (furniture != null)
                {
                    furniture.IsDestroyed(true);
                }
                Destroy(target);
            }
        }
    }
    // public void HandleCurrentPlatformControls(DeviceManager.PlatformType currentPlatform)
    // {
    //     switch(currentPlatform)
    //     {
    //         case DeviceManager.PlatformType.PC:
    //             HandlePcControls();
    //             break;
    //         case DeviceManager.PlatformType.Mobile:
    //             break;
    //         default:
    //             throw new System.Exception("Cannot find device");
    //     }
    // }
}




//     public void SetPCControls()
//     {
//         currentPlatform = PlatformType.Pc;
//     }
//     public void SetMobileControls()
//     {
//         currentPlatform = PlatformType.Mobile;
//     }
// }
