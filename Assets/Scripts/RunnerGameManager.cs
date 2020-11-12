using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RunnerGameManager : MonoBehaviour
{
    [SerializeField] private GameObject _spawer = null;
    [SerializeField] private float _spawnDelay = 1f;
    [SerializeField] private float _objectSpeed = 1f;

    [SerializeField] private Obstacle[] _obstacles = null;

    [SerializeField] private Player _player = null;

    [SerializeField] private GameObject _gameOverScreen = null;
    [SerializeField] private GameObject _pauseMenu = null;
    [SerializeField] private TextMeshProUGUI _scoreTxt = null;

    private int _score = 0;

    private bool _gamePaused = false;
    private bool _run = true;
    private bool _pauseSpawn = true;

    public bool GamePaused
    {
        get => _gamePaused; set
        {
            _gamePaused = value;
            _pauseMenu.SetActive(_gamePaused);
            Time.timeScale = _gamePaused ? 0 : 1;
        }
    }

    public int Score
    {
        get => _score; set
        {
            _score = value;
            _scoreTxt.text = string.Format("{0:D5}", _score);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GamePaused = false;
        _pauseMenu.SetActive(false);
        _gameOverScreen.SetActive(false);
        StartCoroutine(DelaySpawn());
    }

    // Update is called once per frame
    void Update()
    {
        if (!_player.IsAlive)
        {
            StopAllCoroutines();
            _gameOverScreen.SetActive(true);
            return;
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            GamePaused = !GamePaused;
        }
        if (!_pauseSpawn) StartCoroutine(DelaySpawn());
    }

    private IEnumerator DelaySpawn()
    {
        _pauseSpawn = true;
        yield return new WaitForSeconds(Random.Range(_spawnDelay, _spawnDelay+5));
        Obstacle ob = Instantiate(_obstacles[Random.Range(0,_obstacles.Length)], _spawer.transform.position, Quaternion.identity);
        ob.Speed = _objectSpeed;
        _pauseSpawn = false;
    }

    public void AddScore(int score)
    {
        Score += score;
    }
    public void TogglePause()
    {
        GamePaused = !GamePaused;
    }
}
