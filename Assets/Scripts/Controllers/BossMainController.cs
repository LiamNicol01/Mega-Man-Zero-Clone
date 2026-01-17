using Unity.VisualScripting;
using UnityEngine;

public class BossMainController : MonoBehaviour
{
    [SerializeField] private GameObject _wizard;

    private void BossUpdate()
    {
		UIManager.Instance.UpdateBossHp(this.GetComponent<isEnemy>()._hp);
        if (this.GetComponent<isEnemy>()._hp == 0)
        {
            this.GetComponent<Renderer>().enabled = false;
            _wizard.GetComponent<Animator>().SetTrigger("death");
        }
    }

    void Update()
    {
        BossUpdate();
    }
}
