using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField]
    [InspectorName("Ground Tag")]
    private string groundTag;
    
    [SerializeField]
    [InspectorName("Layer Mask")]
    private LayerMask layerMask;
    
    [SerializeField]
    [InspectorName("Distance to ground")]
    private float radius;

    
    private bool isOnGround;

    public bool IsOnGround
    {
        get {
            if (isOnGround) return isOnGround;

            var check= Physics2D.OverlapCircle(transform.position, radius, layerMask);
           
            return (Object)check != null;
        }
    }
    
    private bool IsChecking { get;  set; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (IsChecking && other.CompareTag(groundTag))
        {
            isOnGround = true;
        }
    }

    public void Reset()
    {
        IsChecking = true;
        isOnGround = false;
    }
}
