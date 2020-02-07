using UnityEngine;

using DG.Tweening;

public class Bullet : Orbit, IPoolable
{
	public float lifeTime;

	private Tween addToPool;

	protected override void FixedUpdate()
	{
		base.FixedUpdate();

		transform.position += transform.forward * speed * Time.fixedDeltaTime;
	}

	public void OnPushedToPool() {}

	public void OnPulledFromPool()
	{
		addToPool.Kill();
		addToPool = DOVirtual.DelayedCall(lifeTime, () => GameManager.Instance.bulletPool.Push(this), false);
	}
}
