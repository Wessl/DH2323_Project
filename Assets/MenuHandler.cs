using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    public GameObject menuPanel;
    public MouseLook mouseLook;
    private float _defaultMouseLook;
    public PlayerController playerController;
    public VoiceCommands voiceCommands;

    private void Start()
    {
        _defaultMouseLook = mouseLook.MouseSensitivity;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            Debug.Log("cancel pressed");
            if (!menuPanel.activeSelf)
            {
                menuPanel.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                mouseLook.MouseSensitivity = 0;
                playerController.CanMove = false;
                Time.timeScale = 0;
            }
            else
            {
                menuPanel.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                mouseLook.MouseSensitivity = _defaultMouseLook;
                playerController.CanMove = true;
                Time.timeScale = 1f;
            }
            
        }
    }
}
