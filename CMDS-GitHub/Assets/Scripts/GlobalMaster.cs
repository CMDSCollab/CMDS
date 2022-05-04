using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalMaster : MonoBehaviour
{
    public CharacterType characterType;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
