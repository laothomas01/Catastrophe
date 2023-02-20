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
        attackRange = 6;
        attackDirection = new Vector3();
        refScript = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        attackDirection = Vector3.ClampMagnitude(refScript.getMouseDirection(), 6);


        if (Input.GetKey(0))
        {
            attack = 1;
        }
        else

        {
            attack = 0;
        }





    }
    private Vector3 getAttackDirection()
    {
        return attackDirection;
    }


}
