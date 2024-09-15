using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    private Animator animator;

    // // 0 = Walking
    // // 1 = Running 
    public string[] movementAnimationNames;
    PlayerMovement playerMovement;

    // // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }
    void Update()
    {
        HandleWalkSprintAnimation(playerMovement.IsMovingForward(), playerMovement.IsSprinting());
    }


    /*
    walk | sprint | ~walk | ~ sprint | walk & sprint | walk & ~sprint | 
    1	1	0	0		1		0
    1	0	0	1		0		1
    0	1	1	0		0		
    0	0 	1	1		0	
    */
    // you have to move to sprint
    // if sprint toggled on and not moving, dont do anything. 
    // if sprint togled on and moving, sprint 
    // if not sprinting and moving, walk 
    void HandleWalkSprintAnimation(bool isMoving, bool isSprinting)
    {

        if (isMoving)
        {
            if (isSprinting)
            {
                // isMoving = 1, isSprinting = 1
                animator.SetBool(movementAnimationNames[1], isMoving && isSprinting);
                animator.SetBool(movementAnimationNames[0], isMoving);
            }
            else
            {
                // isMoving = 1, isSprinting = 0
                animator.SetBool(movementAnimationNames[1], isMoving && isSprinting);
                animator.SetBool(movementAnimationNames[0], isMoving);
            }
        }
        else
        {
            animator.SetBool(movementAnimationNames[1], false);
            animator.SetBool(movementAnimationNames[0], false);
        }
    }
}
