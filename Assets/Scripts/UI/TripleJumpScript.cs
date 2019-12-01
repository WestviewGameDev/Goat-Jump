using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TripleJumpScript : MonoBehaviour
{
    Text textDisplay;
    Queue<float> endTimes;
    Image timeLeftBar;

    private void Start()
    {
        textDisplay = GetComponent<Text>();
        endTimes = new Queue<float>();
        timeLeftBar = GetComponentInChildren<Image>();

    }
    private void Update()
    {
        while (endTimes.Count > 0 && endTimes.Peek() < Time.time)
        {
            endTimes.Dequeue();
        }
        textDisplay.text = "+" + endTimes.Count + " Jumps";
        if (endTimes.Count > 0)
        {
            float timeLeft = endTimes.Peek() - Time.time;
            timeLeftBar.fillAmount = timeLeft / 10f;
        }
        else
        {
            timeLeftBar.fillAmount = 0f;
        }
    }
    public void addTime(float time)
    {
        endTimes.Enqueue(time);
    }

}
