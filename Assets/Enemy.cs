using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject player;
    public float windParticlePushForce;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnParticleCollision(GameObject other)
    {
        var direction = gameObject.transform.position - player.transform.position ;
        rb.AddForce(direction.normalized * windParticlePushForce, ForceMode.Force);
    }
}
