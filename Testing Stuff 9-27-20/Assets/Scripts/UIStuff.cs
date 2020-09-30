using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIStuff : MonoBehaviour
{
    public TextMeshProUGUI enemiesKilledText, waveNumText, moneyText, healthText, shieldsText;


    GameManager gm;
    WaveManager wm;
    GuyController gc;
    GuyShooting gs;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        wm = gm.GetComponent<WaveManager>();
        gc = FindObjectOfType<GuyController>();
        gs = gc.GetComponent<GuyShooting>();
    }

    // Update is called once per frame
    void Update()
    {
        enemiesKilledText.text = "Enemies Killed: " + gm.enemiesKilled;
        waveNumText.text = "Wave: " + wm.waveNumber;
        moneyText.text = "Money: $" + gm.money;
        healthText.text = "Health: " + gm.health;
        shieldsText.text = "Shields: " + gm.shields;


    }

    
}
