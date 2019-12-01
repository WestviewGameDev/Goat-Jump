using UnityEngine;
using UnityEngine.UI;

public class playerScore : MonoBehaviour
{

    public Text scoreText;

    GameManager gm;

    // Update is called once per frame
    private void Start()
    {
        gm = GameManager.instance;
    }
    void Update()
    {
        scoreText.text = Mathf.RoundToInt(gm.score).ToString();

        gm.addScore(10*Time.deltaTime);
    }
}
