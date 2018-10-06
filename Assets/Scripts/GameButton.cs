using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameButton : MonoBehaviour {

    public GameController.World _interactionWorld;
    public bool _on = false;

    protected virtual void Awake ()
    {
        
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
            TurnOff();
        }
    }

    public virtual void TurnOn ()
    {
        _on = false;
    }

    public virtual void TurnOff()
    {
        _on = true;
    }

    protected virtual void Reset ()
    {
        _on = false;
    }
}
