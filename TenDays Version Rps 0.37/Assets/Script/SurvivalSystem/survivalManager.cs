using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class survivalManager : MonoBehaviour
{
    public Slider healthSilder;
    public int maxHealth;
    public int healthFallRate;

    public Slider thristSilder;
    public int maxThrist;
    public int thristFallRate;

    public Slider hungerSilder;
    public int maxHunger;
    public int hungerFallRate;

    void Start()
    {
        healthSilder.maxValue = maxHealth;
        healthSilder.value = maxHealth;

        thristSilder.maxValue = maxThrist;
        thristSilder.value = maxThrist;

        hungerSilder.maxValue = maxHunger;
        hungerSilder.value = maxHunger;
    }

    void Update()
    {
        //Health controller
        if(hungerSilder.value <= 0 && (thristSilder.value <= 0))
        {
            healthSilder.value -= Time.deltaTime / healthFallRate * 2;
        }

        else if(hungerSilder.value <= 0 || thristSilder.value <= 0)
        {
            healthSilder.value -= Time.deltaTime / healthFallRate;
        }

        if(healthSilder.value <= 0)
        {
            CharacterDeath();
        }

        //Hunger controller
        if(hungerSilder.value >= 0)
        {
            hungerSilder.value -= Time.deltaTime / hungerFallRate;
        }

        else if(hungerSilder.value <= 0)
        {
            hungerSilder.value = 0;
        }

        else if(hungerSilder.value >= maxHunger)
        {
            hungerSilder.value = maxHunger;
        }

        //Thrist controller
        if (thristSilder.value >= 0)
        {
            thristSilder.value -= Time.deltaTime / thristFallRate;
        }

        else if (thristSilder.value <= 0)
        {
            thristSilder.value = 0;
        }

        else if (thristSilder.value >= maxThrist)
        {
            thristSilder.value = maxThrist;
        }
    }

    void CharacterDeath()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
