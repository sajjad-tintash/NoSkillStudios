using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour
{
    private Vector2 _initialPosition;

    protected virtual void Awake ()
    {
        _initialPosition = transform.position;
    }

    protected virtual void Start ()
    {
        //transform.position = _
    }

    public virtual void Move ()
    {
        LeanTween.cancel(gameObject);
    }

    protected virtual void Reset()
    {
        transform.position = _initialPosition;
    }
}
