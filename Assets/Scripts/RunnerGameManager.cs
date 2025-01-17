﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RunnerGameManager : MonoBehaviour
{
    [SerializeField] private InputManager _im = null;
    [SerializeField] private SceneChanger _changer = null;

    [SerializeField] private GameObject _spawer = null;
    [SerializeField] private float _spawnDelay = 1f;
    [SerializeField] private float _objectSpeed = 1f;

    [SerializeField] private Player _player = null;

    [SerializeField] private GameObject _gameOverScreen = null;
    [SerializeField] private GameObject _HUD = null;
    [SerializeField] private GameObject _pauseMenu = null;
    [SerializeField] private TextMeshProUGUI _scoreTxt = null;

    [SerializeField] private EnemyPattern[] _patterns = null;

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
        get => Score1; set
        {
            Score1 = value;
            _scoreTxt.text = string.Format("{0:D5}", Score1);
        }
    }

    public int Score1 { get => _score; set => _score = value; }
    public Player Player { get => _player; set => _player = value; }

    // Start is called before the first frame update
    void Start()
    {
        GamePaused = false;
        _pauseMenu.SetActive(false);
        _gameOverScreen.SetActive(false);
        _HUD.SetActive(true);
        StartCoroutine(DelaySpawn());
    }

    // Update is called once per frame
    void Update()
    {
        if (!Player.IsAlive)
        {
            StopAllCoroutines();
            _gameOverScreen.SetActive(true);
            _HUD.SetActive(false);
            return;
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            GamePaused = !GamePaused;
        }
        if (!_pauseSpawn) StartCoroutine(DelaySpawn());
        if(_gameOverScreen.activeInHierarchy)
        {

            if(_im.InputedNote != "")
            {
                _changer.ChangeScene();
            }
        }
    }

    private IEnumerator DelaySpawn()
    {
        _pauseSpawn = true;
        yield return new WaitForSeconds(_spawnDelay);
        EnemyPattern pattern = _patterns[Random.Range(0, _patterns.Length)];
        for (int i = 0; i < pattern.Pattern.Length; i++)
        {
            Obstacle ob = Instantiate(pattern.Pattern[i], _spawer.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(_spawnDelay);
        }
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
