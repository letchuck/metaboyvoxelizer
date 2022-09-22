using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualizerSpawner
{
    public GameObject SpawnedMetaBoy;
    public MetaBoyConfig DebugMetaboy;
    public bool bDoSpawn = false;

    public static VisualizerSpawner Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new VisualizerSpawner();
            }
            if(instance.SpawnedMetaBoy == null)
            {
                instance.SpawnedMetaBoy = GameObject.Find("Body_Placeholder");
            }
            return instance;
        }
    }
    private static VisualizerSpawner instance;

    public static void Spawn(MetaBoyData InData)
    {
        if(Instance.SpawnedMetaBoy != null)
        {
            GameObject.Destroy(Instance.SpawnedMetaBoy);
        }
        BodyDefinition Definition = BodyDatabase.Instance.GetBodyDefinition(InData.Body);
        if (Definition != null)
        {
            Instance.SpawnedMetaBoy = GameObject.Instantiate(Definition.BodyPrefab);
            Instance.SpawnedMetaBoy.GetComponent<BodyComponent>().LoadMetaBoy(InData);
        }
    }
    
}
