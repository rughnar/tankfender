using System;
using System.Collections;
using System.Collections.Generic;
using Tankfender;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.Tilemaps;

public class BulletBehaviour : MonoBehaviour
{
    public string objectiveTag = "Enemy";

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }

        if (other.gameObject.CompareTag(objectiveTag))
        {
            if (other.gameObject.tag == "Enemy")
            {
                other.gameObject.GetComponent<EnemyController>().TakeDamage();
            }
            else
            {
                other.gameObject.GetComponent<PlayerController>().TakeDamage();
            }
            Destroy(this.gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }


}
