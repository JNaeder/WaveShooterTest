using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIPreviewScreen : MonoBehaviour
{
    public GameObject statsPrefab;
    public Transform statsParent;

    public TextMeshProUGUI itemNameText;
    public Image itemImage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowPreview(Item newItem) {
        itemNameText.text = newItem.itemName;
        itemImage.sprite = newItem.itemImage;
        DestroyAllStats();

        // If is Gun Item
        if (newItem.gunItem != null) {
            TextMeshProUGUI[] statsUI = new TextMeshProUGUI[4];
            for (int i = 0; i < statsUI.Length; i++)
            {
                GameObject newStat = Instantiate(statsPrefab, statsParent) as GameObject;
                statsUI[i] = newStat.GetComponent<TextMeshProUGUI>();
            }

            statsUI[0].text = "Fire Rate: " + newItem.gunItem.fireRate;
            statsUI[1].text = "Fire Method: " + newItem.gunItem.fireMethod;
            statsUI[2].text = "Reload Time: " + newItem.gunItem.reloadTime;
            statsUI[3].text = "Clip Capacity: " + newItem.gunItem.clipCapcity; 




        }


    }



    void DestroyAllStats() {
        TextMeshProUGUI[] oldStats = statsParent.GetComponentsInChildren<TextMeshProUGUI>();
        foreach (TextMeshProUGUI t in oldStats)
        {
            Destroy(t.gameObject);

        }


    }
}
