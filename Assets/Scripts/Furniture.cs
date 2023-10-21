using UnityEngine;

public class Furniture : MonoBehaviour

{

    bool isDestroyed = false;
    void OnDestroy()
    {

        if (isDestroyed)
        {
            Debug.Log("Destroyed by player!");
            //handle enemy alert mechanic
            EnemyManager.handleAlertEnemyEvent(this.gameObject);
            // enemyManager.handleAlertEnemyEvent(this.gameObject);
            //handle scoring system
        }
        Debug.Log(this + " Destroyed by game exit!");

    }
    void OnDisable()
    {
    }
    public void setIsDestroyed(bool destroyed)
    {
        isDestroyed = destroyed;
    }



}
