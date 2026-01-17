using UnityEngine;

public class BoltLogic : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("SelfDestruct", 1);
    }

    private void SelfDestruct()
    {
        Destroy(gameObject);
    }

	private void OnCollisionEnter(Collision other)
	{
        if (this.gameObject.GetComponent<isBolt>()) if (!other.gameObject.GetComponent<isPlayer>()) SelfDestruct();
	}

    private void OnCollisionStay(Collision other)
    {
        SelfDestruct();
    }

	// Update is called once per frame
	void Update()
    {
        
    }
}
