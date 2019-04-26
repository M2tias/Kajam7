using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera = null;
    [SerializeField]
    private SpriteRenderer spriteRenderer = null;
    private Vector2 acc = Vector2.zero;
    private Vector2 vel = Vector2.zero;
    private float g = -0.45f;
    private bool climb_search = false;
    private bool climbing_left = false;
    private bool climbing_right = false;
    [SerializeField]
    private Animator animator = null;
    private bool shooting = false;
    [SerializeField]
    private Bone bone = null;
    [SerializeField]
    private LevelRuntime levelRuntime = null;
    [SerializeField]
    private PlayerRuntime playerRuntime = null;
    [SerializeField]
    private bool invulnerable = true;
    private float invul_started = 0f;
    private float invul_time = 2f;
    private float lastFlash = 0f;
    private float flashTime = 0.1f;
    private bool flash = false;
    private Shader shaderFlash;
    private Shader shaderSpritesDefault;
    private int shootDir = 1;

    // Start is called before the first frame update
    void Start()
    {
        playerRuntime.Mana = 3;
        playerRuntime.HP = 3;
        shaderFlash = Shader.Find("GUI/Text Shader");
        shaderSpritesDefault = Shader.Find("Sprites/Default");
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = playerRuntime.Position;
        if (invulnerable)
        {
            if (invul_started + invul_time <= Time.time)
            {
                invulnerable = false;
            }

            if (lastFlash + flashTime <= Time.time)
            {
                flash = !flash;
                lastFlash = Time.time;
            }

            if (flash)
            {
                flashSprite();
            }
            else
            {
                normalSprite();
            }
        }
        else
        {
            invul_started = Time.time;
        }

        //death by falling
        if(transform.localPosition.y < -4)
        {
            playerRuntime.HP = 0;
        }


        if (!invulnerable && flash)
        {
            normalSprite();
            flash = false;
        }

        vel = new Vector2(0, vel.y);
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            vel = new Vector3(-0.05f, vel.y);
            setAnimationTrue("walking");
            spriteRenderer.flipX = true;
            shootDir = -1;

        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            vel = new Vector3(0.05f, vel.y);
            setAnimationTrue("walking");
            spriteRenderer.flipX = false;
            shootDir = 1;
        }
        else
        {
            setAnimationTrue("idling");
        }

        if (Input.GetKeyDown(KeyCode.H) || Input.GetKeyDown(KeyCode.Z))
        {
            acc = new Vector2(acc.x, 11f);
        }

        if (Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.X))
        {
            setAnimationTrue("shooting");
            shooting = true;
            bone.Fire(shootDir, transform.position);
        }

        // set "climb_search" is true when pressing "up"
        // look if player is near stair's collider
        // if so set "climbing_left|right" true
        // climbing is false when the stairs is over
        // when climbing is true, move player higher on the stairs collider
        // TODO DOES NOT WORK >:(
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            climb_search = true;
            if (climbing_left)
            {
                transform.position = transform.position - 0.05f * Vector3.left + 0.1f * Vector3.up;
                //Debug.Log("Climbing left");
                setAnimationTrue("walking");
            }
            else if (climbing_right)
            {
                transform.position = transform.position - 0.05f * Vector3.right + 0.01f * Vector3.up;
                //Debug.Log("Climbing right");
                setAnimationTrue("walking");
            }
            else
            {
                setAnimationTrue("idling");
            }
        }

        // TODO: All colliders are now full colliders. Semicolliders that let the player pass from below are NEEDED
        Vector3 downLeftRay = transform.position + Vector3.left * 0.5f + Vector3.down * 0.9f;
        Vector3 downRightRay = transform.position + Vector3.right * 0.5f + Vector3.down * 0.9f;
        Vector3 sideLeftRay = transform.position + Vector3.left * 0.25f;
        Vector3 sideRightRay = transform.position + Vector3.right * 0.25f;
        Vector3 lowSideLeftRay = transform.position + Vector3.left * 0.25f + Vector3.down * 0.90f;
        Vector3 lowSideRightRay = transform.position + Vector3.right * 0.25f + Vector3.down * 0.90f;
        Vector3 highSideLeftRay = transform.position + Vector3.left * 0.25f + Vector3.up * 0.90f;
        Vector3 highSideRightRay = transform.position + Vector3.right * 0.25f + Vector3.up * 0.90f;
        Vector3 upLeftRay = transform.position + Vector3.left * 0.5f + Vector3.up * 0.9f;
        Vector3 upRightRay = transform.position + Vector3.right * 0.5f + Vector3.up * 0.9f;

        Debug.DrawLine(downLeftRay, downLeftRay + Vector3.down * 0.1f, Color.green);
        Debug.DrawLine(downRightRay, downRightRay + Vector3.down * 0.1f, Color.green);
        Debug.DrawLine(sideLeftRay, sideLeftRay + Vector3.left * 0.3f, Color.blue);
        Debug.DrawLine(sideRightRay, sideRightRay + Vector3.right * 0.3f, Color.red);
        Debug.DrawLine(lowSideLeftRay, lowSideLeftRay + Vector3.left * 0.3f, Color.blue);
        Debug.DrawLine(lowSideRightRay, lowSideRightRay + Vector3.right * 0.3f, Color.red);
        Debug.DrawLine(highSideLeftRay, highSideLeftRay + Vector3.left * 0.3f, Color.blue);
        Debug.DrawLine(highSideRightRay, highSideRightRay + Vector3.right * 0.3f, Color.red);
        Debug.DrawLine(upLeftRay, upLeftRay + Vector3.up * 0.1f, Color.magenta);
        Debug.DrawLine(upRightRay, upRightRay + Vector3.up * 0.1f, Color.magenta);
        ///-------------- transform's center
        Debug.DrawLine(transform.position, transform.position + Vector3.down * 0.1f, Color.magenta);
        Debug.DrawLine(transform.position + Vector3.left * 0.01f, transform.position + Vector3.left * 0.01f + Vector3.down * 0.1f, Color.magenta);
        Debug.DrawLine(transform.position + Vector3.right * 0.01f, transform.position + Vector3.right * 0.01f + Vector3.down * 0.1f, Color.magenta);

        int colliderMask = 1 << LayerMask.NameToLayer("colliders");
        int objectMask = 1 << LayerMask.NameToLayer("objects");
        int layerMask = colliderMask | objectMask;

        RaycastHit2D leftDownHit = raycast(downLeftRay, Vector3.down, 0.1f, layerMask);
        RaycastHit2D rightDownHit = raycast(downRightRay, Vector3.down, 0.1f, layerMask);
        RaycastHit2D leftUpHit = raycast(upLeftRay, Vector3.up, 0.1f, layerMask);
        RaycastHit2D rightUpHit = raycast(upRightRay, Vector3.up, 0.1f, layerMask);
        RaycastHit2D leftSideHit = raycast(sideLeftRay, Vector3.left, 0.3f, layerMask);
        RaycastHit2D rightSideHit = raycast(sideRightRay, Vector3.right, 0.3f, layerMask);
        RaycastHit2D lowLeftSideHit = raycast(lowSideLeftRay, Vector3.left, 0.3f, layerMask);
        RaycastHit2D lowRightSideHit = raycast(lowSideRightRay, Vector3.right, 0.3f, layerMask);
        RaycastHit2D highLeftSideHit = raycast(highSideLeftRay, Vector3.left, 0.3f, layerMask);
        RaycastHit2D highRightSideHit = raycast(highSideRightRay, Vector3.right, 0.3f, layerMask);

        if (leftDownHit.collider != null || rightDownHit.collider != null)
        {
            Vector2 point = Vector2.zero;
            if (rightDownHit.collider != null) point = rightDownHit.point;
            if (leftDownHit.collider != null) point = leftDownHit.point;

            // TODO: Same jumping code is in two places?!
            if (Input.GetKeyDown(KeyCode.H) || Input.GetKeyDown(KeyCode.Z))
            {
                //acc = new Vector2(acc.x, 11f);
            }
            else
            {
                acc = Vector2.zero;
                vel = new Vector2(vel.x, 0);
                transform.position = new Vector3(transform.position.x, point.y + 1f, transform.position.z);
            }
        }
        else
        {
            //hack to stop stair climbing
            climbing_left = false;
            climbing_right = false;
            acc = new Vector2(acc.x, g);
        }

        if (leftUpHit.collider != null)
        {
            GameObject collider = leftUpHit.collider.gameObject;
            TileCollider tileCollider = collider.GetComponent<TileCollider>();

            if (tileCollider.Parent.ColliderType == ColliderType.Full)
            {

                if (acc.y > 0)
                {
                    acc = new Vector3(acc.x, 0);
                }

                vel = new Vector2(vel.x, 0);
            }
        }
        else if (rightUpHit.collider != null)
        {
            GameObject collider = rightUpHit.collider.gameObject;
            TileCollider tileCollider = collider.GetComponent<TileCollider>();

            if (tileCollider.Parent.ColliderType == ColliderType.Full)
            {
                if (acc.y > 0)
                {
                    acc = new Vector3(acc.x, 0);
                }

                vel = new Vector2(vel.x, 0);
            }
        }

        if (leftSideHit.collider != null)
        {
            vel = leftSideCollision(vel, leftSideHit);
        }
        if (lowLeftSideHit.collider != null)
        {
            vel = leftSideCollision(vel, lowLeftSideHit);
        }
        if (highLeftSideHit.collider != null)
        {
            vel = leftSideCollision(vel, highLeftSideHit);
        }
        if (rightSideHit.collider != null)
        {
            vel = rightSideCollision(vel, rightSideHit);
        }
        if (lowRightSideHit.collider != null)
        {
            vel = rightSideCollision(vel, lowRightSideHit);
        }
        if (highRightSideHit.collider != null)
        {
            vel = rightSideCollision(vel, highRightSideHit);
        }

        vel = new Vector2(Mathf.Min(10f, vel.x), Mathf.Min(10f, vel.y));

        vel = new Vector2(vel.x, vel.y + acc.y * Time.deltaTime);
        transform.position = transform.position + new Vector3(vel.x, vel.y);
        playerRuntime.Position = transform.localPosition;

        float levelMaxX = levelRuntime.LevelWidth - 9.5f;
        float cameraX = Mathf.Max(-0.5f, Mathf.Min(levelMaxX, transform.position.x));
        mainCamera.transform.position = new Vector3(cameraX, mainCamera.transform.position.y, mainCamera.transform.position.z);
    }

    private Vector2 leftSideCollision(Vector2 vel, RaycastHit2D hit)
    {
        GameObject collider = hit.collider.gameObject;
        TileCollider tileCollider = collider.GetComponent<TileCollider>();
        if (tileCollider == null) return vel;

        if (tileCollider.Parent.ColliderType == ColliderType.Full && vel.x < 0)
        {
            vel = new Vector2(0, vel.y);
        }

        if (climb_search)
        {
            //Debug.Log("Left side collider type while searching for stairs: " + Enum.GetName(typeof(ColliderType), tileCollider.Parent.ColliderType));
            if (tileCollider.Parent.ColliderType == ColliderType.RightStairs) //why is this right stairs and not left??!
            {
                //Debug.Log("found climb left side stairs");
                climbing_left = true;
            }
            else
            {
                climbing_left = false;
            }
        }

        return vel;
    }

    private Vector2 rightSideCollision(Vector2 vel, RaycastHit2D hit)
    {
        GameObject collider = hit.collider.gameObject;
        TileCollider tileCollider = collider.GetComponent<TileCollider>();
        if (tileCollider == null) return vel;

        if (tileCollider.Parent.ColliderType == ColliderType.Full && vel.x > 0)
        {
            vel = new Vector2(0, vel.y);
        }

        if (climb_search)
        {
            //Debug.Log("Right side collider type while searching for stairs: " + Enum.GetName(typeof(ColliderType), tileCollider.Parent.ColliderType));
            if (tileCollider.Parent.ColliderType == ColliderType.LeftStairs) //why is this left stairs and not right??!
            {
                climbing_right = true;
                //Debug.Log("found climb right side stairs");
            }
            else
            {
                climbing_right = false;
            }
        }

        return vel;
    }

    private void setAnimationTrue(string name)
    {
        if (name == "walking" && shooting)
        {
            return;
        }

        animator.SetBool("walking", false);
        animator.SetBool("idling", false);
        animator.SetBool("shooting", false);
        animator.SetBool(name, true);
    }

    private void shootingOver()
    {
        shooting = false;
        animator.SetBool("shooting", false);
    }

    public void TakeHit(int damage)
    {
        if (invulnerable) return;

        invulnerable = true;
        playerRuntime.HP -= damage;
        if (playerRuntime.HP <= 0)
        {
            //todo: death
        }
    }

    private void flashSprite()
    {
        spriteRenderer.material.shader = shaderFlash;
        spriteRenderer.color = Color.white;
    }

    private void normalSprite()
    {
        spriteRenderer.material.shader = shaderSpritesDefault;
        spriteRenderer.color = Color.white;
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        TriggerTrigger(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        TriggerTrigger(collision);
    }

    private void TriggerTrigger(Collision2D col)
    {
        Debug.Log("Trigger triggered");
        if (col.gameObject.tag == "Switch")
        {
            Debug.Log("Switch triggered");
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                Switch s = col.gameObject.GetComponent<Switch>();
                if (s != null)
                {
                    s.Flip();
                    Debug.Log("Switch Flipped");
                }
            }
        }
    }*/

    private RaycastHit2D raycast(Vector3 origin, Vector2 dir, float distance, int layerMask)
    {
        RaycastHit2D result = new RaycastHit2D();
        ContactFilter2D filter = new ContactFilter2D();
        filter.layerMask = layerMask;
        filter.useTriggers = false;
        filter.useLayerMask = true;
        List<RaycastHit2D> results = new List<RaycastHit2D>();

        int hits = Physics2D.Raycast(origin, dir, filter, results, distance);
        if (hits > 0)
        {
            result = results[0];
        }

        return result;
    }
}
