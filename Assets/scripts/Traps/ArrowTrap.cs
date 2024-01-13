using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject[] fireballs;
    private float cooldowntimer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    
    private void Attack()
    {
        cooldowntimer = 0;
        fireballs[FindFireball()].transform.position = firepoint.position;
        fireballs[FindFireball()].GetComponent<EnemyProjectile>().ActivateProjectile();
    }

    // private int FindFireball()
    // {
    //      for (int i = 0; i < fireballs.Length; i++)
    //     {
    //         if (!fireballs[i].activeInHierarchy)
    //         {
    //             return i;
    //         }
    //         return 0;
    //     }
    // }
    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }
    void Update()
    {
       
        cooldowntimer += Time.deltaTime;
        if (cooldowntimer >= attackCooldown)
            Attack();
    }
}
