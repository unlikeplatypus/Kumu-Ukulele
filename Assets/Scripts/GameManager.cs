using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Song _song = null;
    [SerializeField] private float _startDelay = 0f;
    [SerializeField] private InputManager _im = null;

    [SerializeField] private int _score = 0;
    [SerializeField] private int _combo = 0;
    [SerializeField] private float _time = 0;
    [SerializeField] private float _hp = 100;
    [SerializeField] private Slider _hpBar = null;
    [SerializeField] private Image _hpBarFill = null;
    [SerializeField] private TextMeshProUGUI _scoreTxt = null;
    [SerializeField] private TextMeshProUGUI _comboTxt = null;
    [SerializeField] private TextMeshProUGUI _timeTxt = null;
    [SerializeField] private GameObject _passScreen = null;
    [SerializeField] private GameObject _failScreen = null;

    private bool _start = false;

    private int _currentIndex = 0;

    private bool _pauseNotes = false;

    private bool _gameOver = false;
    private bool _won = false;

    public int Score { 
        get => _score;
        set 
        {
            _score = value;
            _scoreTxt.text = string.Format("{0:D5}", _score);
        }
    }

    public int Combo { 
        get => _combo; 
        set
        {
            _combo = value;
            _comboTxt.text = "x" + _combo;
            _comboTxt.GetComponent<Animator>().SetTrigger("Run");
        }
    }

    public float Counter
    {
        get => _time;
        set
        {
            _time = value;
            _timeTxt.text = $"{_time.ToString("0.00")}/{_song.Length.ToString("0.00")}";
        }
    }

    public float Hp { get => _hp;
        set
        {
            _hp = value;
            print(_hp);
            _hpBar.value = _hp;
            _hpBar.GetComponent<Animator>().SetTrigger("Run");
            if (_hp > 50)
            {
                _hpBarFill.color = Color.cyan;
            }
            else if (_hp <= 50 && _hp > 20)
            {
                _hpBarFill.color = Color.yellow;
            }
            else if(_hp <=20 && _hp > 0)
            {
                _hpBarFill.color = Color.red;
            }
            else
            {
                _start = false;
                Won = false;
                GameOver = true;
            }
        }
    }

    public bool GameOver
    {
        get => _gameOver;
        set
        {
            _gameOver = value;
            if(Won)
            {
                _passScreen.SetActive(true);
            }
            else
            {
                _failScreen.SetActive(true);
            }
        }
    }

    public bool Won { get => _won; set => _won = value; }

    // Start is called before the first frame update
    void Start()
    {
        float length = 0;
        foreach(NoteType note in _song.Notes)
        {
            if (note.IsNote)
            {
                length += 0.43f;
            }
            else
            {
                length += (note as Delay).Time;
            }
        }
        _song.Length = length;
        Counter = 0;
        StartCoroutine(DelayStart());
        StartCoroutine(CountTimer());
    }

    // Update is called once per frame
    void Update()
    {
        if(Counter >= _song.Length && !GameOver)
        {
            Won = true;
            GameOver = true;
        }
        if (!_start)
        {
            return;
        }
        if (!_pauseNotes) StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        _pauseNotes = true;
        if (_currentIndex >= _song.Notes.Length)
        {
            _start = false;
        }
        else
        {
            if (_song.Notes[_currentIndex].IsNote)
            {
                SpawnNote();
            }
            else
            {
                yield return new WaitForSeconds((_song.Notes[_currentIndex] as Delay).Time);
            }
            _currentIndex++;
            _pauseNotes = false;
        }
    }
    private void SpawnNote()
    {
        int stringNum = (_song.Notes[_currentIndex] as Note).String;
        int fretNum = (_song.Notes[_currentIndex] as Note).Fret;
        int x = 0, y = 0;

        switch (stringNum)
        {
            case 1:
                y = -2;
                break;
            case 2:
                y = 0;
                break;
            case 3:
                y = 2;
                break;
            case 4:
                y = 4;
                break;
            default:
                break;
        }
        switch (fretNum)
        {
            case 0:
                x = -9;
                break;
            case 1:
                x = -7;
                break;
            case 2:
                x = -5;
                break;
            case 3:
                x = -3;
                break;
            case 4:
                x = -1;
                break;
            case 5:
                x = 1;
                break;
            case 6:
                x = 3;
                break;
            case 7:
                x = 5;
                break;
            case 8:
                x = 7;
                break;
            default:
                break;
        }
        Note note = Instantiate((_song.Notes[_currentIndex] as Note), new Vector3(x, y, 0), Quaternion.identity);
        note.Im = _im;
        note.Gm = this;
    }
    private IEnumerator DelayStart()
    {
        yield return new WaitForSeconds(_startDelay);
        _start = true;
    }
    private IEnumerator CountTimer()
    {
        while(Counter < _song.Length)
        {
            if(Counter + 1 * Time.deltaTime != _song.Length)
            { 
                Counter += 1f * Time.deltaTime;
            }
            yield return null;
        }
    }
}
