using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTraverse : MonoBehaviour
{
    [SerializeField] Transform Target1;
    [SerializeField] Transform Target2;
    [SerializeField] Transform Target3;
    [SerializeField] Transform Target4;
    [SerializeField] Transform Target5;
    [SerializeField] Transform Target6;
    [SerializeField] Transform Target7;

    [SerializeField] public static int CurrentTarget = 1;

    private Transform TargetPosition;
    private bool Contact = false;

    // Start is called before the first frame update
    void Start()
    {
        Contact = false;
        TargetPosition = Target1;
        CurrentTarget = 1;

        MoveToTarget();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyTarget"))
        {
            if (Contact == false)
            {
                Contact = true;
                CurrentTarget++;
                if (CurrentTarget > 7)
                {
                    CurrentTarget = 1;
                }

                MoveToTarget();
            }
        }
    }

    public void MoveToTarget()
    {
        if (CurrentTarget == 1)
        {
            TargetPosition = Target1;
            Contact = false;
        }
        if (CurrentTarget == 2)
        {
            TargetPosition = Target2;
            Contact = false;
        }
        if (CurrentTarget == 3)
        {
            TargetPosition = Target3;
            Contact = false;
        }
        if (CurrentTarget == 4)
        {
            TargetPosition = Target4;
            Contact = false;
        }
        if (CurrentTarget == 5)
        {
            TargetPosition = Target5;
            Contact = false;
        }
        if (CurrentTarget == 6)
        {
            TargetPosition = Target6;
            Contact = false;
        }
        if (CurrentTarget == 7)
        {
            TargetPosition = Target7;
            Contact = false;
        }

        GetComponent<NavMeshAgent>().destination = TargetPosition.position;

        Contact = false;
    }

}
