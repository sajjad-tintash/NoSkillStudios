using UnityEngine;
using System.Collections;

public class Lift : Obstacle
{
    public Vector2 _startPosition;
    public Vector2 _endPosition;

    private bool _atStart = true;

    protected override void Start()
    {
        base.Start();
        transform.position = _startPosition;
    }

    public override void Move()
    {
        base.Move();

        if (_atStart)
        {
            LeanTween.move(gameObject, _endPosition, Utility.SpeedToTime(Vector2.Distance(transform.position, _endPosition),
                                                                         Constants.LiftSpeed));
            _atStart = false;
        }
        else 
        {
            LeanTween.move(gameObject, _startPosition, Utility.SpeedToTime(Vector2.Distance(transform.position, _startPosition),
                                                                         Constants.LiftSpeed));
            _atStart = true;
        }
    }

    protected override void Reset()
    {
        base.Reset();
        _atStart = false;
    }
}
