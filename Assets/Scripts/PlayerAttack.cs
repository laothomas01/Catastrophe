using UnityEngine;
public class PlayerAttack : MonoBehaviour

{
    FieldOfView fieldOfView;

    void Start()
    {
        fieldOfView = GetComponent<FieldOfView>();
    }

    void Update()
    {
        Attack();
    }

    void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (fieldOfView.GetCurrentDetectedObject() != null)
            {
                fieldOfView.GetCurrentDetectedObject().GetComponent<Furniture>().IsDestroyed(true);
                Destroy(fieldOfView.GetCurrentDetectedObject());
            }
        }
    }

}