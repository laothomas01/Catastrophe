using UnityEngine;

public class UIManager : MonoBehaviour
{
    // public GameObject player;
    public GameObject mobileControlsUI;

    void Awake()
    {
        if(Application.isMobilePlatform)
        {
            mobileControlsUI.SetActive(true);
        }
        else
        {
            mobileControlsUI.SetActive(false);
        }

    }

}
