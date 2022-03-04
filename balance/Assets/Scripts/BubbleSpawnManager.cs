using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BubbleSpawnManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> oxygenBubbles;
    [SerializeField]
    private List<GameObject> nitrogenBubbles;
    [SerializeField]
    private List<GameObject> co2Bubbles;

    [SerializeField]
    private GameObject oxygenBubble;
    [SerializeField]
    private GameObject nitrogenBubble;
    [SerializeField]
    private GameObject co2Bubble;

    public Slider oxygenSlider;
    public Slider nitrogenSlider;
    public Slider co2Slider;
    public Slider playerHealth;

    private float optimalOxygenLevel = 0.7f;
    private float optimalNitrogenLevel = 0.2f;
    private float optimalCO2Level = 0.1f;

    public float currentOxygenLevel;
    public float currentNitrogenLevel;
    public float currentCO2Level;
    public float health;

    public bool isPaused;

    [SerializeField]
    private GameObject loseScreen;
    [SerializeField]
    private GameObject winScreen;
    [SerializeField]
    private Text lostCause;

    private void Start()
    {
        isPaused = true;
        //initialise first bubble of each kind
        oxygenBubbles[0].SetActive(true);
        oxygenBubbles[0].transform.position = new Vector3(0f, 0.2f, 0f);
        nitrogenBubbles[0].SetActive(true);
        nitrogenBubbles[0].transform.position = new Vector3(2f, 0.2f, 0f);
        co2Bubbles[0].SetActive(true);
        co2Bubbles[0].transform.position = new Vector3(-2f, 0.2f, 0f);

        //define random start levels at start
        currentOxygenLevel = Random.Range(optimalOxygenLevel - 0.3f, optimalOxygenLevel + 0.25f);
        currentNitrogenLevel = Random.Range(optimalNitrogenLevel - 0.1f, optimalNitrogenLevel + 0.2f);
        currentCO2Level = Random.Range(optimalCO2Level - 0.05f, optimalCO2Level + 0.15f);

        //initialise sliders
        oxygenSlider.value = currentOxygenLevel;
        nitrogenSlider.value = currentNitrogenLevel;
        co2Slider.value = currentCO2Level;
        health = 1f;
        playerHealth.value = health;

        //start spawning bubbles at regular intervals
        InvokeRepeating("SpawnBubbles", 2f, 0.7f);
        InvokeRepeating("ConsumeOxygen", 2f, 1f);

    }

    private void SpawnBubbles()
    {
        //decides which bubble to spawn
        int decider = Random.Range(1, 4);

        if (!isPaused)
        {
            if (decider == 1)
            {

                //dec gas level
                //if less than 0 end the game
                if (currentOxygenLevel > 0f)
                {
                    currentOxygenLevel -= 0.01f;
                    oxygenSlider.value = currentOxygenLevel;
                }else
                {
                    isPaused = true;
                    loseScreen.SetActive(true);
                    lostCause.text = "OXYGEN LEVEL NOT OPTIMAL!";
                }
                //loop through all bubbles
                for (int i = 0; i < oxygenBubbles.Count; i++)
                {
                    //finds the first inactive bubble
                    //make it active and reposition it at the ground
                    if (!oxygenBubbles[i].activeInHierarchy)
                    {
                        oxygenBubbles[i].SetActive(true);
                        oxygenBubbles[i].transform.position = new Vector3(Random.Range(-7f, 7f), 0.1f, 0f);
                        break;
                    }
                    else if (i == oxygenBubbles.Count - 1)
                    {
                        //if all bubbles are already active
                        //instantiate a new bubble and add it in the list
                        GameObject g = Instantiate(oxygenBubble, new Vector3(Random.Range(-7f, 7f), 0.1f, 0f), Quaternion.identity, oxygenBubbles[0].transform.parent) as GameObject;
                        oxygenBubbles.Add(g);
                        break;
                    }
                }
            }
            else if (decider == 2)
            {
                if (currentNitrogenLevel > 0f)
                {
                    currentNitrogenLevel -= 0.005f;
                    nitrogenSlider.value = currentNitrogenLevel;
                }
                else
                {
                    isPaused = true;
                    loseScreen.SetActive(true);
                    lostCause.text = "NITROGEN LEVEL NOT OPTIMAL!";
                }

                for (int i = 0; i < nitrogenBubbles.Count; i++)
                {
                    if (!nitrogenBubbles[i].activeInHierarchy)
                    {
                        nitrogenBubbles[i].SetActive(true);
                        nitrogenBubbles[i].transform.position = new Vector3(Random.Range(-7f, 7f), 0.1f, 0f);
                        break;
                    }
                    else if (i == nitrogenBubbles.Count - 1)
                    {

                        GameObject g = Instantiate(nitrogenBubble, new Vector3(Random.Range(-7f, 7f), 0.1f, 0f), Quaternion.identity, nitrogenBubbles[0].transform.parent) as GameObject;
                        nitrogenBubbles.Add(g);
                        break;
                    }
                }
            }
            else
            {
                if (currentCO2Level > 0f)
                {
                    currentCO2Level -= 0.005f;
                    co2Slider.value = currentCO2Level;
                }
                else
                {
                    isPaused = true;
                    loseScreen.SetActive(true);
                    lostCause.text = "CO2 LEVEL NOT OPTIMAL!";
                }

                for (int i = 0; i < co2Bubbles.Count; i++)
                {
                    if (!co2Bubbles[i].activeInHierarchy)
                    {
                        co2Bubbles[i].SetActive(true);
                        co2Bubbles[i].transform.position = new Vector3(Random.Range(-7f, 7f), 0.1f, 0f);
                        break;
                    }
                    else if (i == nitrogenBubbles.Count - 1)
                    {

                        GameObject g = Instantiate(co2Bubble, new Vector3(Random.Range(-7f, 7f), 0.1f, 0f), Quaternion.identity, co2Bubbles[0].transform.parent) as GameObject;
                        co2Bubbles.Add(g);
                        break;
                    }
                }
            }
        }
    }

    private void ConsumeOxygen()
    {
        //consume oxygen
        if(health > 0f && !isPaused)
        {
            health -= 0.025f;
            playerHealth.value = health;
        }else if(health <= 0f && !isPaused)
        {
            isPaused = true;
            loseScreen.SetActive(true);
            lostCause.text = "PLAYER DIED!";
        }
    }


    //refill player's oxygen tank on button click
    public void FillTank()
    {
        if (!isPaused)
        {
            health = 1f;
            playerHealth.value = health;
            float temp = currentOxygenLevel;
            currentOxygenLevel = 0.7f * temp;
            oxygenSlider.value = currentOxygenLevel;
        }
    }

    public void ResumeGame()
    {
        isPaused = false;
    }


    //did player won or lost when timer ends
    public void Evaluate()
    {
        if(health > 0)
        {
            if(currentCO2Level >= 0.1f && currentCO2Level <= 0.4F)
            {
                if(currentNitrogenLevel >= 0.2f && currentNitrogenLevel <= 0.45F)
                {
                    if(currentOxygenLevel >= 0.5f && currentOxygenLevel <= 0.9F)
                    {
                        winScreen.SetActive(true);
                    }
                    else
                    {
                        loseScreen.SetActive(true);
                        lostCause.text = "OXYGEN LEVEL NOT OPTIMAL!";
                    }
                }
                else
                {
                    loseScreen.SetActive(true);
                    lostCause.text = "NITROGEN LEVEL NOT OPTIMAL!";
                }
            }
            else
            {
                loseScreen.SetActive(true);
                lostCause.text = "CO2 LEVEL NOT OPTIMAL!";
            }
        }
        else
        {
            loseScreen.SetActive(true);
            lostCause.text = "PLAYER DIED!";
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
