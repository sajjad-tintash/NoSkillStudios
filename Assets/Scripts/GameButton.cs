using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameButton : MonoBehaviour {

    public GameController.World _interactionWorld;
    public bool _on = false;

    public Sprite _onSprite;
    public Sprite _offSprite;

    private SpriteRenderer _spriteRenderer;

    protected virtual void Awake ()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

	// Use this for initialization
	protected virtual void Start () {
		
	}
	
	public virtual void PerformAction ()
    {
        if (_on)
        {
            TurnOff(); 
        }
        else 
        {
            TurnOn();
        }
    }

    public virtual void TurnOn ()
    {
        _on = true;
        _spriteRenderer.sprite = _onSprite;
    }

    public virtual void TurnOff()
    {
        _on = false;
        _spriteRenderer.sprite = _offSprite;
    }

    protected virtual void Reset ()
    {
        _on = false;
        _spriteRenderer.sprite = _offSprite;
    }
}
