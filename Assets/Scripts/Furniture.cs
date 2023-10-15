using UnityEngine;

public class Furniture : MonoBehaviour

{
    void Start()
    {

    }
    void Update()
    {

    }
    void OnDestroy()
    {

        GameEvents.AlertEnemy(gameObject);
    }
    void OnDisable()
    {
    }

}
