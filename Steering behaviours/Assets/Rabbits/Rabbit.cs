using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : MonoBehaviour
{
    public Vector3 position;
    public Vector3 velocity;
    public Vector3 accelerration;

    public Level level;
    public RabbitConfig conf;

    //public List<Wall> walls;

    Vector3 wanderTarget;

    void Start()
    {
        level = FindObjectOfType<Level>();
        conf = FindObjectOfType<RabbitConfig>();
        //walls = new List<GameObject>();

        position = transform.position;
        velocity = new Vector3(Random.Range(-3, 3), Random.Range(-3, 3), 0);
    }

    private void Update()
    {
        accelerration = Combine();
        accelerration = Vector3.ClampMagnitude(accelerration, conf.maxAcceleration);
        velocity = velocity + accelerration * Time.deltaTime;
        velocity = Vector3.ClampMagnitude(velocity, conf.maxVelocity);
        position = position + velocity * Time.deltaTime;
        transform.position = position;
    }

    protected Vector3 Wander()
    {
        float jiter = conf.wanderJitter * Time.deltaTime;
        wanderTarget += new Vector3(RandomBinomial() * jiter, RandomBinomial() * jiter, 0);
        wanderTarget = wanderTarget.normalized;
        wanderTarget *= conf.wanderRadius;
        Vector3 targetInLocalSpace = wanderTarget + new Vector3(conf.wanderDistance, conf.wanderDistance, 0);
        Vector3 targetInWorldSpace = transform.TransformPoint(targetInLocalSpace);
        targetInWorldSpace -= this.position;
        return targetInWorldSpace.normalized;
    }

    Vector3 Avoidance()
    {
        Vector3 avoidVector = new Vector3();
        /*foreach(var wall in wallsList)
        {
            avoidVector += RunAway(wall.position);
        }*/
        var player = level.GetPlayer();
        avoidVector += RunAway(player.movement);
        return avoidVector.normalized;
    }

    Vector3 RunAway(Vector3 target)
    {
        Vector3 neededVelocity = (position - target).normalized * conf.maxVelocity;
        return neededVelocity - velocity;
    }

    virtual protected Vector3 Combine()
    {
        Vector3 finalVec = conf.wanderPriority * Wander() + conf.avoidancePriority * Avoidance();
        return finalVec;
    }

    float RandomBinomial()
    {
        return Random.Range(0f, 1f) - Random.Range(0f, 1f);
    }
}
