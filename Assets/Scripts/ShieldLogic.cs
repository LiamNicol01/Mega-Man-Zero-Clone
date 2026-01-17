using UnityEngine;

public class ShieldLogic : MonoBehaviour
{
    [SerializeField] private GameObject _player;

    float shieldCharge = 0.0f;

    void Start()
    {
        
    }

    private void ShieldUpdate()
    {
		Vector3 pos = _player.transform.position;
        if (_player.GetComponent<PlayerController>().right) pos.x += 0.6f;
        if (!_player.GetComponent<PlayerController>().right) pos.x -= 0.6f;
        this.transform.position = pos;

        if (Input.GetKey(KeyCode.Q))
        {
			// Show shield in direction player is looking
			this.gameObject.GetComponent<MeshRenderer>().enabled = true;
			this.gameObject.GetComponent<BoxCollider>().enabled = true;

            // Charge shield timer
            shieldCharge += Time.deltaTime;

			// Change isShielded to true
			_player.GetComponent<isShielded>().enabled = true;
		}

		// If shield is charged
		if (shieldCharge > 2.0f)
		{
            this.gameObject.GetComponent<Renderer>().material.color = Color.blue;
			if (Input.GetKeyUp(KeyCode.Q))
			{
                ThrowShield();
			}
		}

        if (Input.GetKeyUp(KeyCode.Q))
		{
			this.gameObject.GetComponent<MeshRenderer>().enabled = false;
			this.gameObject.GetComponent<BoxCollider>().enabled = false;
			this.gameObject.GetComponent<Renderer>().material.color = Color.grey;
			_player.GetComponent<isShielded>().enabled = false;
			shieldCharge = 0.0f;
        }
	}

    private void ThrowShield()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ShieldUpdate();
    }
}
