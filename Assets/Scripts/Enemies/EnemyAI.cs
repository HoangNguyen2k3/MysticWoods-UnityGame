using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    //A* algorithms
    [Header("Pathfinding")]
    [SerializeField] private bool IsFollowPlayer = false;
    [SerializeField] private Seeker seeker;
    [SerializeField] private GameObject target;
    private Path path;
    private Coroutine moveCoroutine;
    [SerializeField] private float nextWPDistance=0.3f;
    [SerializeField] private float speedFollow = 5f;
    [SerializeField] public float attackFollow = 0f;
    //
    [Header("EnemyAI Normal")]
    [SerializeField] private float roamChangeDirFloat = 2f;
    [SerializeField] public float attackRange = 0f;
    [SerializeField] private MonoBehaviour enemyType;
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private bool stopMovingWhileAttacking = false;

    public bool canAttack = true;
    public bool isDead = false;

    private enum State
    {
        Roaming,
        Attacking,
        FollowPlayer
    }

    private Vector2 roamPosition;
    private float timeRoaming = 0f;

    private State state;
    private EnemyPathFinding enemyPathfinding;

    private void Awake()
    {
        enemyPathfinding = GetComponent<EnemyPathFinding>();
        state = State.Roaming;
        
    }

    private void Start()
    {
        target = FindObjectOfType<Playercontroller>().gameObject;
        roamPosition = GetRoamingPosition();
    }

    private void Update()
    {
        
        MovementStateControl();
    }

    private void MovementStateControl()
    {
        switch (state)
        {
            default:
            case State.Roaming:
                Roaming();
                break;

            case State.Attacking:
                Attacking();
                break;
            case State.FollowPlayer:
                FollowingPlayer();
                break;
        }
    }

    private void Roaming()
    {
        timeRoaming += Time.deltaTime;
        enemyPathfinding.moveSpeed = 2f;
        enemyPathfinding.MoveTo(roamPosition);
        if (Playercontroller.Instance)
        {
            if (Vector2.Distance(transform.position, Playercontroller.Instance.transform.position) < attackRange)
            {
                state = State.Attacking;
            }else if(Vector2.Distance(transform.position, Playercontroller.Instance.transform.position) >= attackRange&&
                Vector2.Distance(transform.position, Playercontroller.Instance.transform.position) < attackFollow&&
                IsFollowPlayer)
            {
                state = State.FollowPlayer;
            }
        }
        if (timeRoaming > roamChangeDirFloat)
        {
            roamPosition = GetRoamingPosition();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Vector2.Distance(transform.position, Playercontroller.Instance.transform.position) > attackFollow)
        {
            roamPosition *= (-1);
        }

    }
    private void FollowingPlayer()
    {
        enemyPathfinding.StopMoving();
        if (Vector2.Distance(transform.position, Playercontroller.Instance.transform.position) > attackFollow)
        {
            state = State.Roaming;
        }else if(Vector2.Distance(transform.position, Playercontroller.Instance.transform.position) < attackRange
            )
        {
            state = State.Attacking;
        }
        /*Vector2 direction = (Playercontroller.Instance.transform.position - transform.position).normalized;
        enemyPathfinding.moveSpeed = speedFollow;
        enemyPathfinding.MoveTo(direction);*/
        CalculatePath();
    }
    private void Attacking()
    {
        if (isDead) return;
        if (Playercontroller.Instance)
        {
            if (Vector2.Distance(transform.position, Playercontroller.Instance.transform.position) > attackFollow)
        {
            state = State.Roaming;
        }
            else if (Vector2.Distance(transform.position, Playercontroller.Instance.transform.position) >= attackRange &&
                Vector2.Distance(transform.position, Playercontroller.Instance.transform.position) <= attackFollow &&
                IsFollowPlayer)
            {
                state = State.FollowPlayer;
            }
        }
        

        if (attackRange != 0 && canAttack)
        {

            canAttack = false;
            if (enemyType != null)
            {
                (enemyType as IEnemy).Attack();
            }

            enemyPathfinding.moveSpeed = 2f;
            if (stopMovingWhileAttacking)
            {
                enemyPathfinding.StopMoving();
            }
            else
            {
                enemyPathfinding.MoveTo(roamPosition);
            }

            StartCoroutine(AttackCooldownRoutine());
        }
    }

    private IEnumerator AttackCooldownRoutine()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    private Vector2 GetRoamingPosition()
    {
        timeRoaming = 0f;
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
    //A*
    private void CalculatePath()     
    {
       // if (seeker.IsDone())
       // {
            seeker.StartPath(transform.position,target.transform.position,OnPathCallBack);
       // }
    }
    private void OnPathCallBack(Path p)
    {
        if (p.error)
        {
            return;
        }
        path = p;
        MoveToTarget();
        //Move To Target
    }
    private void MoveToTarget()
    {
/*        if(moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }*/
        moveCoroutine = StartCoroutine(MoveToTargetCoroutine());
    }
   private IEnumerator MoveToTargetCoroutine()
    {
        int currentWP = 0;
        while (currentWP < path.vectorPath.Count)
        {
            Vector2 direction = ((Vector2)path.vectorPath[currentWP]-(Vector2)transform.position).normalized;
            enemyPathfinding.moveSpeed = speedFollow;
            enemyPathfinding.MoveTo(direction);
            if (Vector2.Distance(transform.position, Playercontroller.Instance.transform.position) > attackFollow)
            {
                state = State.Roaming;
                break;
            }
            else if (Vector2.Distance(transform.position, Playercontroller.Instance.transform.position) < attackRange)
            {
                state = State.Attacking;
                break;
            }
            float distance = Vector2.Distance(transform.position, path.vectorPath[currentWP]);
            if (distance < nextWPDistance)
            {
                currentWP++;
            }
            yield return null;
        }
    }
}
