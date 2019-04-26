using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bone : MonoBehaviour
{
    [SerializeField]
    private BoxCollider2D boxCollider = null;
    [SerializeField]
    private SpriteRenderer spriteRenderer = null;
    [SerializeField]
    private float speed = 3;

    private bool isFlying;
    private int dir = 1;
    private Vector3 startPos = Vector3.zero;


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer.enabled = false;
        boxCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isFlying)
        {
            Vector3 v = transform.position;
            transform.position = new Vector3(v.x+dir*speed* Time.deltaTime, v.y, v.z);
        }

        if(Vector3.Distance(transform.position, startPos) > 9)
        {
            isFlying = false;
            spriteRenderer.enabled = false;
            boxCollider.enabled = false;
        }
    }

    public void Fire(int dir, Vector3 pos)
    {
        if(isFlying)
        {
            return;
        }

        isFlying = true;

        startPos = new Vector3(pos.x + dir*0.75f, pos.y - 0.5f, 0);
        transform.position = startPos;
        spriteRenderer.enabled = true;
        boxCollider.enabled = true;
        this.dir = dir;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            EnemyChild enemy = other.GetComponent<EnemyChild>();
            if (enemy == null) return;
            bool hit = enemy.TakeDamage();
            if (hit)
            {
                isFlying = false;
                spriteRenderer.enabled = false;
                boxCollider.enabled = false;
            }
        }
    }
}
