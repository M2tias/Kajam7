using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    private LevelRuntime levelRuntime = null;
    public LevelRuntime LevelRuntime { set { levelRuntime = value; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Player")
        {
            Debug.Log("End triggered " + Time.time);
            levelRuntime.LoadNext = true;
        }
    }
}
