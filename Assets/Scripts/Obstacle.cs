using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] protected float _speed = 6f;

    public float Speed { get => _speed; set => _speed = value; }

    // Update is called once per frame
    void Update()
    {
        DefaultUpdate(Speed);
    }

    protected void DefaultUpdate(float f)
    {
        transform.position += new Vector3(-1, 0, 0) * f * Time.deltaTime;
    }

    enum Weakness
    {
        None,
        Fire,
        Wind,
        Ice
    }
}
