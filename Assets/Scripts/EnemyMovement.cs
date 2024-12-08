using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.UIElements;


public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private GameObject head;
    [SerializeField] private float timeBetweenMovementChange = 5f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float movementSpeed = 4f;
    [SerializeField] private float turnSpeed = 90f;
    private float lastMovementChange = 0f;

    private int[] angles;

    void Awake()
    {
        lastMovementChange = 0f;
        rb = GetComponent<Rigidbody2D>();
        angles = new int[] { 0, 90, 180, 270 };
    }

    void FixedUpdate()
    {
        if (Time.fixedTime - lastMovementChange < timeBetweenMovementChange)
        {
            Move();
        }
        else
        {
            Turn();
        }

    }

    void Move()
    {
        Vector2 movement = transform.up * movementSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + movement);
    }

    void Turn()
    {
        rb.SetRotation(angles[Random.Range(0, 4)] * (Random.Range(0, 2) * 2 - 1));
        lastMovementChange = Time.fixedTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Limit") || other.gameObject.CompareTag("Border"))
        {
            rb.SetRotation(rb.rotation + 90 * (Random.Range(0, 2) * 2 - 1));
        }
    }
}
