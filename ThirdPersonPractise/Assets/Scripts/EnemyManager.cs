using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public List<GameObject> enemyList;
    public static bool isTraverseAI;

    // Update is called once per frame
    void Update()
    {
        if (BallController.isBallGrounded)
        {
            isTraverseAI = false;
            if (!enemyList[0].GetComponent<EnemyAI>().isNearBall && !enemyList[1].GetComponent<EnemyAI>().isNearBall && BallController.isBallGrounded)
            {
                enemyList[0].GetComponent<EnemyAI>().enabled = true;
                enemyList[1].GetComponent<EnemyAI>().enabled = true;

                enemyList[0].GetComponent<EnemyTraverse>().enabled = false;
                enemyList[1].GetComponent<EnemyTraverse>().enabled = false;
            }
            else if (!enemyList[0].GetComponent<EnemyAI>().isNearBall && enemyList[1].GetComponent<EnemyAI>().isNearBall && BallController.isBallGrounded)
            {
                enemyList[1].GetComponent<EnemyAI>().enabled = true;
                enemyList[1].GetComponent<EnemyTraverse>().enabled = false;
                enemyList[0].GetComponent<EnemyAI>().enabled = false;
                enemyList[0].GetComponent<EnemyTraverse>().enabled = true;
                EnemyTraverse.CurrentTarget = Random.Range(2,6);
            }
            else if (enemyList[0].GetComponent<EnemyAI>().isNearBall && !enemyList[1].GetComponent<EnemyAI>().isNearBall && BallController.isBallGrounded)
            {
                enemyList[0].GetComponent<EnemyAI>().enabled = true;
                enemyList[0].GetComponent<EnemyTraverse>().enabled = false;
                enemyList[1].GetComponent<EnemyAI>().enabled = false;
                enemyList[1].GetComponent<EnemyTraverse>().enabled = true;
                EnemyTraverse.CurrentTarget = Random.Range(2,6);
                
            }

        }
        else
        {
            if(BallController.isBallToPlayer)
            {
                EnemyTraverse.CurrentTarget = Random.Range(2,6);
                enemyList[0].GetComponent<EnemyAI>().enabled = false;
                enemyList[1].GetComponent<EnemyAI>().enabled = false;
                enemyList[0].GetComponent<EnemyTraverse>().enabled = true;
                enemyList[1].GetComponent<EnemyTraverse>().enabled = true;
                isTraverseAI = true;
                

                
            }

            // enemy 0
            if (BallController.AIEnemyNum == 0 && !BallController.isBallGrounded && !BallController.isBallToPlayer)
            {
                enemyList[0].GetComponent<EnemyAI>().enabled = true;
                enemyList[0].GetComponent<EnemyTraverse>().enabled = false;
                enemyList[1].GetComponent<EnemyTraverse>().enabled = true;
                enemyList[1].GetComponent<EnemyAI>().enabled = false;
            }

            // enemy 1
            if (BallController.AIEnemyNum == 1 && !BallController.isBallGrounded && !BallController.isBallToPlayer ) 
            {
                enemyList[1].GetComponent<EnemyAI>().enabled = true;
                enemyList[1].GetComponent<EnemyTraverse>().enabled = false;
                enemyList[0].GetComponent<EnemyAI>().enabled = false;
                enemyList[0].GetComponent<EnemyTraverse>().enabled = true;
            }



        }

    }
}
