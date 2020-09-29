using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FireMethods
{

    

    public static Vector3 StraightShot(Vector3 firePosition, Vector3 targetPosition) {
        Vector3 dir = targetPosition - firePosition;
        dir.Normalize();
        return dir;
    }


    public static Vector3 ThreeShot(int index, Vector3 firePosition, Vector3 targetPosition) {
        Vector3 newVel = Vector3.zero;
        Vector3 dir = targetPosition - firePosition;
        dir.Normalize();
        if (index == 0)
        {
            newVel = dir;
        }
        else if (index == 1)
        {
            newVel = dir;
        }
        else if (index == 2) {
            newVel = dir;
        }


        return newVel;
    }

}
