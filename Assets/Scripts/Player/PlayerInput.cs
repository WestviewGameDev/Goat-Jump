using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInput : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private Player player;
    Vector3 clickMousePos;
    GameManager gm;
    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        player = GetComponent<Player>();

    }
    private void Start()
    {
        gm = GameManager.instance;
    }
    void Update()
    {
        bool mouseClicked = Input.GetMouseButtonDown(0);
        bool mouseHeld = Input.GetMouseButton(0);
        bool mouseUp = Input.GetMouseButtonUp(0);
        bool pauseButton = Input.GetButtonDown("Pause");
        if(pauseButton == true)
        {
            gm.togglePause();
            player.hideLaunchLine();
        }
        if (playerMovement.getJumpStatus() == true)
        {

            if (gm.paused == false && mouseUp == true)
            {
                Vector3 currentMousePos = Input.mousePosition;
                Vector3 launchDirection = calculateDirection(clickMousePos, currentMousePos);
                if (launchDirection.magnitude >= 0.1)
                {
                    playerMovement.launchPlayer(launchDirection);
                }
              /*  playerMovement.launchPlayer(new Vector2(Input.mousePosition.x - clickMousePos.x, Input.mousePosition.y - clickMousePos.y));*/
            }
            else if (mouseClicked == true)
            {
                clickMousePos = Input.mousePosition;
            }
            else if (gm.paused == false && mouseHeld == true)
            {
                Vector3 currentMousePos = Input.mousePosition;
                player.drawLaunchLine(calculateDirection(clickMousePos, currentMousePos));

            }
        }
    }

    private Vector3 calculateDirection(Vector3 v1, Vector3 v2)
    {
        v1.z = 15;
        v2.z = 15;
        Vector3 v1WorldPoint = player.cam.ScreenToWorldPoint(v1);
        Vector3 v2WorldPoint = player.cam.ScreenToWorldPoint(v2);
        Vector3 direction = new Vector3(v2WorldPoint.x - v1WorldPoint.x, v2WorldPoint.y - v1WorldPoint.y, 0);
        direction = Vector3.ClampMagnitude(direction, playerMovement.maxLaunchVelocity);
        return direction;
    }
}
