using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChild : MonoBehaviour
{
    [SerializeField]
    private Enemy parent;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage()
    {
        parent.TakeDamage();
    }

    public void NotHittable()
    {
        parent.NotHittable();
    }

    public void Hittable()
    {
        parent.Hittable();
    }
}
