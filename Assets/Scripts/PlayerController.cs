using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public GameButton _linkedButton = null;
    public GameController.World _world;

    public GameObject[] _progressBars;

    public Sprite[] _centreSprites;
    public Sprite[] _rightSprites;
    public Sprite[] _leftSprites;

    public int _mutationLevel = 0;

    private SpriteRenderer _spriteRenderer;
    private Vector2 _initialPosition;

    private PlayerPlatformerController.Direction _currentDirection = PlayerPlatformerController.Direction.Centre;

    private void Awake()
    {
        _initialPosition = transform.position;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        ChangeSprite();

        UpdateProgressBar();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Button")
        {
            _linkedButton = collision.GetComponent<GameButton>();
        }
        else if (collision.gameObject.tag == "Candy")
        {
            Candy candy = collision.GetComponent<Candy>();
            if (candy._increaseMutation)
            {
                MutateUp();
            }
            else 
            {
                MutateDown();
            }

            candy.Disable();

            GameController.instance.CheckWinCondition();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _linkedButton = null;
    }

    public void ActionKeyPressedHandler ()
    {
        if (_linkedButton != null && GameController.instance._currentWorld == _world)
        {
            _linkedButton.PerformAction();
        }
    }

    private void Update()
    {
        if (GameController.CONTROLS_ENABLED)
        {
            if (Input.GetKeyDown(KeyCode.LeftCommand) ||
                Input.GetKeyDown(KeyCode.RightCommand) ||
                Input.GetKeyDown(KeyCode.LeftControl) ||
                Input.GetKeyDown(KeyCode.RightControl))
            {
                ActionKeyPressedHandler();
            }
        }
    }

    public void Reset()
    {
        transform.position = _initialPosition;
        _mutationLevel = 0;
        _currentDirection = PlayerPlatformerController.Direction.Centre;
        ChangeSprite();
        UpdateProgressBar();
    }

    public void MutateUp ()
    {
        _mutationLevel++;

        if (_mutationLevel < 0)
            _mutationLevel = 0;
        else if (_mutationLevel > 6)
            _mutationLevel = 6;

        UpdateProgressBar();
    }

    public void MutateDown () 
    {
        _mutationLevel--;

        if (_mutationLevel < 0)
            _mutationLevel = 0;
        else if (_mutationLevel > 6)
            _mutationLevel = 6;

        UpdateProgressBar();
    }

    public void UpdateProgressBar ()
    {
        for (int i = 0; i < _progressBars.Length; i++)
        {
            if (i <= _mutationLevel)
                _progressBars[i].gameObject.SetActive(true);
            else
                _progressBars[i].gameObject.SetActive(false);
        }
    }

    public void ChangeDirection (PlayerPlatformerController.Direction direction)
    {
        _currentDirection = direction;
        ChangeSprite();
    }

    public void ChangeSprite ()
    {
        switch(_currentDirection)
        {
            case PlayerPlatformerController.Direction.Centre:
                _spriteRenderer.sprite = _centreSprites[_mutationLevel];
                break;

            case PlayerPlatformerController.Direction.Left:
                _spriteRenderer.sprite = _leftSprites[_mutationLevel];
                break;

            case PlayerPlatformerController.Direction.Right:
                _spriteRenderer.sprite = _rightSprites[_mutationLevel];
                break;
        }
    }
}
