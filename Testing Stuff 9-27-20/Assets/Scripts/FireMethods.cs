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



    public static Quaternion BulletRotation(Vector2 mousePosition, Vector3 muzzlePosition)
    {
        Vector3 diff = mousePosition - new Vector2(muzzlePosition.x, muzzlePosition.y);
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(0f, 0f, rot_z - 90);
    }

    public static Quaternion MultiBulletRotation(Vector2 mousePosition, Vector3 muzzlePosition, float spreadAngle, int numOfShots, int shotIndex)
    {
        Vector3 diff = mousePosition - new Vector2(muzzlePosition.x, muzzlePosition.y);
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        float offset =  90 - (spreadAngle / 2);
        float increment = spreadAngle / (numOfShots - 1);
        return Quaternion.Euler(0f, 0f, rot_z - (offset + (increment * shotIndex)));
    }

}
