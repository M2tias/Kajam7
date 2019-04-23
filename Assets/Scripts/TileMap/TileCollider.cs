using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCollider : MonoBehaviour
{
    [SerializeField]
    private Tile parent;
    public Tile Parent { get { return parent; } }
    [SerializeField]
    private Collider2D collider;
    public Collider2D Collider { get { return collider; } }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
