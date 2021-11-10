using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControllerDynamic : MonoBehaviour
{
    Rigidbody rb;
    SphereCollider sc;

    public float throwSpeed = 10f;
    public Transform CatchPoint;
    public List<Transform> AICatchPoint;
    public Transform shootingPoint;
    public List<Transform> AIShootingPoint;
    public static int catchValue;
    public static int AICatchValue;
    public static bool isBallGrounded;
    public static bool isBallToAI;

    public static int AIEnemyNum;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sc = GetComponent<SphereCollider>();
        catchValue = 3;
        AICatchValue = 3;
        isBallGrounded = true;
        isBallToAI = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (catchValue == 2) // Player catch
        {
            // catch the ball
            gameObject.transform.position = new Vector3(CatchPoint.position.x, CatchPoint.position.y, CatchPoint.position.z);
            sc.enabled = false; // collider set to false
            isBallGrounded = false;
            isBallToAI = false;
        }

        else if (AICatchValue == 2) // AI catch ball
        {
            // catch the ball
            gameObject.transform.position = new Vector3(AICatchPoint[AIEnemyNum].position.x, AICatchPoint[AIEnemyNum].position.y, AICatchPoint[AIEnemyNum].position.z);
            sc.enabled = false; // collider set to false
            isBallGrounded = false;
            isBallToAI = true;
        }

        else if (AICatchValue == 0 && !isBallGrounded && isBallToAI) // AI throw Ball
        {

            gameObject.transform.position = new Vector3(AIShootingPoint[AIEnemyNum].position.x, AIShootingPoint[AIEnemyNum].position.y, AIShootingPoint[AIEnemyNum].position.z);
            rb.velocity = (AIShootingPoint[AIEnemyNum].transform.forward * throwSpeed);  // throw to forward direction

            isBallGrounded = true;
            isBallToAI = false;
            AICatchValue = 3;
        }

        else if (catchValue == 0 && !isBallGrounded) // Player throw Ball
        {
            gameObject.transform.position = new Vector3(shootingPoint.position.x, shootingPoint.position.y, shootingPoint.position.z);
            rb.velocity = (shootingPoint.transform.forward * throwSpeed);  // throw to forward direction

            isBallGrounded = true;
            catchValue = 3;
        }
        else
        {
            sc.enabled = true;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("LegSensor"))
        {
            //gameObject.transform.position = new Vector3(point.position.x, point.position.y, point.position.z);
            catchValue = 1; // start Lifting animation, code > ThirdPersonController
        }

        else if (other.gameObject.CompareTag("AILegSensor0") && catchValue != 1)
        {
            //gameObject.transform.position = new Vector3(point.position.x, point.position.y, point.position.z);
            AICatchValue = 1; // start Lifting animation, code > EnemyAI
            AIEnemyNum = 0;
        }

        else if (other.gameObject.CompareTag("AILegSensor1") && catchValue != 1)
        {
            //gameObject.transform.position = new Vector3(point.position.x, point.position.y, point.position.z);
            AICatchValue = 1; // start Lifting animation, code > EnemyAI
            AIEnemyNum = 1;
        }
    }
}
