using UnityEngine.InputSystem;
using UnityEngine;
public class PlayerAttack : MonoBehaviour
{
    // FieldOfView fieldOfView;

    bool isAttacking = false;
    void Start()
    {
        // fieldOfView = GetComponent<FieldOfView>();
    }

    //event driven handling for attack input 
    public void OnAttack(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            isAttacking = true;
            GameObject target = GetComponent<FieldOfView>().GetCurrentDetectedObject();

            if (target != null)
            {
                Furniture furniture = target.GetComponent<Furniture>();
                if (furniture != null)
                {
                    EnemyManager enemyManager = FindAnyObjectByType<EnemyManager>();
                    enemyManager.HandleAlertEnemyEvent(this.gameObject.transform.position);
                    furniture.IsDestroyed(true);
                }


                // Vector3 attackDirection = GetComponent<PlayerMovement>().GetCurrentLookDirection();
                Destroy(target);
            }
        }
        else if (value.canceled)
        {
            isAttacking = false;
        }
    }

    public bool IsAttacking()
    {
        return isAttacking;
    }
}

