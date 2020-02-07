using UnityEngine;

using Cinemachine;

public class GameManager : Singleton<GameManager>
{
	public Camera mainCamera { get; private set; }
	public Ship localPlayer { get; private set; }

	public Transform planet;
	public Ship shipPrefab;
	public Bullet bulletPrefab;
	public Monster monsterPrefab;
	public Pool<Bullet> bulletPool;

	private Material space;

	private void Start()
	{
		localPlayer = Instantiate(shipPrefab, Random.onUnitSphere, Quaternion.identity);

		mainCamera = Camera.main;
		mainCamera.GetComponent<CinemachineBrain>().m_WorldUpOverride = localPlayer.transform;

		bulletPool = new Pool<Bullet>(bulletPrefab);

		RenderSettings.skybox = (space = new Material(RenderSettings.skybox));
		space.name += " (Clone)";
	}

	private void FixedUpdate()
	{
		space.SetFloat("_Rotation", Time.fixedTime);

		if (Random.Range(1, 1000) == 1)
			Instantiate(monsterPrefab, Random.onUnitSphere, Quaternion.identity);
	}
}
