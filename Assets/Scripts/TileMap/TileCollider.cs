using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCollider : MonoBehaviour
{
    [SerializeField]
    private Tile parent = null;
    public Tile Parent { get { return parent; } }
    [SerializeField]
    private Collider2D tileCollider = null;
    public Collider2D Collider { get { return tileCollider; } }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
