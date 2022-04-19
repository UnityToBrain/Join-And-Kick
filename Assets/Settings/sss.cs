using System;
using UnityEngine;

public class sss : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("add"))
        {
         //    other.transform.parent = null;
         // plrManager.playerManagerCls.teamMate.Add(other.transform);
         // plrManager.playerManagerCls.rbList.Add(other.collider.GetComponent<Rigidbody>());
         //
         // other.transform.parent = plrManager.playerManagerCls.transform;
         //
         // if (!other.gameObject.GetComponent<Recruitment>())
         //     other.gameObject.AddComponent<Recruitment>();
        }
    }
}
