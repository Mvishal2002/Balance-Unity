
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timeValue = 60;
    public Text timer;
    [SerializeField]
    private BubbleSpawnManager bubbleSpawn;

    // Update is called once per frame
    void Update()
    {
        if (!bubbleSpawn.isPaused)
        {
            if (timeValue > 0)
            {
                timeValue -= Time.deltaTime;
            }
            else
            {
                bubbleSpawn.Evaluate();
                bubbleSpawn.isPaused = true;
            }

            DisplayTime(timeValue);
        }
    }

    private void DisplayTime(float timeValue)
    {
        if(timeValue < 0)
        {
            timeValue = 0;
        }else if(timeValue > 0)
        {
            timeValue += 1;
        }

        timer.text = timeValue.ToString("0");
    }
}
