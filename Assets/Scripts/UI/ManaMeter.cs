using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaMeter : MonoBehaviour
{
    [SerializeField]
    private List<Image> mana = null;
    [SerializeField]
    private PlayerRuntime playerRuntime = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        for (var i = 0; i < mana.Count; i++)
        {
            if (i < playerRuntime.Mana)
            {
                mana[i].gameObject.SetActive(true);
            }
            else
            {
                mana[i].gameObject.SetActive(false);
            }
        }
    }
}
