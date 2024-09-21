using UnityEngine;
using UnityEngine.InputSystem;

/*
on start:
    if mobile:

    if PC:

*/
public class GameManager : MonoBehaviour
{
    // public GameObject player;
    public GameObject mobileControlsUI;

    void Awake()
    {
        if(Application.isMobilePlatform)
        {
            mobileControlsUI.SetActive(true);
        }

    }

}
