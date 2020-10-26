using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpEffect : MonoBehaviour
{
    private Vector3 _direction = Vector2.zero;
    void Start()
    {
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(0.1f, 1f);
        _direction = new Vector3(x, y, 0);
        Destroy(gameObject, 0.4f);
    }
    private void Update()
    {
        transform.position += _direction * Time.deltaTime;
    }
}
