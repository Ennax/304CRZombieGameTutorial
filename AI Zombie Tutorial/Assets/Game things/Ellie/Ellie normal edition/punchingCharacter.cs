using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class punchingCharacter : MonoBehaviour
{

    private Animator animator;
    private HitEnemy enemyScript;
    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        enemyScript = GetComponentInChildren<HitEnemy>();
        if (enemyScript != null)
        {
            enemyScript.isPunching = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        bool punching = Input.GetKey(KeyCode.B);
        animator.SetBool("isPunching", punching);
        if (enemyScript != null)
        {
            enemyScript.isPunching = punching;
        }

    }
}