using UnityEngine;

public class BlasterLogic : MonoBehaviour
{
	[SerializeField] private GameObject _player;
	[SerializeField] private GameObject _bolt;
	[SerializeField] private GameObject _strongBolt;

	float blasterTimer = 0.5f;
    float blasterCharge = 0.0f;

    private void UpdateBlaster()
    {
		// Set blaster position to player
		Vector3 pos = _player.transform.position;
		if (_player.GetComponent<PlayerController>().right) pos.x += 1.0f;
		if (!_player.GetComponent<PlayerController>().right) pos.x -= 1.0f;
		pos.y += 1.0f;
		this.transform.position = pos;

		// Timer for when you can use the blaster
		if (blasterTimer <= 0.5f) blasterTimer += Time.deltaTime;

		// 
		if (Input.GetKey(KeyCode.E))
		{
			blasterCharge += Time.deltaTime;
			if (blasterTimer >= 0.5f)
			{
				GameObject bolt = Instantiate(_bolt, this.transform);
				bolt.transform.parent = null;
				if (_player.GetComponent<PlayerController>().right)
					bolt.GetComponent<Rigidbody>().linearVelocity = new Vector3(20, 0, 0);
				if (!_player.GetComponent<PlayerController>().right)
					bolt.GetComponent<Rigidbody>().linearVelocity = new Vector3(-20, 0, 0);
				blasterTimer = 0.0f;
			}
		}

		// Charged bolt
		if (Input.GetKeyUp(KeyCode.E) && blasterCharge >= 2.0f)
		{
			GameObject strongBolt = Instantiate(_strongBolt, this.transform);
			if (_player.GetComponent<PlayerController>().right)
				strongBolt.GetComponent<Rigidbody>().linearVelocity = new Vector3(20, 0, 0);
			if (!_player.GetComponent<PlayerController>().right)
				strongBolt.GetComponent<Rigidbody>().linearVelocity = new Vector3(-20, 0, 0);
			blasterCharge = 0.0f;
		}
	}

	// Update is called once per frame
	void Update()
    {
        UpdateBlaster();
    }
}
