using UnityEngine.InputSystem;
using UnityEngine;
public class PlayerAttack : MonoBehaviour
{
    FieldOfView fieldOfView;
    void Start()
    {
        fieldOfView = GetComponent<FieldOfView>();
    }

    //event driven handling for attack input 
    public void OnAttack(InputAction.CallbackContext value)
    {
        if (value.performed)
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
}

