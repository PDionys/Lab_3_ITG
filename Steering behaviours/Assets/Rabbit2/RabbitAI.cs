using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitAI : MonoBehaviour
{
    private enum State
    {
        Wandering,
        Runnig,
    }

    public float moveSpeed = 5f;
    public Transform player;
    public Transform wolf;
    public float targetRange = 5f;
    public float stopRunnigDistance = 8f;
    public float reachedPositionDistance = 1f;

    private Vector2 movement;
    private Rigidbody2D rb;
    private State state;
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
        //startingPosition = transform.position;
        starterMoveSpeed = moveSpeed;
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
                //startingPosition = transform.position;
                if (Vector2.Distance(transform.position, movement) < range)
                {
                    SetNewDestination();
                }
                FindTarget();
                break;
            case State.Runnig:
                direction = player.position + transform.position;
                //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                //rb.rotation = angle;
                direction.Normalize();
                movement = direction;
                moveCharacter(movement);

                //float stopChaseDistance = 8f;
                if (Vector3.Distance(transform.position, player.position) > stopRunnigDistance)
                {
                    moveSpeed = starterMoveSpeed;
                    state = State.Wandering;
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
            state = State.Runnig;
            moveSpeed += 2f;
        }
    }

    void SetNewDestination()
    {
        movement = new Vector2(Random.Range(-maxDistance, maxDistance), Random.Range(-maxDistance, maxDistance));
    }
}
