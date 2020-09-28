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


    
}
