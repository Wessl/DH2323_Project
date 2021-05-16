using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PyroCasting : MonoBehaviour
{
    public GameObject pyroObject;
    public Transform instantiationPos;
    public float pyroSpeedMultiplier;
    public MouseLook mouseLook;
    private float currentSeconds;
    private float currentPyroCharge;
    private bool movingUp;

    private bool isHoldingDownMouse;
    
    // Start is called before the first frame update
    void Start()
    {
        movingUp = true;
    }

    public void SpawnPyro()
    {
        var spawnedPyroObject = Instantiate(pyroObject, instantiationPos.position, Quaternion.identity);
        var mouseVertical = mouseLook.verticalRotation;
        var vertRot = Quaternion.AngleAxis(mouseLook.verticalRotation, mouseLook.transform.right);
        var body = spawnedPyroObject.GetComponent<Rigidbody>();
        body.rotation = Quaternion.Euler(mouseVertical, 0, 0);
        body.AddForce(vertRot * transform.forward * (pyroSpeedMultiplier), ForceMode.Impulse);
    }

    public float CurrentPyroCharge => currentPyroCharge;
}
