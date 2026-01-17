using UnityEngine;

public class FollowLogic : MonoBehaviour
{
    [SerializeField] Transform Target;

    void Start()
    {
        
    }

    private void FollowTarget()
    {
        if (Target != null)
        {
            gameObject.transform.position = Target.position;
            Quaternion followRot = Target.rotation;
            followRot.x = 45.0f;
            followRot.z = 45.0f;
            gameObject.transform.rotation = followRot;
        }
    }

    void Update()
    {
        FollowTarget();
    }
}
