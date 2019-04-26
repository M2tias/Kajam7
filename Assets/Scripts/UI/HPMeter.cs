using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPMeter : MonoBehaviour
{
    [SerializeField]
    private List<Image> hp = null;
    [SerializeField]
    private PlayerRuntime playerRuntime = null;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        for (var i = 0; i < hp.Count; i++)
        {
            if (i < playerRuntime.HP)
            {
                hp[i].gameObject.SetActive(true);
            }
            else
            {
                hp[i].gameObject.SetActive(false);
            }
        }
    }
}
