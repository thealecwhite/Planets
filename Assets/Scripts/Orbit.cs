using UnityEngine;

public class Orbit : MonoBehaviour
{
	public float height, speed;

	protected Vector3 movement
	{
		get
		{
			return _movement;
		}

		set
		{
			_movement = Vector3.ClampMagnitude(value, 1f);
		}
	}

	protected Vector3 gravity => transform.position - GameManager.Instance.planet.position;

	private Vector3 _movement;

	protected virtual void FixedUpdate()
	{
		Move();

		transform.rotation = Quaternion.FromToRotation(transform.up, gravity) * transform.rotation;
		transform.position += GetHeight();
	}

	private void Move()
	{
		transform.position += _movement * speed * Time.fixedDeltaTime;
	}

	private Vector3 GetHeight()
	{
		return ((GameManager.Instance.planet.localScale.x / 2f + height) - gravity.magnitude) * gravity.normalized;
	}
}
