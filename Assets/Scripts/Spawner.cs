using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Objects")]
    [Tooltip("The prefab to spawn.")]
    public GameObject Prefab;
    [Tooltip("The number of this object to spawn.")]
    public int Quantity = 1;
    [Tooltip("Minimum spacing between objects spawned.")]
    public float spacing = 1;

    [Header("Location")]
    [Tooltip("The spawn location.")]
    public Vector3 Position = Vector3.zero;
    [Tooltip("The radius of the circle within which this spawner will create objects. Set to 0 to spawn only on the position above.")]
    public float Radius = 0;
    [Tooltip("The Y axies offset to use when deciding the spawn location. This will be aded to the Y spwarn coordinate.")]
    public float yOffset = 0;
    
    void Start()
    {
        Spawn();
    }

    public void Spawn()
    {
        Quaternion rotation = new Quaternion();
        for (int i = 0; i < Quantity; i++)
        {
            if (Radius > 0)
            {
                Vector2 circlePos = Random.insideUnitCircle * Radius;
                Position.x += circlePos.x;
                Position.z += circlePos.y;
            }
            Position.y += yOffset;
            Instantiate(Prefab, Position, rotation);
        }
    }
}
