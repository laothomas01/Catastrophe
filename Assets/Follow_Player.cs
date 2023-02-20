using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow_Player : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    public Vector3 offset;
    void Start()
    {

    }

    void Update()
    {
        transform.position = player.transform.position + offset;
    }
}
