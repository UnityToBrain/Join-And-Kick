using UnityEngine;


public class damageTheBoss : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<Rigidbody>().isKinematic = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("boss") && Random.Range(0,2) == 1)
        {
            bossManager.BossManagerCls.Health--;
            bossManager.BossManagerCls.Health_bar_amount.text = bossManager.BossManagerCls.Health.ToString();
            bossManager.BossManagerCls.HealthBar.value = bossManager.BossManagerCls.Health;
        }
    }
}
