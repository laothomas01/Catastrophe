using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    FieldOfView fieldOfView;
    private DeviceManager.PlatformType currentPlatform;
    PlayerInput rightStickPress;

    void Start()
    {
        fieldOfView = GetComponent<FieldOfView>();
        rightStickPress = GetComponent<PlayerInput>();
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
                // HandlePcControls();
                HandleMobileControls(); // for testing purposes 

                break;
            case DeviceManager.PlatformType.Mobile:
                break;
            default:
                throw new System.Exception("Current device type not found");

        }
    }
    public void SetCurrentPlatform(DeviceManager.PlatformType platform)
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
    void HandleMobileControls()
    {
        if(rightStickPress.actions["Attack"].IsPressed())
        {
            Debug.Log("Attack");
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
