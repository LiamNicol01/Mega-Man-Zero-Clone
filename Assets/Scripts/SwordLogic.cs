using UnityEngine;

public class SwordLogic : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private Vector3 _axis;
    [SerializeField] private float _rotSpd;
    [SerializeField] private BoxCollider _swordBlade;

    private void UpdateSword()
    {
        this.transform.position = _player.transform.position;
        if (Input.GetKeyDown(KeyCode.F))
        {
            this.transform.rotation = Quaternion.identity;
			Renderer[] renderers = this.GetComponentsInChildren<Renderer>();
			foreach (Renderer renderer in renderers) renderer.enabled = true;
            _swordBlade.enabled = true;
		}
        if (Input.GetKey(KeyCode.F))
        {
            float dir = 1.0f;
            if (_player.GetComponent<PlayerController>().right) dir = -1.0f;
			this.gameObject.transform.Rotate(_axis, dir * _rotSpd * Time.deltaTime);
		}
        if (Input.GetKeyUp(KeyCode.F))
        {
            _swordBlade.enabled = false;
            Renderer[] renderers = this.GetComponentsInChildren<Renderer>();
            foreach (Renderer renderer in renderers) renderer.enabled = false;
        }
    }

	private void OnCollisionEnter(Collision collision)
	{
        //Debug.Log("Sword Collision");
	}

	void Update()
    {
        UpdateSword();
    }
}
