using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera = null;
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
    private Bone bone;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        vel = new Vector2(0, vel.y);
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            vel = new Vector3(-0.05f, vel.y);
            //transform.position = transform.position - 0.05f * Vector3.right;
            //mainCamera.transform.position = mainCamera.transform.position - 0.05f * Vector3.right;
            setAnimationTrue("walking");

        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            vel = new Vector3(0.05f, vel.y);
            //transform.position = transform.position - 0.05f * Vector3.left;
            //mainCamera.transform.position = mainCamera.transform.position - 0.05f * Vector3.left;
            setAnimationTrue("walking");
        }
        else
        {
            setAnimationTrue("idling");
        }

        if (Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.Z))
        {
            acc = new Vector2(acc.x, 10f);
        }

        if(Input.GetKeyDown(KeyCode.H) || Input.GetKeyDown(KeyCode.X))
        {
            setAnimationTrue("shooting");
            shooting = true;
            bone.Fire(1, transform.position);
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
                //mainCamera.transform.position = mainCamera.transform.position - 0.05f * Vector3.left;
                Debug.Log("Climbing left");
                setAnimationTrue("walking");
            }
            else if (climbing_right)
            {
                transform.position = transform.position - 0.05f * Vector3.right + 0.01f * Vector3.up;
                //mainCamera.transform.position = mainCamera.transform.position - 0.05f * Vector3.right;
                Debug.Log("Climbing right");
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

        RaycastHit2D leftDownHit = Physics2D.Raycast(downLeftRay, Vector3.down, 0.1f, 1 << LayerMask.NameToLayer("colliders"));
        RaycastHit2D rightDownHit = Physics2D.Raycast(downRightRay, Vector3.down, 0.1f, 1 << LayerMask.NameToLayer("colliders"));
        RaycastHit2D leftUpHit = Physics2D.Raycast(upLeftRay, Vector3.up, 0.1f, 1 << LayerMask.NameToLayer("colliders"));
        RaycastHit2D rightUpHit = Physics2D.Raycast(upRightRay, Vector3.up, 0.1f, 1 << LayerMask.NameToLayer("colliders"));
        RaycastHit2D leftSideHit = Physics2D.Raycast(sideLeftRay, Vector3.left, 0.3f, 1 << LayerMask.NameToLayer("colliders"));
        RaycastHit2D rightSideHit = Physics2D.Raycast(sideRightRay, Vector3.right, 0.3f, 1 << LayerMask.NameToLayer("colliders"));
        RaycastHit2D lowLeftSideHit = Physics2D.Raycast(lowSideLeftRay, Vector3.left, 0.3f, 1 << LayerMask.NameToLayer("colliders"));
        RaycastHit2D lowRightSideHit = Physics2D.Raycast(lowSideRightRay, Vector3.right, 0.3f, 1 << LayerMask.NameToLayer("colliders"));
        RaycastHit2D highLeftSideHit = Physics2D.Raycast(highSideLeftRay, Vector3.left, 0.3f, 1 << LayerMask.NameToLayer("colliders"));
        RaycastHit2D highRightSideHit = Physics2D.Raycast(highSideRightRay, Vector3.right, 0.3f, 1 << LayerMask.NameToLayer("colliders"));

        if (leftDownHit.collider != null || rightDownHit.collider != null)
        {
            float distance = 0f;
            Vector2 point = Vector2.zero;
            if (rightDownHit.collider != null) point = rightDownHit.point;
            if (leftDownHit.collider != null) point = leftDownHit.point;

            // TODO: Same jumping code is in two places?!
            if (Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.Z))
            {
                acc = new Vector2(acc.x, 10f);
            }
            else
            {
                acc = Vector2.zero;
                vel = new Vector2(vel.x, 0);
                transform.position = new Vector3(transform.position.x, point.y + 1f, transform.position.z);
            }
            //Debug.Log("not falling");
        }
        else
        {
            //hack to stop stair climbing
            climbing_left = false;
            climbing_right = false;
            acc = new Vector2(acc.x, g);
            //Debug.Log("falling");
        }
        
        if(leftUpHit.collider != null)
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
        mainCamera.transform.position = new Vector3(transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z);
    }

    private Vector2 leftSideCollision(Vector2 vel, RaycastHit2D hit)
    {
        GameObject collider = hit.collider.gameObject;
        TileCollider tileCollider = collider.GetComponent<TileCollider>();
        //Debug.Log("Left side collision detected");
        if (tileCollider.Parent.ColliderType == ColliderType.Full && vel.x < 0)
        {
            vel = new Vector2(0, vel.y);
        }

        if (climb_search)
        {
            Debug.Log("Left side collider type while searching for stairs: " + Enum.GetName(typeof(ColliderType), tileCollider.Parent.ColliderType));
            if (tileCollider.Parent.ColliderType == ColliderType.RightStairs) //why is this right stairs and not left??!
            {
                Debug.Log("found climb left side stairs");
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
        //Debug.Log("Right side collision detected");
        if (tileCollider.Parent.ColliderType == ColliderType.Full && vel.x > 0)
        {
            vel = new Vector2(0, vel.y);
        }

        if (climb_search)
        {
            Debug.Log("Right side collider type while searching for stairs: " + Enum.GetName(typeof(ColliderType), tileCollider.Parent.ColliderType));
            if (tileCollider.Parent.ColliderType == ColliderType.LeftStairs) //why is this left stairs and not right??!
            {
                climbing_right = true;
                Debug.Log("found climb right side stairs");
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
        if(name == "walking" && shooting)
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
}
