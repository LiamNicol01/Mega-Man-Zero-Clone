using UnityEngine;

public class EnemyMagicLogic : MonoBehaviour
{
	private void AoELogic()
	{
		Vector3 newScale = transform.localScale;
		newScale.x += 3 * Time.deltaTime;
		newScale.y += 3 * Time.deltaTime;
		newScale.z += 3 * Time.deltaTime;
		transform.localScale = newScale;
		if (newScale.x > 4.0f) Destroy(gameObject);
	}

	void Update()
	{
		AoELogic();
	}
}
