using UnityEngine;

public class PitLogic : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

	private void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.GetComponent<isPlayer>())
        {
            // Damage Player
            //RespawnPlayer(other.gameObject);
            other.gameObject.GetComponent<PlayerController>().TakeDamage(10);
        }
	}

    private void RespawnPlayer(GameObject Player)
    {
        Player.transform.position = this.gameObject.GetComponentInChildren<isRespawnPoint>().transform.position;
    }

	// Update is called once per frame
	void Update()
    {
        
    }
}
