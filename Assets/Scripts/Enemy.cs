using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Collider2D colliderForTakingDamage;
    [SerializeField]
    private Collider2D colliderForDamagingPlayer;
    [SerializeField]
    private int HP = 1;
    [SerializeField]
    private int damage = 1;
    [SerializeField]
    private bool walks = false;
    [SerializeField]
    private int dir = 1;
    [SerializeField]
    private float speed = 3;
    [SerializeField]
    private SpriteRenderer spriteRenderer = null;

    private bool isHittable = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.flipX = dir > 0;

        if (walks)
        {
            Vector3 v = transform.position;
            Vector2 vel = new Vector2(speed * dir, 0);

            //better collision detection? gravity? possible to jump?
            Vector3 lowSideLeftRay = transform.position + Vector3.left * 0.25f + Vector3.down * 0.90f;
            Vector3 lowSideRightRay = transform.position + Vector3.right * 0.25f + Vector3.down * 0.90f;
            Debug.DrawLine(lowSideLeftRay, lowSideLeftRay + Vector3.left * 0.3f, Color.blue);
            Debug.DrawLine(lowSideRightRay, lowSideRightRay + Vector3.right * 0.3f, Color.red);
            RaycastHit2D lowLeftSideHit = Physics2D.Raycast(lowSideLeftRay, Vector3.left, 0.3f, 1 << LayerMask.NameToLayer("enemyboundaries"));
            RaycastHit2D lowRightSideHit = Physics2D.Raycast(lowSideRightRay, Vector3.right, 0.3f, 1 << LayerMask.NameToLayer("enemyboundaries"));


            if (lowLeftSideHit.collider != null)
            {
                vel = leftSideCollision(vel, lowLeftSideHit);
            }
            if (lowRightSideHit.collider != null)
            {
                vel = rightSideCollision(vel, lowRightSideHit);
            }

            transform.position = new Vector3(v.x + vel.x * Time.deltaTime, v.y, v.z);
        }
    }

    // TODO: damage amounts
    public bool TakeDamage()
    {
        if(!isHittable)
        {
            return false;
        }

        HP--;
        if(HP <= 0)
        {
            Destroy(gameObject);
        }
        return true;
    }

    public void NotHittable()
    {
        isHittable = false;
    }

    public void Hittable()
    {
        isHittable = true;
    }

    private Vector2 leftSideCollision(Vector2 vel, RaycastHit2D hit)
    {
        GameObject collider = hit.collider.gameObject;
        TileCollider tileCollider = collider.GetComponent<TileCollider>();
        if (tileCollider.Parent.ColliderType == ColliderType.Full && vel.x < 0)
        {
            vel = new Vector2(0, vel.y);
            dir = -dir;
        }

        return vel;
    }

    private Vector2 rightSideCollision(Vector2 vel, RaycastHit2D hit)
    {
        GameObject collider = hit.collider.gameObject;
        TileCollider tileCollider = collider.GetComponent<TileCollider>();
        if (tileCollider.Parent.ColliderType == ColliderType.Full && vel.x > 0)
        {
            vel = new Vector2(0, vel.y);
            dir = -dir;
        }

        return vel;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Player")
        {
            Player player = other.gameObject.GetComponent<Player>();
            if(player != null)
            {
                player.TakeHit(damage);
            }
        }
    }

    public void DoDamage(Player player)
    {
        player.TakeHit(damage);
    }
}
