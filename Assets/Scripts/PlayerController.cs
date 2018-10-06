using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public GameButton _linkedButton = null;

    private Vector2 _initialPosition;

    private void Awake()
    {
        _initialPosition = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Button")
        {
            _linkedButton = collision.GetComponent<GameButton>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _linkedButton = null;
    }

    public void ActionKeyPressedHandler ()
    {
        if (_linkedButton != null)
        {
            _linkedButton.PerformAction();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftCommand) ||
            Input.GetKeyDown(KeyCode.RightCommand) ||
            Input.GetKeyDown(KeyCode.LeftControl) || 
            Input.GetKeyDown(KeyCode.RightControl))
        {
            ActionKeyPressedHandler();
        }
    }

    public void Reset()
    {
        transform.position = _initialPosition;
    }
}
