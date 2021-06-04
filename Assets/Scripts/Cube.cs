using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public GameObject shatteredCube;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PyroBall"))
        {
            Instantiate(shatteredCube, transform.position, transform.rotation);
            GameObject.FindWithTag("Audio").GetComponent<AudioPlayer>().PlayShatterSFX();
            Destroy(gameObject);
        }
    }
}
