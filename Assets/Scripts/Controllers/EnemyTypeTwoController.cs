using UnityEngine;

public class EnemyTypeTwoController : MonoBehaviour
{
	[SerializeField] private Transform _playerPos;
	[SerializeField] private float _detectRange;
    [SerializeField] private GameObject _projectileSpawner;
    [SerializeField] private GameObject _projectile;

    float projectileTimer = 0.0f;

    private void EnemyLogicUpdate()
    {
        projectileTimer += Time.deltaTime;
        if (projectileTimer > 3.0f)
        {
            Instantiate(_projectile, _projectileSpawner.transform.position, this.transform.rotation);
            projectileTimer = 0.0f;
        }
	}

    void Update()
    {
        EnemyLogicUpdate();
    }
}
