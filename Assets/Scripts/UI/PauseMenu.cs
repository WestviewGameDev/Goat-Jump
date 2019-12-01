using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager GM = GameManager.instance;
        GM.pauseUI = gameObject;
        gameObject.SetActive(false);
        Button[]buttons = GetComponentsInChildren<Button>();
        for (int i = 0; i < buttons.Length; i++)
        {
            switch (buttons[i].name)
            {
                case "Resume":
                    buttons[i].onClick.AddListener(delegate { GM.togglePause(); });
                    break;
                case "Quit":
                    buttons[i].onClick.AddListener(delegate { GM.playerDeath(); });
                    break;
            }
        }
    }


}
