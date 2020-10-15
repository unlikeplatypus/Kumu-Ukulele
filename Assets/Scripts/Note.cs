using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour
{
    public enum HitValue
    {
        Miss,
        Early,
        Perfect,
        Late
    }
    [SerializeField] private string _name = "";
    [SerializeField] private GameObject _icon = null;
    [SerializeField] private GameObject _hitCircle = null;
    [SerializeField] private ParticleSystem _particleEffect = null;
    [SerializeField] private Transform _fretPosition = null;
    [SerializeField] private bool _hit = false;
    [SerializeField] private InputManager _im = null;

    private float _aproachRate = 0.01f;
    private HitValue _value = HitValue.Miss;

    private void Awake()
    {
        _hitCircle.transform.localScale = new Vector3(2, 2, 1);
        StartCoroutine(ShrinkCircle());
    }

    // Update is called once per frame
    void Update()
    {
        if (_im.InputedNote == _name)
        {
            _hit = true;
        }
        float size = _hitCircle.transform.localScale.x;
        if (size > 1.3 && _hit)
        { 
            //Early Hit
            print("Early");
            _value = HitValue.Early;
            StopAllCoroutines();
        }
        else if (size >= 1 && size < 1.3 && _hit)
        {
            //Perfect Hit
            print("Perfect");
            _value = HitValue.Perfect;
            StopAllCoroutines();
        }
        else if(size < 1 && _hit)
        {
            //Late Hit
            print("Late");
            _value = HitValue.Late;
            StopAllCoroutines();
        }
        else if(size <= 0.9 && !_hit)
        {
            //Miss
            print("Miss");
            _value = HitValue.Miss;
            StopAllCoroutines();
        }

        if(_hit || ((size <= 0.9 && !_hit)))
        {
            print(_value);
            Destroy(this.gameObject);
        }

    }

    public HitValue Hit(string inputName)
    {
        if(inputName == _name)
        {
            _hit = true;
        }
        print(_value);
        return _value;
    }

    private IEnumerator ShrinkCircle()
    {
       while(_hitCircle.transform.localScale.x > 0.9f)
        {
            _hitCircle.transform.localScale += new Vector3(-1, -1, 0) * Time.deltaTime;
            yield return new WaitForSeconds(_aproachRate);
        }
        yield return null;
    }
}
