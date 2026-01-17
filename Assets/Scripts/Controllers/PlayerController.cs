using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Vector3 _axis;
    [SerializeField] private float _axisMag;
    [SerializeField] private Animator _anim;
    [SerializeField] private float _spd;
    [SerializeField] private float _jumpTimeMax;
    [SerializeField] private AudioSource _damageSound;
    [SerializeField] private AudioSource _blockSound;

    [SerializeField] public int _hp;

    public bool right = true;

    float jumpTime = 0.0f;
    bool invulnerable = false;
    float invulnerableTimer = 0.0f;
    float idleTimer = 0.0f;

    void Start()
    {
        if (_anim != null)
        {

        }

        Vector3 startPos = transform.position;
        startPos.x = -7.0f;
        this.transform.position = startPos;
    }

    private void PlayerInputLogicUpdate()
    {
        if (_hp <= 0)
        {
            _anim.SetTrigger("death");
            _anim.SetBool("dead", true);
            this.enabled = false;
        }

		Vector3 newPos = this.gameObject.transform.position;
		Quaternion newRot = this.gameObject.transform.rotation;

        // Idle
        idleTimer += Time.deltaTime;
        if (idleTimer > 3.0f)
		{
            float idleNum = 0;
            int idle = new System.Random().Next(3);
            for (int i = 0; i < idle; i++)
            {
                idleNum += 0.333f;
            }
			_anim.SetFloat("idleNum", idleNum);
            idleTimer = 0.0f;
		}

		// Horizontal Movement Inputs
		_axis.x = Input.GetAxis("Horizontal");
        _axisMag = _axis.magnitude;

		_anim.SetFloat("h", _axis.x);
		_anim.SetFloat("v", _axis.y);
        _anim.SetFloat("axisMag", _axisMag);
        
		if (_axis.x > 0.01 && _anim.GetBool("moveable"))
        {
            right = true;
            newRot.y = 180;
            newPos.x += _spd * Time.deltaTime;
        }
        if (_axis.x < -0.01 && _anim.GetBool("moveable"))
        {
            right = false;
            newRot.y = 0;
            newPos.x -= _spd * Time.deltaTime;
        }

        // Jump Inputs
        if ((Input.GetKeyDown(KeyCode.Space) ||
            Input.GetKeyDown(KeyCode.UpArrow) ||
            Input.GetKeyDown(KeyCode.W)) &&
            !_anim.GetBool("descending"))
        {
			//_anim.SetTrigger("jump");
			JumpingAscend();
        }
        if (Input.GetKeyUp(KeyCode.Space) ||
            Input.GetKeyUp(KeyCode.UpArrow) ||
            Input.GetKeyUp(KeyCode.W) ||
            jumpTime >= _jumpTimeMax)
		{
			JumpingDescend();
		}
        if (_anim.GetBool("ascending"))
        {
            jumpTime += Time.deltaTime;
            newPos.y += _spd * 2 * Time.deltaTime;
        }

		// Sword Inputs
		if (Input.GetKeyDown(KeyCode.F))
		{
            _anim.SetTrigger("slash");
		}
		if (Input.GetKeyDown(KeyCode.G))
		{
            _anim.SetTrigger("jumpSlash");
		}
		if (Input.GetKeyDown(KeyCode.C))
		{
			_anim.SetTrigger("spinAttack");
		}

		// Blaster Inputs
		if (Input.GetKeyDown(KeyCode.E))
        {
            _anim.SetBool("castingShield", true);
        }
        if (Input.GetKeyUp(KeyCode.E))
		{
			_anim.SetBool("castingShield", false);
		}

		// Shield Inputs
		if (Input.GetKeyDown(KeyCode.Q))
		{
			_anim.SetBool("blocking", true);
		}
		if (Input.GetKeyUp(KeyCode.Q))
		{
			_anim.SetBool("blocking", false);
		}

		// Cast Inputs
		if (Input.GetKey(KeyCode.R))
		{
			_anim.SetBool("castSword", true);
		}
		if (!Input.GetKey(KeyCode.R))
		{
			_anim.SetBool("castSword", false);
		}

		this.gameObject.transform.position = newPos; 
        this.gameObject.transform.rotation = newRot;
	}

    private void JumpingAscend()
    {
		_anim.SetBool("ascending", true);
	}

    private void JumpingDescend()
    {
		_anim.SetBool("ascending", false);
		_anim.SetBool("descending", true);
	}

	public void OnCollisionStay(Collision other)
	{
		if (other.gameObject.GetComponent<isTerrain>())
		{
			_anim.SetBool("descending", false);
			jumpTime = 0.0f;
		}
	}

	public void OnCollisionEnter(Collision other)
	{
        if (other.gameObject.GetComponentInChildren<isHammer>()) TakeDamage(2);
        if (other.gameObject.GetComponent<isEnemy>())
        {
			if (!other.gameObject.GetComponent<isProjectile>()) TakeDamage();
            else if (!this.gameObject.GetComponent<isShielded>()) TakeDamage();
        }
	}

	public void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.GetComponent<isEnemyMagic>()) TakeDamage(2);
	}

	public void TakeDamage(int damage = 1)
    {
        if (!invulnerable && !_anim.GetBool("blocking"))
        {
            _anim.SetTrigger("tookDamage");
			_hp -= damage;
            if (_damageSound != null) _damageSound.Play();
			UIManager.Instance.UpdateHp(_hp);
            invulnerable = true;
            this.GetComponent<Renderer>().material.color = Color.white;
		}

        if (_anim.GetBool("blocking"))
        {
            // Play block sound
            if (_blockSound != null) _blockSound.Play();
            
            _anim.SetTrigger("blockRecoil");
        }
    }

    private void InvulnerabilityUpdate()
    {
        invulnerableTimer += Time.deltaTime;
        if (invulnerableTimer >= 1.5f)
        {
            invulnerable = false;
            this.GetComponent<Renderer>().material.color = Color.gray;
            invulnerableTimer = 0.0f;
        }
    }

	void Update()
    {
        PlayerInputLogicUpdate();
        InvulnerabilityUpdate();
    }
}
