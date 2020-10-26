using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Note : NoteType
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
    [SerializeField] private GameObject _popUpTextPrefab = null;
    [SerializeField] private TextMeshProUGUI _text = null;
    [SerializeField] private bool _hit = false;
    [SerializeField] private InputManager _im = null;
    [SerializeField] private GameManager _gm = null;

    [SerializeField] private int _string = 1;
    [SerializeField] private int _fret = 1;

    private HitValue _value = HitValue.Miss;

    public int String { get => _string; set => _string = value; }
    public int Fret { get => _fret; set => _fret = value; }
    public InputManager Im { get => _im; set => _im = value; }
    public GameManager Gm { get => _gm; set => _gm = value; }

    private void Awake()
    {
        _hitCircle.transform.localScale = new Vector3(2, 2, 1);
        _text.text = Fret.ToString();
        StartCoroutine(ShrinkCircle());
    }

    // Update is called once per frame
    void Update()
    {
        float size = _hitCircle.transform.localScale.x;
        if (Im.InputedNote == _name && size < 1.15f)
        {
            _hit = true;
        }
        if (size > 1.3 && _hit)
        { 
            //Early Hit
            print("Early");
            _value = HitValue.Early;
            StopAllCoroutines();
        }
        else if (size >= 1 && size < 1.15 && _hit)
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
            switch (_value)
            {
                case HitValue.Miss:
                    _gm.Combo = 0;
                    _gm.Hp -= 25;
                    break;
                case HitValue.Early:
                case HitValue.Late:
                    Gm.Score += 50;
                    _gm.Combo += 1;
                    break;
                case HitValue.Perfect:
                    Gm.Score += 100;
                    _gm.Hp += 5;
                    _gm.Combo += 1;
                    break;
                default:
                    break;
            }
            TextMeshPro text = Instantiate(_popUpTextPrefab, transform.position, Quaternion.identity).GetComponent<TextMeshPro>();
            text.text = _value.ToString();
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
            yield return new WaitForFixedUpdate();
        }
        yield return null;
    }
}
