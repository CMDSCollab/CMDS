using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    public ProgrammerAI proAI;
    public ArtistAI artAI;
    public DesignerAI desAI;

    public Programmer pro;
    public Designer des;

    public GameMaster gM;

    private void Start()
    {
        gM = FindObjectOfType<GameMaster>();
    }

}
