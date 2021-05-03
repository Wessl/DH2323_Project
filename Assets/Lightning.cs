using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Lightning : MonoBehaviour
{
    private VisualEffect vfx;

    public float destroyAfterSeconds;
    // Start is called before the first frame update
    void Start()
    {
        vfx = GetComponent<VisualEffect>();
        Destroy(gameObject, destroyAfterSeconds);
    }
}
