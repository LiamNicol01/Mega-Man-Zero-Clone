using UnityEngine;

public class isEnemy : MonoBehaviour
{
	[SerializeField] public int _hp;
	[SerializeField] private float _deathTimer;

	[SerializeField] private AudioSource _damageSound;

	private void OnCollisionEnter(Collision other)
	{
		// Take damage if enemy collides with weapon
		// If colliding with the player's ranged attack, take 1 damage
		if (other.gameObject.GetComponent<isBolt>()) TakeDamage();
		// If colliding with the player's charged ranged attack, take 2 damage
		if (other.gameObject.GetComponent<isStrongBolt>()) TakeDamage(2);
		// If the object is a projectile and is colliding with the shield, take 1 damage
		if (this.gameObject.GetComponent<isProjectile>() && other.gameObject.GetComponent<isShield>()) TakeDamage();
	}

	private void OnTriggerEnter(Collider other)
	{
		// Take damage if enemy collides with weapon
		// If colliding with the player's sword, take 1 damage
		if (other.gameObject.GetComponent<isSword>()) TakeDamage();
		// If colliding with magic aoe, take 5 damage
		if (other.gameObject.GetComponent<isMagicAoE>()) TakeDamage(5);
	}

	public void TakeDamage(int damage = 1)
	{
		_hp -= damage;
		if (_damageSound != null) _damageSound.Play();
	}

	private void CommonEnemyLogicUpdate()
    {
		// If HP is less than or equal to 0, destroy self
		if (_hp <= 0)
		{
			Invoke("SelfDestruct", _deathTimer);
		}
	}

	private void SelfDestruct()
	{
		Destroy(this.gameObject);
	}

    // Update is called once per frame
    void Update()
    {
        CommonEnemyLogicUpdate();
    }
}
