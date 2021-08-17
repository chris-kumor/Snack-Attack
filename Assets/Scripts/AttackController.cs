using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCOntroller : MonoBehaviour
{

    public static GameObject Attack(GameObject Atk, Vector3 weaponPos, Vector3 targetDir, float atkDistance, Quaternion ProjectileRotation){
        Vector3 atkPos = new Vector3(weaponPos.x + targetDir.x * atkDistance * Time.deltaTime, weaponPos.y + targetDir.y * atkDistance * Time.deltaTime, 1.00f);
        GameObject atk = Instantiate(Atk, atkPos, ProjectileRotation);
        return atk;
    }

}
