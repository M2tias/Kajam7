using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChild : MonoBehaviour
{
    [SerializeField]
    private Enemy parent = null;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool TakeDamage()
    {
        return parent.TakeDamage();
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
