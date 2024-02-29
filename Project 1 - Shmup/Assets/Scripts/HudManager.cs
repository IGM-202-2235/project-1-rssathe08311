using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour
{

    [SerializeField] Text scoreText;

    const string k_SCORE_STR = "Score: {0}";

    [SerializeField] Slider healthSlider;

    // Start is called before the first frame update
    void Start()
    {
        //dont have this in yet
        healthSlider.minValue = 0;
        healthSlider.maxValue = 100;
    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.value = 20;

        scoreText.text = string.Format(k_SCORE_STR, healthSlider.value);
    }
}
