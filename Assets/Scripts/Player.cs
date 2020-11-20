using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PitchDetector;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    enum Action
    {
        Jump,
        Fire,
        Ice,
        Wind
    }

    [SerializeField] private Transform _attackCheck = null;
    [SerializeField] private InputManager _im = null;
    [SerializeField] private int _hp = 2;
    [SerializeField] private Image[] _hpImages = null;

    [SerializeField] private GameObject _scorePopUp = null;

    [SerializeField] private bool _isAlive = true;

    [SerializeField] private RunnerGameManager _gm = null;

    [SerializeField] private Animator _attackAnimator = null;

    private bool _delayed = false;

    public bool IsAlive { get => _isAlive; set => _isAlive = value; }

    private bool _falling = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (_delayed || !IsAlive) return;
        if (_falling)
        {
            StartCoroutine(Jump(-5));
            _falling = false;
            GetComponent<BoxCollider2D>().enabled = true;
            GetComponent<Animator>().SetBool("Jumping", !GetComponent<Animator>().GetBool("Jumping"));
        }
        switch(_im.InputedNote)
        {
            case "G":
                DoAction(Action.Jump);
                break;
            case "C":
                DoAction(Action.Ice);
                break;
            case "E":
                DoAction(Action.Fire);
                break;
            case "A":
                DoAction(Action.Wind);
                break;
            default:
                break;
        }
           
    }

    void DoAction(Action action)
    {
        StartCoroutine(DelayInput());
        Collider2D obstacle = Physics2D.OverlapCircle(_attackCheck.position, 1f);
        switch (action)
        {
            case Action.Jump:
                print("Jump");
                _falling = true;
                GetComponent<BoxCollider2D>().enabled = false;
                GetComponent<Animator>().SetBool("Jumping", !GetComponent<Animator>().GetBool("Jumping"));
                StartCoroutine(Jump(5));
                break;
            case Action.Fire:
                GetComponent<Animator>().SetTrigger("Attack");
                _attackAnimator.SetTrigger("Fire");
                if (obstacle != null)
                {
                    _gm.AddScore(100);
                    Instantiate(_scorePopUp, obstacle.transform.position, Quaternion.identity);
                    Destroy(obstacle.gameObject);
                }
                break;
            case Action.Ice:
                GetComponent<Animator>().SetTrigger("Attack");
                _attackAnimator.SetTrigger("Ice");
                if (obstacle != null)
                {
                    _gm.AddScore(100);
                    Instantiate(_scorePopUp, obstacle.transform.position, Quaternion.identity);
                    Destroy(obstacle.gameObject);
                }
                break;
            case Action.Wind:
                GetComponent<Animator>().SetTrigger("Attack");
                _attackAnimator.SetTrigger("Lightning");

                if (obstacle != null)
                {
                    _gm.AddScore(100);
                    Instantiate(_scorePopUp, obstacle.transform.position, Quaternion.identity);
                    Destroy(obstacle.gameObject);
                }
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            if(_hp > 0)
            {
                _hp--;
                GetComponent<Animator>().SetTrigger("Hurt");
                _hpImages[_hp + 1].GetComponent<Animator>().SetBool("Empty", true);
            }
            else
            {
                IsAlive = false;
                GetComponent<Animator>().SetTrigger("Death");
                _hpImages[0].GetComponent<Animator>().SetBool("Empty", true);
            }
        }
    }

    private IEnumerator Jump(int power)
    {
        float timer = .4f;
        while(timer > 0)
        {
            timer -= 1 * Time.deltaTime;
            transform.position += new Vector3(0, 1, 0) * power * Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator DelayInput()
    {
        _delayed = true;
        _im.GetComponent<MicrophonePitchDetector>().ToggleRecord();
        _im.InputedNote = "";
        yield return new WaitForSeconds(0.5f);
        _im.GetComponent<MicrophonePitchDetector>().ToggleRecord();
        _delayed = false;
    }
}
