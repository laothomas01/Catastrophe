using UnityEngine.InputSystem;
using UnityEngine;
using Unity.VisualScripting;

public class PlayerAttack : MonoBehaviour
{
    FieldOfView fieldOfView;
    // private DeviceManager.PlatformType currentPlatform;
    PlayerInput playerInput;
    private string currentControlScheme;

    void Start()
    {
        fieldOfView = GetComponent<FieldOfView>();
        playerInput = GetComponent<PlayerInput>();
        currentControlScheme = playerInput.currentControlScheme;
    }

    void Update()
    {
        // HandleCurrentPlatformControls();
    }

    public void OnAttack(InputAction.CallbackContext value)
    {
        if(value.performed)
        {
            if(currentControlScheme == "KeyboardMouse" || currentControlScheme == "Mobile")
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
            else if(currentControlScheme == "Keyboard")
            {
                
            }
        }
    }
    // void HandleCurrentPlatformControls()
    // {
    //     switch (currentPlatform)
    //     {
    //         case DeviceManager.PlatformType.PC:
    //             // HandlePcControls();
    //             // HandleMobileControls(); // for testing purposes 

    //             break;
    //         case DeviceManager.PlatformType.Mobile:
    //             break;
    //         default:
    //             throw new System.Exception("Current device type not found");

    //     }
    // }
    // public void SetCurrentPlatform(DeviceManager.PlatformType platform)
    // {
    //     currentPlatform = platform;
    // }



    // void HandlePcControls()
    // {
    //     if (Input.GetMouseButtonDown(0))
    //     {
    //         GameObject target = fieldOfView.GetCurrentDetectedObject();
    //         if (target != null)
    //         {
    //             Furniture furniture = target.GetComponent<Furniture>();
    //             if (furniture != null)
    //             {
    //                 furniture.IsDestroyed(true);
    //             }
    //             Destroy(target);
    //         }
    //     }
    // }


    // public void On_Attack_Mobile_Controls(InputAction.CallbackContext context)
    // {
    //     // if(context.performed)
    //     // {
    //     //     GameObject target = fieldOfView.GetCurrentRaycastDetectedObject();
    //     //     if (target != null)
    //     //     {

    //     //         Furniture furniture = target.GetComponent<Furniture>();
    //     //         if (furniture != null)
    //     //         {
    //     //             furniture.IsDestroyed(true);
    //     //         }
    //     //         Destroy(target);
    //     //     }
    //     // }
    // }
    // void HandleMobileControls()
    // {
    //     if (rightStickPress.actions["Attack"].IsPressed())
    //     {

    //     }
    // }


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
