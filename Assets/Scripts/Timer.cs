using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private float timer = 0f;
    public Text TimerTxt;

    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Block").Length > 0)
        {
            timer += Time.deltaTime;
        }

        TimerTxt.text = timer.ToString("F2");
    }



}
