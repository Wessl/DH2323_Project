using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    private ParticleSystem vfx;
    public float destroyAfterSeconds;

    public float windParticlePushForce;
    // Start is called before the first frame update
    void Start()
    {
        vfx = GetComponent<ParticleSystem>();
        Destroy(gameObject, destroyAfterSeconds);
    }
    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Enemy"))
        {
            var rb = other.GetComponent<Rigidbody>();
            var direction = other.transform.position - transform.position ;
            rb.AddForce(direction.normalized * windParticlePushForce, ForceMode.Force);
        }
    }
}
