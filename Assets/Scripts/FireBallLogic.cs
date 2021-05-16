using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireBallLogic : MonoBehaviour
{
    private Rigidbody rb;
    public Material extinguishedMat;
    private MeshRenderer mesh;
    public ParticleSystem extinguishSmoke;
    private bool extinguished;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        mesh = GetComponent<MeshRenderer>();
        extinguished = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Hit new position
            // Also shatter cube into pieces
            
        }
    }

    public void Extinguish()
    {
        if (!extinguished)
        {
            mesh.material = extinguishedMat;
            extinguished = true;  
            var smonkSystems = GetComponentsInChildren<ParticleSystem>();
            foreach (var smonk in smonkSystems)
            {
                smonk.Stop();
            }

            Instantiate(extinguishSmoke, transform.position, Quaternion.identity); 
             
        }
        
    }
}
