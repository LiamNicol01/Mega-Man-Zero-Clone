using UnityEngine;

public class EnemyTypeThreeController : MonoBehaviour
{
    [SerializeField] private int _hp;
    [SerializeField] private float _rotSpd;
    [SerializeField] private Vector3 _axis;
    [SerializeField] private GameObject _hammer;
    [SerializeField] private Transform _player;
    [SerializeField] private float _spd;
    [SerializeField] private float _detectRange;

	Quaternion startRot;
	float dist;
	public bool swing = false;
	public bool resetting = false;
    private float resetTimer = 0.0f;

	void Start()
	{
        startRot = _hammer.transform.rotation;
	}

    private void UpdateEnemy()
    {
        dist = Vector3.Distance(_player.transform.position, this.transform.position);
        // If not resetting, move enemy
        if (!resetting)
        {
			// Move towards the player
			if (dist < _detectRange) MoveTowards(_player.transform.position);
			// Face towards the player
			if (_player.position.x < this.transform.position.x) SetYRot(0);
			if (_player.position.x > this.transform.position.x) SetYRot(180);
		}
	}

    private void MoveTowards(Vector3 target)
    {
        Vector3 newPos = this.transform.position;
        this.transform.position += _spd * (target - this.transform.position) * Time.deltaTime;
    }

    private void SetYRot(float rot)
    {
		Quaternion newRot = this.transform.rotation;
		newRot.y = rot;
		this.transform.rotation = newRot;
	}

	private void UpdateHammer()
    {
        // If the player is within range, swing
        if (dist < 3 && !resetting) {
            swing = true;
        }
        // If swinging, apply swing logic
        if (swing) SwingHammer();
        // If resetting, apply resetting logic
        if (resetting) ResettingSwing();
        // If the hammer isn't being swung, isn't resetting,
	}

    private void SwingHammer()
    {
        _hammer.transform.Rotate(_axis, _rotSpd * Time.deltaTime);
    }
    
    public void ResetSwing()
    {
        resetting = true;
		swing = false;
	}

    public void ResettingSwing()
    {
        // Let the hammer recoil or be stuck for a short time
        resetTimer += Time.deltaTime;
        if (resetTimer > 3.0f)
        {
            resetTimer = 0.0f;
            SetYRot(0);
			_hammer.transform.rotation = startRot;
			resetting = false;
        }
	}

    void Update()
    {
        UpdateEnemy();
        UpdateHammer();
    }
}
