using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private SpriteRenderer spriteRenderer = null;
    private Collider2D collider2d = null;
    private Switch doorSwitch = null;
    private bool open = false;
    private int id = -1;
    public int ID { get { return id; } set { id = value; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (doorSwitch != null)
        {
            open = doorSwitch.GetStatus();
        }
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = !open;
        }
        if (collider2d != null)
        {
            collider2d.enabled = !open;
        }
    }

    public void SetDeps(SpriteRenderer spriteRenderer, Collider2D collider2d)
    {
        this.spriteRenderer = spriteRenderer;
        this.collider2d = collider2d;
    }

    public void SetSwitch(Switch s)
    {
        doorSwitch = s;
    }
}
