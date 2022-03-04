
using UnityEngine;

public class Bubble : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private bool isMoving = false;
    [SerializeField]
    private BubbleSpawnManager bubbleSpawn;

    [SerializeField]
    private GameObject particleEffect;

    private void OnEnable()
    {
        //start moving the bubble on enable
        isMoving = true;
    }
    void Update()
    {
        if (!bubbleSpawn.isPaused)
        {
            if (isMoving)
            {
                //move the bubble with a constant velocity
                transform.Translate(Vector3.up * Time.deltaTime * speed);
            }

            //disable bubble if it goes too high
            if (transform.position.y > 8f)
            {
                gameObject.SetActive(false);
            }
        }
        
    }


    //disable bubble when user presses on it it
    private void OnMouseDown()
    {
        if (!bubbleSpawn.isPaused)
        {
            isMoving = false;
            if (gameObject.tag == "oxygen" && bubbleSpawn.currentOxygenLevel < 1f)
            {
                //inc current oxygen level and update slider
                bubbleSpawn.currentOxygenLevel += 0.02f;
                bubbleSpawn.oxygenSlider.value = bubbleSpawn.currentOxygenLevel;
            }
            else if (gameObject.tag == "nitrogen" && bubbleSpawn.currentNitrogenLevel < 1f)
            {
                bubbleSpawn.currentNitrogenLevel += 0.01f;
                bubbleSpawn.nitrogenSlider.value = bubbleSpawn.currentNitrogenLevel;
            }
            else if (gameObject.tag == "co2" && bubbleSpawn.currentCO2Level < 1f)
            {
                bubbleSpawn.currentCO2Level += 0.01f;
                bubbleSpawn.co2Slider.value = bubbleSpawn.currentCO2Level;
            }
            Destroy(Instantiate(particleEffect, transform.position, Quaternion.identity), 1f);
            gameObject.SetActive(false);
        }
    }
}
