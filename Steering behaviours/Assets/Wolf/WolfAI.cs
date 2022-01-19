using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfAI : MonoBehaviour
{
    private enum State
    {
        Wandering,
        ChaseTarget,
        GoingBackToStar,
    }

    public float moveSpeed = 5f;
    public Transform player;
    public Transform rabbit;
    public float targetRange = 5f;
    public float stopChaseDistance = 8f;
    public float reachedPositionDistance = 1f;

    private Vector2 movement;
    private Rigidbody2D rb;
    private State state;
    private Vector3 startingPosition;
    private string whatistarget;
    private Vector3 direction;
    private float starterMoveSpeed;

    [SerializeField]
    float range;
    [SerializeField]
    float maxDistance;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        state = State.Wandering;
        startingPosition = transform.position;
        starterMoveSpeed = moveSpeed;

        SetNewDestination();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        switch (state)
        {
            default:
                break;
            case State.Wandering:
                transform.position = Vector2.MoveTowards(transform.position, movement, moveSpeed * Time.deltaTime);
                startingPosition = transform.position;
                if (Vector2.Distance(transform.position, movement) < range)
                {
                    SetNewDestination();
                }
                FindTarget();
                break;
            case State.ChaseTarget:
                if(whatistarget == "Player")
                {
                    direction = player.position - transform.position;
                }else if(whatistarget == "Rabbit")
                {
                    direction = rabbit.position - transform.position;
                }
                //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                //rb.rotation = angle;
                direction.Normalize();
                movement = direction;
                moveCharacter(movement);

                //float stopChaseDistance = 8f;
                if (Vector3.Distance(transform.position, player.position) > stopChaseDistance)
                {
                    state = State.GoingBackToStar;
                }
                break;
            case State.GoingBackToStar:
                direction = startingPosition - transform.position;
                //angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                //rb.rotation = angle;
                direction.Normalize();
                movement = direction;
                moveCharacter(movement);
                FindTarget();

                //float reachedPositionDistance = 1f;
                if (Vector3.Distance(transform.position, startingPosition) < reachedPositionDistance)
                {
                    state = State.Wandering;
                    moveSpeed = starterMoveSpeed;
                }
                break;
        }
    }

    void moveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }

    private void FindTarget()
    {
        if (Vector3.Distance(transform.position, player.position) < targetRange)
        {
            state = State.ChaseTarget;
            whatistarget = "Player";
            moveSpeed += 2f;
        }else if(Vector3.Distance(transform.position, rabbit.position) < targetRange)
        {
            state = State.ChaseTarget;
            whatistarget = "Rabbit";
        }
    }

    void SetNewDestination()
    {
        movement = new Vector2(Random.Range(-maxDistance, maxDistance), Random.Range(-maxDistance, maxDistance));
    }
}
