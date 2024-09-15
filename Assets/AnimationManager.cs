using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    // private Animator animator;

    // // 0 = Walking
    // // 1 = Running 
    public string[] movementAnimationNames;
    // // PlayerMovement playerMovement;

    // // Start is called before the first frame update
    void Start()
    {
        // animator = GetComponent<Animator>();
        // if (animator == null)
        // {
        //     throw new System.Exception("Cannot find Animator component");
        // }
        // playerMovement = GetComponent<PlayerMovement>();
        // if (playerMovement == null)
        // {
        //     throw new System.Exception("Cannot find PlayerMovement component");
        // }
    }
    void Update()
    {
        // HandleMovementAnimation(playerMovement.GetMovementInput(), );

        // HandleMovementAnimation(playerMovement.GetMovementInput(), playerMovement.IsSprinting());
    }
    // void HandleMovementAnimation(Vector3 movementInput, bool isSprinting)
    // {
    //     bool isMoving = movementInput.x != 0 || movementInput.z != 0; // Movement in the horizontal plane
    //     // Debug.Log("is moving: "  + isMoving);
    //     // // animator.SetBool("Walking",!isSprinting && isMoving);
    //     animator.SetBool("ToggleRun",isSprinting  && isMoving);
    //     // if (isMoving)
    //     // {
    //     //     animator.Play("Walking");
    //     // }
    //     //     if (isSprinting)
    //     //     {
    //     //         // If running, turn on running animation and turn off walking animation
    //     //         animator.SetBool("Walking", false); // Walk animation off
    //     //         animator.SetBool("Running", true);  // Sprint animation on
    //     //     }
    //     //     else
    //     //     {
    //     //         // If not running but still moving, ensure walking animation is on
    //     //         animator.SetBool("Walking", true);  // Walk animation on
    //     //         animator.SetBool("Running", false); // Sprint animation off
    //     //     }
    //     // }
    //     // else
    //     // {
    //     //     // If not moving, turn off both animations
    //     //     animator.SetBool("Walking", false); // Walk animation off
    //     //     animator.SetBool("Running", false); // Sprint animation off
    //     // }
    // }



}
