using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
   [Header("Component")]
    public Text timerText;

    [Header("Timer Settings")]
    public float currentTime;
    public bool countDown;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = countDown ? currentTime -= Time.deltaTime : currentTime += Time.deltaTime;
        SetTimerText();
    }

    private void SetTimerText()
    {
        timerText.text = currentTime.ToString("Tiempo: " + "0.00");
    }
}
