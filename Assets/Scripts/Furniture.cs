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
    }
    void OnDisable()
    {
        GameEvents.handleAlertEnemyEvent(this.gameObject);
    }
    


}
