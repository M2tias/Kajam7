using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField]
    private BoxCollider2D fullCollider = null;
    [SerializeField]
    private BoxCollider2D semiCollider = null;
    [SerializeField]
    private PolygonCollider2D leftStairsCollider = null;
    [SerializeField]
    private PolygonCollider2D rightStairsCollider = null;
    [SerializeField]
    private ColliderType type = ColliderType.None;
    public ColliderType ColliderType { get { return type; } }
    [SerializeField]
    private SpriteRenderer spriteRenderer = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCollider(string sType)
    {
        ColliderType cType = ColliderType.None;
        bool worked = Enum.TryParse(sType, out cType);
        type = cType;

        fullCollider.enabled = false;
        semiCollider.enabled = false;
        leftStairsCollider.enabled = false;
        rightStairsCollider.enabled = false;

        switch (cType)
        {
            case ColliderType.Semi:
                semiCollider.enabled = true;
                semiCollider.gameObject.layer = LayerMask.NameToLayer("colliders");
                break;
            case ColliderType.LeftStairs:
                leftStairsCollider.enabled = true;
                leftStairsCollider.gameObject.layer = LayerMask.NameToLayer("colliders");
                break;
            case ColliderType.RightStairs:
                rightStairsCollider.enabled = true;
                rightStairsCollider.gameObject.layer = LayerMask.NameToLayer("colliders");
                break;
            case ColliderType.Full:
                fullCollider.enabled = true;
                break;
        }
    }

    public void SetLayerInt(int layerInt)
    {
        spriteRenderer.sortingOrder = layerInt;
    }
}

public enum ColliderType
{
    None = 0,
    Semi,
    LeftStairs,
    RightStairs,
    Full
}