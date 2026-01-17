using UnityEngine;

public class MovingPlatformLogic : MonoBehaviour
{
    [SerializeField] Transform _dest1;
    [SerializeField] Transform _dest2;
    [SerializeField] Transform _dest3;
    [SerializeField] Transform _dest4;
    [SerializeField] float _spd;

    int currDest = 0;

	void Start()
    {
        
    }

    private void MoveToDest()
    {
		Transform[] destinations = { _dest1, _dest2, _dest3, _dest4 };
		float dist = Vector3.Distance(this.transform.position, destinations[currDest].position);
        if (dist < 0.01) {
            this.transform.position = destinations[currDest].position;
            currDest++;
            if (currDest >= 4) currDest = 0;
        }
        else
        {
            Vector3 dir = Vector3.Normalize(destinations[currDest].position - this.transform.position);
            this.transform.position += _spd * (dir) * Time.deltaTime;
        }
    }

    void Update()
    {
        MoveToDest();
    }
}
