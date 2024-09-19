using UnityEngine.InputSystem;
using UnityEngine;
public class PlayerAttack : MonoBehaviour
{
    FieldOfView fieldOfView;
    bool isAttacking = false;
    void Start()
    {
        fieldOfView = GetComponent<FieldOfView>();
    }
    
    //event driven handling for attack input 
    public void OnAttack(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            isAttacking = true;
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
        else if(value.canceled)
        {
            isAttacking = false;
        }
    }

    public bool IsAttacking()
    {
        return isAttacking;
    }
}

