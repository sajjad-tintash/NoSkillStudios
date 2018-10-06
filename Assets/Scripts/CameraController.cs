using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	public Transform Target;

	public Vector2 
		Margin,
		Smoothing;

	public BoxCollider2D Bounds;

	public bool FollowingX { get; set;}
	public bool FollowingY { get; set;}

	private Vector3 
	_min,
	_max;
	
	private Camera _mainCamera;

	//public static CameraController instance = null;

	void Awake () {
        //if (instance == null)
        //{
        //	instance = this;
        //}
        //else Destroy (this.gameObject);

        //this.enabled = false;

        FollowingX = true;
        FollowingY = true;
	}

	public void Init ()
	{
		_mainCamera = Camera.main;
		
		_min = Bounds.bounds.min;
		_max = Bounds.bounds.max;

		this.enabled = true;
	}

	void Update ()
	{
		if (FollowingX || FollowingY) 
		{	
			float x = transform.position.x;
			float y = transform.position.y;
			float cameraWidth = _mainCamera.orthographicSize * ((float)Screen.width / Screen.height);

			if (FollowingX) {
				if (Mathf.Abs (x - Target.position.x) > Margin.x) {
					x = Mathf.Lerp (x, Target.position.x, Smoothing.x * Time.deltaTime);
				}

				x = Mathf.Clamp (x, _min.x + cameraWidth, _max.x - cameraWidth);
			}

			if (FollowingY) {
				if (Mathf.Abs (y - Target.position.y) > Margin.y) {
					y = Mathf.Lerp (y, Target.position.y, Smoothing.y * Time.deltaTime);
				}

				y = Mathf.Clamp (y, _min.y + _mainCamera.orthographicSize, _max.y - _mainCamera.orthographicSize);
			}

			transform.position = new Vector3 (x, y, transform.position.z);
		}
	}

	public void ResetCamera ()
	{
		FollowingX = false;
		FollowingY = false;

		transform.position = new Vector3 (0f, 0f, transform.position.z);
	}
}
