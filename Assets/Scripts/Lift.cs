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
            float distance = Vector2.Distance(transform.localPosition, _endPosition);
            LeanTween.move(gameObject, _endPosition, Utility.SpeedToTime(distance,
                                                                         Constants.LiftSpeed));
            _atStart = false;
        }
        else 
        {
            float distance = Vector2.Distance(transform.localPosition, _startPosition);
            LeanTween.move(gameObject, _startPosition, Utility.SpeedToTime(distance,
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
