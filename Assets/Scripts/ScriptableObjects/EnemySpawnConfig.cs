using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySpawnConfig", menuName = "New EnemySpawnConfig")]
public class EnemySpawnConfig : ScriptableObject
{
    [SerializeField]
    List<IntEnemy> spawnIdlist = null;
    public List<IntEnemy> SpawnIdlist { get { return spawnIdlist; } }
}
