using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private bool on = false;
    private Sprite onSprite;
    private Sprite offSprite;
    private int id = -1;
    public int ID { get { return id; } set { id = value; } }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (on && spriteRenderer.sprite != onSprite)
        {
            spriteRenderer.sprite = onSprite;
        }
        else if (!on && spriteRenderer.sprite != offSprite)
        {
            spriteRenderer.sprite = offSprite;
        }
    }

    public bool GetStatus()
    {
        return on;
    }

    public void Flip()
    {
        on = !on;
    }

    public void SetRenderer(SpriteRenderer renderer)
    {
        spriteRenderer = renderer;
    }

    public void SetSprites(Sprite onSprite, Sprite offSprite)
    {
        this.onSprite = onSprite;
        this.offSprite = offSprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Flip();
        }
    }
}
