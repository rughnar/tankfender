using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.Tilemaps;

public class BulletBehaviour : MonoBehaviour
{
    public string objectiveTag = "Enemy";

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag(objectiveTag))
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }

        if (other.gameObject.CompareTag("Wall"))
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }


}
