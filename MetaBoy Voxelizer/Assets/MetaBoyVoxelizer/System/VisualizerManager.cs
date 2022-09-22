using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualizerManager : MonoBehaviour
{
    public GameObject MainPanel;
    public MetaBoyList MetaList;


    private void Awake()
    {
        if(!instance)
        {
            instance = this;
        }
    }

    public static VisualizerManager Instance
    {
        get
        {
            return instance;
        }
    }
    public static VisualizerManager instance;

    public void TurnGameOn(List<metadataJson> Tokens)
    {
        MainPanel.SetActive(false);
        MetaList.LoadNftMetaBoys(Tokens);
    }
}
