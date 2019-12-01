using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Camera cam;
    LineRenderer launchLine;
    GameManager gameManager;

    PlayerInput input;

    playerScore playerScore;

    private PlayerMovement playerMovement;


    private void Awake()
    {
        launchLine = GetComponent<LineRenderer>();
        launchLine.enabled = false;

        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        gameManager = GameManager.instance;
        gameManager.loadNewLife();
    }

    public void Update()
    {
        //Debug.Log(playerMovement.currentJumps);
    }
    public void drawLaunchLine(Vector3 direction)
    {
        launchLine.enabled = true;

        direction.x += transform.position.x;
        direction.y += transform.position.y;
        direction.z = -0.01f;

        launchLine.startColor = Color.white;
        launchLine.endColor = Color.white;

        Material aquaDiffuseMat = new Material(Shader.Find("Transparent/Diffuse"));

        //aquaDiffuseMat.color = Color.cyan;



        launchLine.SetPosition(0, transform.position);
        launchLine.SetPosition(1, direction);

        launchLine.material = aquaDiffuseMat;

    }

    public void hideLaunchLine()
    {
        launchLine.enabled = false;
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Barrier")
        {
            //gameManager.playerDeath();
        }

        if(collision.gameObject.tag == "Platform")
        {
            playerMovement.resetPlayerJumps();
        }
    }

}   

