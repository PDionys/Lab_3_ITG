using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitBehaviours : MonoBehaviour
{
    [SerializeField]
    float speed;
    [SerializeField]
    float range;
    [SerializeField]
    float maxDistance;
    [SerializeField]
    float rotationSpeed;

    Vector2 wayPoint;

    void Start()
    {
        SetNewDestination();
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, wayPoint, speed * Time.deltaTime);

        if (wayPoint != Vector2.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, wayPoint);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        if (Vector2.Distance(transform.position, wayPoint) < range)
        {
            SetNewDestination();
        }
    }

    void SetNewDestination()
    {
        wayPoint = new Vector2(Random.Range(-maxDistance, maxDistance), Random.Range(-maxDistance, maxDistance));
    }
}
