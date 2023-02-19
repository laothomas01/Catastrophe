using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    //Creating and assigning the reference variable

    PlayerMovement refScript;
    [SerializeField]
    protected int attackRange;
    protected Vector3 attackDirection;
    int attack = 0;
    void Start()
    {
        //attackDirection = refScript.getMouseDirection();
        //requires player movement script
        refScript = GetComponent<PlayerMovement>();
        attackDirection = new Vector3();
        attackRange = 6;
    }

    void Update()
    {
        //limit the directional distance of mouse

        attackDirection = refScript.getMouseDirection();
        if (attackDirection.magnitude > attackRange)
        {
            attackDirection = Vector3.ClampMagnitude(attackDirection, attackRange);
        }
        //Debug.Log(attackDirection.magnitude);
        //attackDirection = refScript.getMousePosition()/attackRange;

        //if(attackDirection.magnitude > attackRange)
        //{
        //    attackDirection = Vector3.ClampMagnitude(attackDirection, attackRange);
        //}


        //Debug.Log(refScript.getMouseDirection().magnitude);

        if (Input.GetKey(0))
        {
            attack = 1;
        }
        else

        {
            attack = 0;
        }

        //Debug.Log(attackDirection);




    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, attackDirection);

    }
    private void FixedUpdate()
    {


    }
    private Vector3 getAttackDirection()
    {
        return attackDirection;
    }
}
