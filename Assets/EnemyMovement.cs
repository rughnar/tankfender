using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField] private GameObject objective;
    [SerializeField] private float distanceToShoot;

    // Update is called once per frame
    void Update()
    {
        //if (Vector2.Distance(objective.transform.position, this.transform.position) <= distanceToShoot) Shoot();
        //else Move();
    }

    void Shoot()
    {

    }

    void Move()
    {

    }
}
