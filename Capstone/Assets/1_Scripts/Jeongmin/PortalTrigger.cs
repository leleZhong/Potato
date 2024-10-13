using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTrigger : MonoBehaviour
{
    public PortalManager _pm;
    
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _pm.EnteredPortal(gameObject, other.tag);
        }
    }
}
