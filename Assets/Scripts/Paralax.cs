using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private RunnerGameManager _gm = null;

    // Start is called before the first frame update
    void Start()
    {
        _gm = FindObjectOfType<RunnerGameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_gm.GamePaused || !_gm.Player.IsAlive) return;
        transform.position += new Vector3(-1, 0, 0) * _speed * Time.deltaTime;
        if(transform.position.x < -14.5f)
        {
            transform.position = new Vector3( 27.3f, transform.position.y, transform.position.y);
        }
    }
}
