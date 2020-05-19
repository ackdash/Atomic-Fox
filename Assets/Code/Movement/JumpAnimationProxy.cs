using System;
using System.Collections;
using System.Collections.Generic;
using Code.Movement;
using UnityEngine;

public class JumpAnimationProxy : MonoBehaviour
{
    private JumpController jumpController;
    public float JumpDeltaProp;
    
    private void Awake()
    {
        jumpController = GetComponentInParent<JumpController>();
    }
    
    private void Update()
    {
        jumpController.JumpDeltaProp = JumpDeltaProp;
    }
    
    public void OnJumpFinished()
    {
        jumpController.OnJumpFinished();
    }
}
