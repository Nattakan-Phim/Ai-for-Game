using System;
using System.Collections;
using System.Collections.Generic;
using _AiForGame.Scripts;
using UnityEngine;

public class Player : MonoBehaviour,IDamageable
{
    [SerializeField] private float speed;
    public event Action<int> OnTakeHit; 

    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(0, speed*Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-speed*Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, -speed*Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(speed*Time.deltaTime, 0, 0);
        }
    }

    public void TakeHit(int count)
    {
        OnTakeHit?.Invoke(count);
    }
}