using UnityEngine;

public class ParticleOrbit : MonoBehaviour
{
	private ParticleSystem system;
	private ParticleSystem.Particle[] particles;

	private void Start()
	{
		system = GetComponent<ParticleSystem>();
		particles = new ParticleSystem.Particle[system.main.maxParticles];
	}

	private void FixedUpdate()
	{
		system.GetParticles(particles);

		for (int i = 0; i < particles.Length; i++)
			particles[i].velocity += (particles[i].position - GameManager.Instance.planet.position).normalized * Physics.gravity.y * Time.fixedDeltaTime;

		system.SetParticles(particles, particles.Length);
	}
}
