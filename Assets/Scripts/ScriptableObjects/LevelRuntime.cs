using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelRuntime", menuName = "New LevelRuntime")]
public class LevelRuntime : ScriptableObject
{
    [SerializeField]
    private float levelWidth = 0f;
    public float LevelWidth { get { return levelWidth; } set { levelWidth = value; } }

    [SerializeField]
    private bool loadNext = false;
    public bool LoadNext { get { return loadNext; } set { loadNext = value; } }
}
