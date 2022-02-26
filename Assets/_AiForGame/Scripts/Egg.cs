using System;
using System.Collections;
using System.Collections.Generic;
using _AiForGame.Scripts;
using UnityEngine;

public class Egg : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var target = other.gameObject.GetComponent<IDamageable>();
        target?.TakeHit(1);
        Destroy(gameObject);
    }
}
