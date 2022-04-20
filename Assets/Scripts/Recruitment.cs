using System.Linq;
using UnityEngine;

public class Recruitment : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("add"))
        {
            PlayerManager.PlayerManagerCls.Rblst.Add(other.collider.GetComponent<Rigidbody>());

            other.transform.parent = null;

            other.transform.parent = PlayerManager.PlayerManagerCls.transform;

            if (!other.collider.gameObject.GetComponent<Recruitment>())
            {
                other.collider.gameObject.AddComponent<Recruitment>();
            }

            other.collider.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material =
                PlayerManager.PlayerManagerCls.Rblst.ElementAt(0).transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material;
        }
    }
}
