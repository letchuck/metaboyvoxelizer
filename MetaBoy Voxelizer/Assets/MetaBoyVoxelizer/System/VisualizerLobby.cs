using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualizerLobby : MonoBehaviour
{
    public GameObject LobbyBoyPrefab;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public static VisualizerLobby Instance
    {
        get
        {
            return instance;
        }
    }
    private static VisualizerLobby instance;

    public static void SpawnInLobby(MetaBoyData InData)
    {       
        BodyDefinition Definition = BodyDatabase.Instance.GetBodyDefinition(InData.Body);
        if (Definition != null && Definition.BodyName != "UNKNOWN")
        {
            float RandomX = Random.Range(-155.0f, -245.0f);
            float RandomZ = Random.Range(55.0f, 145.0f);
            Vector3 SpawnLoc = new Vector3(RandomX, 1.0f, RandomZ);
            GameObject LobbyBoy = Instantiate(Instance.LobbyBoyPrefab, SpawnLoc, Quaternion.identity);
            GameObject SpawnedMetaBoy = Instantiate(Definition.BodyPrefab, LobbyBoy.transform);
            SpawnedMetaBoy.transform.localPosition = new Vector3(0.0f, -1.0f, 0.0f);
            SpawnedMetaBoy.GetComponent<BodyComponent>().LoadMetaBoy(InData);
            LobbyBoy.SetActive(true);
        }
    }
}
