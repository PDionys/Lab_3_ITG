using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public Transform rabitPrefab;
    public int numberOfRabbits;
    public List<Rabbit> rabbits;
    public List<Enemy> enemies;
    public float spawnRadius;

    public PlayerMovement player;
    void Start()
    {
        rabbits = new List<Rabbit>();
        enemies = new List<Enemy>();

        Spawn(rabitPrefab, numberOfRabbits);

        //rabbits.AddRange((IEnumerable<Rabbit>)FindObjectOfType<Rabbit>());
        //enemies.AddRange((IEnumerable<Enemy>)FindObjectOfType<Enemy>());
    }

    void Spawn(Transform prefab, int count)
    {
        for(int i = 0; i < count; i++)
        {
            Instantiate(prefab, new Vector3(Random.Range(-spawnRadius, spawnRadius), Random.Range(-spawnRadius, spawnRadius), 0), Quaternion.identity);
        }
    }

    public PlayerMovement GetPlayer()
    {
        PlayerMovement pm = player;

        return pm;
    }
}
