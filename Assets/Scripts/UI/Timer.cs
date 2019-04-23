using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private Image tensOfMinutes;
    [SerializeField]
    private Image minutes;
    [SerializeField]
    private Image tensOfSeconds;
    [SerializeField]
    private Image seconds;
    [SerializeField]
    List<Sprite> digits;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int secondsVal = (int)((Time.time % 60) % 10);
        int tensOfSecondsVal = (int)((Time.time % 60) / 10);
        int minutesVal = ((int)(Time.time / 60) % 10);
        int tensOfMinutesVal = (int)((int)(Time.time / 60) / 10);
        Debug.Log(tensOfMinutesVal + " " + minutesVal + " : " + tensOfSecondsVal + " " + secondsVal);

        tensOfMinutes.sprite = digits[tensOfMinutesVal];
        minutes.sprite = digits[minutesVal];
        tensOfSeconds.sprite = digits[tensOfSecondsVal];
        seconds.sprite = digits[secondsVal];
    }
}
