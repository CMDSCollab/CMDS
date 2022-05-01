using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMaster : MonoBehaviour
{
    public GameMaster gM;

    private void Awake()
    {
        gM = FindObjectOfType<GameMaster>();
    }
    public void LoadThisScene(string scenename)
    {
        SceneManager.LoadScene(scenename);
        gM.LoadingForFightScene();
    }
}
