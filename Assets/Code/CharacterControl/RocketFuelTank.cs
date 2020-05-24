using System;
using System.Collections;
using System.Collections.Generic;
using Code.Interfaces;
using Data;
using UnityEngine;

public class RocketFuelTank : MonoBehaviour
{
    [SerializeField]
    private FloatReference amountOfFuel;
    // Start is called before the first frame update
    void Start()
    {
        amountOfFuel.Value = 0f;
    }

    public void AddFuel(float amount)
    {
        amountOfFuel.Value += amount;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
   
        
        // Debug.Log($"Got Fuel! {other.tag}");
    }
    // private void  OnCollisionEnter2D(Collision2D other){
    //    
    //     Debug.Log($"Got Fuel! {other.gameObject.tag}");
    // }
}
