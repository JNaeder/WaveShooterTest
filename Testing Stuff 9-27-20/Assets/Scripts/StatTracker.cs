using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatTracker : MonoBehaviour
{
   public static float shotsFired;
    public static float shotsHit;
    public static float shotsMissed;



    public static float Accuracy() {
        float perc = (shotsHit / shotsFired) * 100;
        return perc;

    }
}
