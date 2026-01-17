using UnityEngine;

public class HammerLogic : MonoBehaviour
{
    [SerializeField] GameObject _gameObject;
	[SerializeField] Transform _enemy;

	private void OnCollisionEnter(Collision other)
	{
        _gameObject.GetComponent<EnemyTypeThreeController>().ResetSwing();
	}

	private void UpdateHammer()
	{
		Vector3 newPos = _enemy.position;
		newPos.y += 0.8f;
		this.transform.position = newPos;
		this.GetComponent<Rigidbody>().linearVelocity = new Vector3(0, 0, 0);
		this.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
	}

	void Update()
	{
		UpdateHammer();
	}
}
