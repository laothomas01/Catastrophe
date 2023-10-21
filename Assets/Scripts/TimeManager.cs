using UnityEngine;
using TMPro;
using System;

public class TimeManager : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    private float time;
    private TimeSpan timespan; 
    private string timeElapsed;
    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        time = 0;
        textMesh.SetText("Test");
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        timespan = TimeSpan.FromSeconds(time);
        timeElapsed = String.Format(@"{0:mm\:ss\.ff}", timespan);
        textMesh.SetText(timeElapsed);
    }
}
