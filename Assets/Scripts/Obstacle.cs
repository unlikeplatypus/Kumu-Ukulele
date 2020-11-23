using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] protected float _speed = 6f;

    [SerializeField] private DamageType _weakness;

    [SerializeField] private GameObject _scorePopUp = null;

    public float Speed { get => _speed; set => _speed = value; }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(-1, 0, 0) * _speed * Time.deltaTime;
    }

    public void TakeDamage(DamageType type)
    {
        if (type == _weakness || _weakness == DamageType.Any)
        {
            Instantiate(_scorePopUp, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    public enum DamageType
    {
        Any,
        Fire,
        Ice,
        Lightning,
        None
    }
}
