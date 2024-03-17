using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour
{

    [SerializeField] Text scoreText;

    const string k_SCORE_STR = "Score: {0}";

    [SerializeField] Slider healthSlider;

    [SerializeField] CollisionManager collisionManager;

    [SerializeField] Image endPanel;
    [SerializeField] Text endText;
    [SerializeField] Text endScore;

    // Start is called before the first frame update
    void Start()
    {
        //dont have this in yet
        healthSlider.minValue = 0;
        healthSlider.maxValue = 100;

        endPanel.enabled = false;
        endScore.enabled = false;
        endText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (collisionManager.playerHealth <= 0) 
        {
            endScore.text = string.Format("Final Score: {0}", collisionManager.score);

            endPanel.enabled = true;
            endScore.enabled = true;
            endText.enabled = true;
        }
        healthSlider.value = collisionManager.playerHealth;

        scoreText.text = string.Format(k_SCORE_STR, collisionManager.score);
    }
}
