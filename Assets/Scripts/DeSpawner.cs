﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeSpawner : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            Destroy(collision.gameObject);
        }
    }
}
