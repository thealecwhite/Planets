using UnityEngine;

using Rewired;

public class Ship : Orbit
{
	public float fireRate;
	public ParticleSystem boost;

	private Animator animator;
	private Player input; // Rewired input
	private Vector2 moveInput;
	private float lookInput, fireTime;
	private bool altFire;

	private void Start()
	{
		animator = GetComponent<Animator>();
		input = ReInput.players.GetPlayer(0);
	}

	protected override void FixedUpdate()
	{
		base.FixedUpdate();

		moveInput = input.GetAxis2D("Strafe", "Fly");
		lookInput = input.GetAxis("Look");

		transform.rotation = Quaternion.AngleAxis(lookInput * 2.5f, transform.up) * transform.rotation;
		// transform.position += transform.TransformDirection(moveInput.x, 0f, moveInput.y).normalized * speed * Time.fixedDeltaTime;
		movement = transform.TransformDirection(moveInput.x, 0f, moveInput.y);

		if (input.GetButton("Fire") && (Time.time >= fireTime + (1f / fireRate)))
		{
			Bullet bullet = GameManager.Instance.bulletPool.Pull();
			bullet.transform.position = transform.position + (transform.forward * 0.5f) + (transform.right * (altFire ? -0.3f : 0.3f));
			bullet.transform.rotation = transform.rotation;
			bullet.height = height;

			fireTime = Time.time;
			altFire = !altFire;
		}
		
		if (moveInput != Vector2.zero)
		{
			if (!boost.isEmitting)
				boost.Play();
		}
		else boost.Stop();

		animator.SetFloat("Strafe", moveInput.x + lookInput, 0.1f, Time.fixedDeltaTime);
	}
}
