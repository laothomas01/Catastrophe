using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow_Player : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    void Start()
    {

    }

    void Update()
    {
        transform.position = player.transform.position + new Vector3(0, 1, -5);
    }
}
