using Unity.VisualScripting;
using UnityEngine;

public class EnemyTypeOneController : MonoBehaviour
{
	[SerializeField] private GameObject _player;
	[SerializeField] private float _detectRange;
	[SerializeField] private float _spd;

	private Transform playerTransform;
	private Vector3 targetPos;

    void Start()
    {
        _player = FindFirstObjectByType<isPlayer>().gameObject;
		playerTransform = _player.transform;
	}

	public void OnCollisionEnter(Collision other)
	{
        // Move away enemy if enemy collides with player
		if (other.gameObject.GetComponent<isPlayer>())
            this.transform.position -= 100 *
                (other.transform.position - this.transform.position) *
                Time.deltaTime;
	}
	
    private void EnemyLogicUpdate()
    {
        // Update player position
		playerTransform = _player.transform;
		// Calculate distance with player
		float dist = Vector3.Distance(playerTransform.position, this.transform.position);
        // If the player is within the detection range, move towards the player
        if (dist <= _detectRange)
        {
			// Calculate the player's location
			targetPos = playerTransform.position;
			targetPos.y += 1;
			// Move towards the player's location
			this.transform.position = Vector3.MoveTowards(this.transform.position, targetPos, _spd * Time.deltaTime);
		}
    }

    void Update()
    {
        if (_player != null) EnemyLogicUpdate();
    }
}
