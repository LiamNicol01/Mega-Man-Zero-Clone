using UnityEngine;

public class BossManager : MonoBehaviour
{
    [SerializeField] private GameObject _wall;

	private void OnCollisionEnter(Collision other)
	{
        if (other.gameObject.GetComponent<isPlayer>())
        {
            this.GetComponent<BoxCollider>().enabled = false;
            UIManager.Instance.StartBoss();
            Invoke("CloseBossRoom", 3);
        }
	}

    private void CloseBossRoom()
    {
        _wall.transform.localScale = new Vector3(1, 13, 1);
    }

	
	void Update()
    {
        
    }
}
