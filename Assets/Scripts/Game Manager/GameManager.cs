using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{


    Player player;
    PlayerInput playerInput;
    PlayerMovement playerMovement;
    PowerUp powerUp;
    




    public float delayBeforeLoading = 3f;
    public string gameOverSceneName = "GameOver";

    public float timeElapsed = 0f;

    public static GameManager instance = null;
    public float score = 0;
    public float scoreMultiplier = 1;

    public int lives = 3;

    bool death = false;

    public bool paused = false;

    public GameObject pauseUI;

   
    private void Awake()
    {
        //Check if instance already exists
        if (instance == null)
        {
            //if not, set instance to this
            instance = this;
        }
        //If instance already exists and it's not this:
        else if (instance != this)
        {
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        }
        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

    }

    private void Update()
    {
        if (death == true)
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed > delayBeforeLoading)
            {
                SceneManager.LoadScene(gameOverSceneName);
                timeElapsed = 0;
                death = false;
            }
        }
    }
    public void playerDeath()
    {
        if(paused == true)
        {
            unpause();
        }
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        playerMovement.anim.Play("death");
        death = true;
    }
    public void loadNewLife()
    {
        score = 0;
        scoreMultiplier = 1;
    }
    public void addScore(float val)
    {
        score += val * scoreMultiplier;
    }
    public void togglePause()
    {
        if(paused == true)
        {
            unpause();
        }
        else
        {
            pause();
        }
    }
    private void pause()
    {
        Time.timeScale = 0;
        pauseUI.SetActive(true);
        paused = true;
    }
    private void unpause()
    {
        Time.timeScale = 1;
        pauseUI.SetActive(false);
        paused = false;
    }
}
