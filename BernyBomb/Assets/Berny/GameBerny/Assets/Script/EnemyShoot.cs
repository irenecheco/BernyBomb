using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    private float attackRefreshRate;

    private AggroDetection aggroDetection;
    private EnemyHealth healtTarget;
    private float attackTimer;
   

    // Start is called before the first frame update
    private void Awake()
    {
        aggroDetection = GetComponent<AggroDetection>();
        aggroDetection.OnAggro += AggroDetection_OnAggro;
    }

    private void AggroDetection_OnAggro(Transform target)
    {
        EnemyHealth health = target.GetComponent<EnemyHealth>();
        if(health != null)
        {
            healtTarget = health;
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(healtTarget != null)
        {
            if(CanAttack())
            {
                Attack();
            }
        }
    }

    private void Attack()
    {
       
    }

    private bool CanAttack()
    {
        return attackTimer >= attackRefreshRate;
    }
}
