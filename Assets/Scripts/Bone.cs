using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bone : MonoBehaviour
{
    [SerializeField]
    private BoxCollider2D collider;
    [SerializeField]
    private SpriteRenderer renderer;
    [SerializeField]
    private float speed = 3;

    private bool isFlying;
    private int dir = 1;
    private Vector3 startPos = Vector3.zero;


    // Start is called before the first frame update
    void Start()
    {
        renderer.enabled = false;
        collider.enabled = false;
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
            renderer.enabled = false;
            collider.enabled = false;
            //transform.localPosition = new Vector3(0.75f, -0.5f, 0);
        }
    }

    public void Fire(int dir, Vector3 pos)
    {
        if(isFlying)
        {
            return;
        }

        isFlying = true;

        transform.position = new Vector3(pos.x + 0.75f, pos.y - 0.5f, 0);
        renderer.enabled = true;
        collider.enabled = true;
        this.dir = dir;
        startPos = new Vector3(pos.x + 0.75f, pos.y - 0.5f, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit something...");
        if (other.tag == "Enemy")
        {
            Debug.Log("Hit!");
            EnemyChild enemy = other.GetComponent<EnemyChild>();
            enemy.TakeDamage();
        }
    }
}
