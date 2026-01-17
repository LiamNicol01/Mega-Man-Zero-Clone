using UnityEngine;

public class GoblinController : MonoBehaviour
{
	[SerializeField] private Animator _anim;
    [SerializeField] private GameObject _player;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        
    }

    private void CalculateMovement()
    {
        if (this.GetComponent<isEnemy>()._hp <= 0
            && !_anim.GetBool("dead"))
        {
            _anim.SetBool("dead", true);
            _anim.SetTrigger("death");
        }

		float dist = Vector3.Distance(_player.transform.position, this.transform.position);
        _anim.SetFloat("dist", dist);

		if (_player.transform.position.x < this.transform.position.x) _anim.SetFloat("playerX", -1);
		if (_player.transform.position.x > this.transform.position.x) _anim.SetFloat("playerX", 1);
	}

	// Update is called once per frame
	void Update()
    {
        CalculateMovement();
	}
}
