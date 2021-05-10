using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    // Following control the size of the collision sphere
    public Transform capsulePoint1;
    public Transform capsulePoint2;
    public float capsuleRadius;
    public float collisionForce;
    public LayerMask enemyLayer;
    public ParticleSystem waterSplash;
    public ParticleSystem waterSplashTextured;

    // Update is called once per frame
    void Update()
    {
        var hits = Physics.OverlapCapsule(capsulePoint1.position, capsulePoint2.position, capsuleRadius, enemyLayer);
        foreach (var hit in hits)
        {
            var dir = hit.transform.position - transform.position;
            hit.GetComponent<Rigidbody>().AddForce(dir.normalized * collisionForce, ForceMode.Impulse);
            var splash = Instantiate(waterSplash, hit.transform.position, Quaternion.identity);
            var splash2 = Instantiate(waterSplashTextured, hit.transform.position, Quaternion.identity);
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(capsulePoint1.position, capsuleRadius);
        Gizmos.DrawWireSphere(capsulePoint2.position, capsuleRadius);
        Gizmos.DrawLine(capsulePoint1.position, capsulePoint2.position);
    }
}
