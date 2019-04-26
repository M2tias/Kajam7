using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerRuntime", menuName = "New PlayerRuntime")]
public class PlayerRuntime : ScriptableObject
{
    [SerializeField]
    private int hp = 0;
    public int HP { get { return hp; } set { hp = value; } }

    [SerializeField]
    private int mana = 0;
    public int Mana { get { return mana; } set { mana = value; } }

    [SerializeField]
    private Vector3 position = new Vector3(0, 0, 0);
    public Vector3 Position { get { return position; } set { position = value; } }
}
