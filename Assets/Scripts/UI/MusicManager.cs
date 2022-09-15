using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MusicManager : MonoBehaviour
{
    public static MusicManager instance = null;

    void Awake()
    {
        MakeSingleton();
    }

    /// <summary>
    /// Make this class as singleton to make sure there is a sigle instance of this script.
    /// </summary>
    private void MakeSingleton()
    {
        if (instance == null)
            instance = this;
        // If, mistakenly, there is second isntance of this script attached to another gameObject,
        // delete the gameObject (which consequently deletes this script instance).
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

}
