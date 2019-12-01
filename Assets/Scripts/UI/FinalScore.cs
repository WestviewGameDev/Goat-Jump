using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalScore : MonoBehaviour
{
    void Start()
    {
        GetComponent<Text>().text = "Final Score" + "\n" + Mathf.RoundToInt(GameManager.instance.score).ToString(); 
    }

}
