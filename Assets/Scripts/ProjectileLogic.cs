using UnityEngine;

public class ProjectileLogic : MonoBehaviour
{
    // Positive or Negative 1 that determines direction of projectile
    [SerializeField] private int _dir;
    [SerializeField] private float _spd;

    void Start()
    {
        Invoke("SelfDestruct", 1);
    }

    private void SelfDestruct()
    {
        Destroy(gameObject);
    }

	private void OnCollisionEnter(Collision other)
	{
		SelfDestruct();
	}

    private void ProjectileUpdate()
    {
        Vector3 newPos = this.gameObject.transform.position;
		newPos.x += _dir * _spd * Time.deltaTime;
		this.gameObject.transform.position = newPos;
	}

	void Update()
    {
        ProjectileUpdate();
    }
}
