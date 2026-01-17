using UnityEngine;

public class MagicAoELogic : MonoBehaviour
{
    private void AoELogic()
    {
        Vector3 newScale = transform.localScale;
        newScale.x += (0.25f + 3.0f - newScale.x) * Time.deltaTime;
        newScale.y += (0.25f + 3.0f - newScale.y) * Time.deltaTime;
        newScale.z += (0.25f + 3.0f - newScale.z) * Time.deltaTime;
        transform.localScale = newScale;
        if (newScale.x > 3.0f) Destroy(gameObject);
    }

    void Update()
    {
        AoELogic();
    }
}
