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
    private bool isHittable = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // TODO: damage amounts
    public void TakeDamage()
    {
        if(!isHittable)
        {
            return;
        }

        Debug.Log("Taking damage");
        HP--;
        if(HP <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void NotHittable()
    {
        isHittable = false;
    }

    public void Hittable()
    {
        isHittable = true;
    }
}
