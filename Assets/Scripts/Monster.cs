using UnityEngine;

public class Monster : Orbit
{
	protected override void FixedUpdate()
	{
		base.FixedUpdate();

		if (GameManager.Instance.localPlayer)
		{
			Vector3 direction = GameManager.Instance.localPlayer.transform.position - transform.position;
			direction -= Vector3.Dot(direction, transform.up) * transform.up;

			transform.rotation = Quaternion.LookRotation(direction, transform.up);
		}

		movement = transform.forward;
	}
}
