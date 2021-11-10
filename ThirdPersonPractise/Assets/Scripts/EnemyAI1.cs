using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI1 : MonoBehaviour
{
    [SerializeField] Transform Ball;
    [SerializeField] Transform Player;
    [SerializeField] float ChaseSpeed = 10.5f;
    [SerializeField] GameObject Patrol;
    [SerializeField] float AttackDistance;

    public float DistanceToBall;
    public float DistanceToPlayer;

    private bool RunToBall = false;
    private bool RunToPlayer = false;
    private Animator anim;
    private NavMeshAgent nav;

    // Enemy traverse targets
    [SerializeField] Transform Target1;
    [SerializeField] Transform Target2;
    [SerializeField] Transform Target3;

    [SerializeField] int CurrentTarget = 1;

    private Transform TargetPosition;
    private bool Contact = false;

    bool isAILiftingCompleted = false;

    // ANimation Timeouts
    public float ThrowTimeout = 1.5f;
    public float LiftingTimeout = 1.5f;
    public float RunToThrowTimeout = 2.0f;


    //time out deltatime
    private float _throwTimeoutDelta;
    private float _liftingTimeoutDelta;
    private float _runToThrowTimeoutDelta;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        Patrol.gameObject.SetActive(false);
        anim.SetFloat("Speed", 0.0f);
        anim.SetBool("Grounded", true);
        anim.SetFloat("MotionSpeed", 1);

        // reset timeout on start
        _throwTimeoutDelta = ThrowTimeout;
        _liftingTimeoutDelta = LiftingTimeout;
        _runToThrowTimeoutDelta = RunToThrowTimeout;

        // Enemy traverse
        TargetPosition = Target1;
        MoveToTarget();

    }



    private void OnTriggerEnter(Collider other)
    {
      /*  if (other.gameObject.CompareTag("TriggerBall") && BallController.isBallGrounded)
        {
            RunToBall = true;
        }
        else
        {
            RunToBall = false;
            MoveToTarget();
        }
*/
        // Enemy traverse
        if (other.gameObject.CompareTag("EnemyTarget") && !BallController.isBallGrounded)
        {
            if (Contact == false)
            {
                Contact = true;
                CurrentTarget++;
                if (CurrentTarget > 3)
                {
                    CurrentTarget = 1;
                }

                MoveToTarget();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (RunToBall == true) // AI runs towards Ball
        {
            DistanceToBall = Vector3.Distance(Ball.position, transform.position);

            nav.speed = ChaseSpeed;
            nav.SetDestination(Ball.position);
            anim.SetFloat("Speed", 5);
            anim.SetFloat("MotionSpeed", 1);
            if (DistanceToBall < AttackDistance)
            {
                //anim.SetBool("Alert",false);
            }
            else if (DistanceToBall > AttackDistance)
            {
                //anim.SetBool("Alert",true);
            }
        }
        else if (RunToPlayer == true) // AI runs towards player
        {
            DistanceToPlayer = Vector3.Distance(Player.position, transform.position);

            nav.speed = ChaseSpeed;
            nav.SetDestination(Player.position);
            anim.SetFloat("Speed", 5);
            anim.SetFloat("MotionSpeed", 1);
            if (DistanceToPlayer < AttackDistance)
            {
                //anim.SetBool("Alert",false);
            }
            else if (DistanceToPlayer > AttackDistance)
            {
                //anim.SetBool("Alert",true);
            }
        }

        if (BallController.isBallGrounded)
        {
            RunToBall = true;
        }

        if (!BallController.isBallGrounded && !RunToPlayer && !isAILiftingCompleted)
        {
            RunToBall = false;
            MoveToTarget();
        }

        AILiftingBall();
        RunToPlayerAndThrow();


        /*   if(isAILiftingCompleted && !BallController.isBallGrounded)
           {
               //StartCoroutine(RunToPlayerAndThrow());

           }
           */
    }


    // Enemy traverse
    void MoveToTarget()
    {
        anim.SetFloat("Speed", 5);
        anim.SetFloat("MotionSpeed", 1);

        if (CurrentTarget == 1)
        {
            TargetPosition = Target1;
        }
        if (CurrentTarget == 2)
        {
            TargetPosition = Target2;
        }
        if (CurrentTarget == 3)
        {
            TargetPosition = Target3;
        }

        GetComponent<NavMeshAgent>().destination = TargetPosition.position;

        Contact = false;
    }

    private void AILiftingBall()
    {
        if (BallController.AICatchValue == 1)
        {
            anim.SetBool("Lifting", true);
            isAILiftingCompleted = false;

            StartCoroutine(waitForLifting());

        }
    }

    IEnumerator waitForLifting()
    {
        yield return new WaitForSeconds(0.6f);
        anim.SetBool("Lifting", false);
        BallController.AICatchValue = 2;
        yield return new WaitForSeconds(0.85f);
        isAILiftingCompleted = true;


    }

    /*   IEnumerator RunToPlayerAndThrow()
       {
           RunToBall = false;
           RunToPlayer = true;
           yield return new WaitForSeconds(2f);
           anim.SetBool("Throw", true);
           yield return new WaitForSeconds(0.7f); // waiting to complete throwing animation
           BallController.AICatchValue = 0;
           anim.SetBool("Throw", false);
           RunToPlayer = false;
           isAILiftingCompleted = false;
           //yield return new WaitForSeconds(0.5f);
           //RunToBall = true;
           MoveToTarget();

       }
   */
    private void RunToPlayerAndThrow()
    {

        if (isAILiftingCompleted && !BallController.isBallGrounded)
        {
            RunToBall = false;
            RunToPlayer = true;
            //_runToThrowTimeoutDelta -= Time.deltaTime;
            if (_runToThrowTimeoutDelta <= 0.0f)
            {
                anim.SetBool("Throw", true);
                //RunToPlayer = false;
            }

            if (_runToThrowTimeoutDelta >= 0.0f)
            {
                _runToThrowTimeoutDelta -= Time.deltaTime;
            }

            if (_throwTimeoutDelta <= 0.0f)
            {
                BallController.AICatchValue = 0;
                anim.SetBool("Throw", false);
                RunToPlayer = false;
                isAILiftingCompleted = false;
                //yield return new WaitForSeconds(0.5f);
                //RunToBall = true;
                MoveToTarget();
            }

            if (_runToThrowTimeoutDelta <= 0.0f && _throwTimeoutDelta >= 0.0f)
            {
                _throwTimeoutDelta -= Time.deltaTime;
            }


        }
        else
        {
            _runToThrowTimeoutDelta = RunToThrowTimeout; // reset timeout
_throwTimeoutDelta = ThrowTimeout;



        }


        //yield return new WaitForSeconds(2f);

        //yield return new WaitForSeconds(0.7f); // waiting to complete throwing animation



    }


}
