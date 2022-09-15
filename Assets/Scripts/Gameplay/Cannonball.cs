using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball : MonoBehaviour
{
    private const int DISAPPEAR_WAIT = 5; // [sec]
    /// <summary>
    /// Inactivates the object so it releases back to the object pool.
    /// </summary>
    public void Disappear()
    {        
        gameObject.SetActive(false);
        // Remove rigid body component to reset velocity and other
        // properties than can create physics issue later on when activated.
        Destroy(GetComponent<Rigidbody>());
    }
    /// <summary>
    /// Disappear (deactivates) the cannonball after some time.
    /// </summary>
    public void InvokeDisappear()
    {
        Invoke(nameof(Disappear), DISAPPEAR_WAIT);
    }

}
