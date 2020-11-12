using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;

    public float Speed { get => _speed; set => _speed = value; }

    // Update is called once per frame
    void Update()
    {
        DefaultUpdate();
    }

    protected void DefaultUpdate()
    {
        transform.position += new Vector3(-1, 0, 0) * Speed * Time.deltaTime;
    }
}
