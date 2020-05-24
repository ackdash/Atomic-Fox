using System.Collections;
using System.Collections.Generic;
using Code.Events.Core;
using Code.Interfaces;
using Code.Player.Human;
using Data;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{

    public GameObject[] players;
   
    void Start()
    {
        Debug.Log(players.Length);
    }

    void Update()
    {
        
    }

    public void LevelComplete()
    {
        foreach (var player in players)
        {
            var inputProxy = player.GetComponent<PlayerInputProxy>();
            
            if (inputProxy.team)
            {
                var completor = inputProxy.team.GetComponent<ICompletor>();
                Debug.Log($"{inputProxy.team.tag} Won => {(completor.TargetReached()).ToString()}");
            }
        }
    }
}
