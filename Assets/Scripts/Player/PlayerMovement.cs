using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator anim;

    Player player;
    public float forceMultiplier;
    Rigidbody2D rigidBody;
    public float gravityScale;

    float wallDir = 0;

    bool canJump = true;
    public int maxJumps;
    public int currentJumps;

    public float maxLaunchVelocity;

    public bool facingRight;


    private BoxCollider2D hurtbox;

    private GameObject lastWall = null;

    GameManager gm;

    private void Awake()
    {
        player = GetComponent<Player>();
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.gravityScale = gravityScale;
        hurtbox = GetComponent<BoxCollider2D>();


    }

    private void Start()
    {
        currentJumps = maxJumps;
        facingRight = false;

        gm = GameManager.instance;
    }

    private void Update()
    {

        if (currentJumps <= 0)
        {
            canJump = false;
        }
        else
        {
            canJump = true;
        }
        float width = hurtbox.size.x * Mathf.Abs(transform.localScale.x);
        float height = hurtbox.size.y * Mathf.Abs(transform.localScale.y);
        float offsetX = hurtbox.offset.x;
        float offsetY = hurtbox.offset.y;
        float centerX = transform.position.x + offsetX * Mathf.Abs(transform.localScale.x);
        float centerY = transform.position.y + offsetY * Mathf.Abs(transform.localScale.y)  ;
        //top left, bottom right
        Collider2D left = Physics2D.OverlapArea(new Vector2(centerX - width / 2, centerY + height / 2), new Vector2(centerX - width / 2 + 0.01f, centerY - height / 2), 1 << LayerMask.NameToLayer("Wall"));

        //top left, bottom right
        Collider2D right = Physics2D.OverlapArea(new Vector2(centerX + width / 2, centerY + height / 2), new Vector2(centerX + width / 2 + 0.01f, centerY - height / 2), 1 << LayerMask.NameToLayer("Wall"));

                DrawDebugBox(new Vector2(centerX - width / 2, centerY + height / 2), new Vector2(centerX - width / 2 + 0.01f, centerY - height / 2), Color.red);
                DrawDebugBox(new Vector2(centerX + width / 2, centerY + height / 2), new Vector2(centerX + width / 2 + 0.01f, centerY - height / 2), Color.blue);

        if (left != null || right != null)
        {

            lockPlayerPosition();
            resetPlayerJumps();

            if (gm.timeElapsed == 0)
            {
                anim.Play("Wall Idle");

                lockPlayerPosition();
            }

            if (left != null)
            {
                wallDir = -1f;
                if (lastWall == null || lastWall != left.gameObject)
                {
                    gm.addScore(10);
                }
                lastWall = left.gameObject;
                //Debug.Log("Touch L");
            }
            else
            {
                wallDir = 1f;
                if (lastWall == null || lastWall != right.gameObject)
                {
                    gm.addScore(10);
                }
                lastWall = right.gameObject;
                //Debug.Log("Touch R");
            }
        }
        else
        {
            wallDir = 0;
        }

        //Debug.Log(currentJumps);

    }
    public void launchPlayer(Vector3 direction)
    {

        

        player.hideLaunchLine();
        if (Mathf.Sign(direction.x) == wallDir)
        {
            return;
        }
        unlockPlayerPosition();
        currentJumps -= 1;
        direction.x *= forceMultiplier;
        direction.y *= forceMultiplier;
        transform.position = new Vector3(Mathf.Sign(direction.x) * 0.02f + transform.position.x, transform.position.y, transform.position.z);
        rigidBody.velocity = direction;

        if (direction.y > 9 && direction.y < 27)
        {
            anim.Play("kick");
            //StartCoroutine(upJump());

        } else if (direction.y > -9 && direction.y < 9)
        {
            anim.Play("Kick Straight");
            //StartCoroutine(straightJump());
        } else if (direction.y > -27 && direction.y < -9)
        {
            anim.Play("Kick Down");
            //StartCoroutine(downJump()); 
        }

        if (direction.x > 0 && !facingRight || direction.x < 0 && facingRight)
        {
            facingRight = !facingRight;

            Vector3 theScale = transform.localScale;

            theScale.x *= -1;

            transform.localScale = theScale;
        }

        //Debug.Log(direction.y);

    }
    public void lockPlayerPosition()
    {
        rigidBody.gravityScale = 0;
        rigidBody.velocity = new Vector3(0, 0, 0);
    }

    public void resetPlayerJumps()
    {
        currentJumps = maxJumps;
    }

    public void unlockPlayerPosition()
    {
        rigidBody.gravityScale = gravityScale;
    }
    public bool getJumpStatus()
    {
        return canJump;
    }
    public void DrawDebugBox(Vector2 start, Vector2 end, Color col)//Draw a Debug Box
    {
        Debug.DrawLine(new Vector2(start.x, start.y), new Vector2(start.x, end.y), col);
        Debug.DrawLine(new Vector2(start.x, end.y), new Vector2(end.x, end.y), col);
        Debug.DrawLine(new Vector2(end.x, end.y), new Vector2(end.x, start.y), col);
        Debug.DrawLine(new Vector2(end.x, start.y), new Vector2(start.x, start.y), col);
    }


    IEnumerator upJump()
    {

        anim.Play("kick");
        
        yield return new WaitForSecondsRealtime(0.30f);

        anim.Play("Jump Up");

        yield return new WaitForEndOfFrame();
    }

    IEnumerator straightJump()
    {

        anim.Play("Kick Straight");

        yield return new WaitForSecondsRealtime(0.30f);

        anim.Play("Jump Straight");

        yield return new WaitForEndOfFrame();
    }

    IEnumerator downJump()
    {

        anim.Play("Kick Down");

        yield return new WaitForSecondsRealtime(0.30f);

        anim.Play("Jump Down");

        yield return new WaitForEndOfFrame();
    }

}
