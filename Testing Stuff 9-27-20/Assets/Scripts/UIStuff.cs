using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIStuff : MonoBehaviour
{
    public TextMeshProUGUI enemiesKilledText, waveNumText, accuracyText;


    GameManager gm;
    WaveManager wm;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        wm = gm.GetComponent<WaveManager>();
    }

    // Update is called once per frame
    void Update()
    {
        enemiesKilledText.text = "Enemies Killed: " + gm.enemiesKilled;
        waveNumText.text = "Wave: " + wm.waveNumber;
        accuracyText.text = StatTracker.Accuracy().ToString("F2") + "%";
        


    }

    
}
