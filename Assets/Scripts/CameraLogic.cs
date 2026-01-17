using UnityEngine;

public class CameraLogic : MonoBehaviour
{
	[SerializeField] private Transform _player;
	[SerializeField] private Transform _bossRoom;

	Transform dest;

	void Start()
    {
		dest = _player;
    }

	public void TargetBossRoom()
	{
		dest = _bossRoom;
	}

	public void CameraUpdate()
	{
		Vector3 offset = new Vector3(0, 1, -10);
		this.transform.position = dest.position + offset;
	}

	// Update is called once per frame
	void Update()
    {
        CameraUpdate();
    }
}
