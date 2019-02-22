using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEnemy : MonoBehaviour
{
    public bool isPunching = false;

    void OnTriggerEnter(Collider other)
    {
        if (isPunching && other.tag == "Zombie")
        {
            other.GetComponent<AITargetcontroller>().kill();
        }
    }
}