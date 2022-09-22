using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaBoyList : MonoBehaviour
{
    public Transform Content;
    public GameObject MetaBoyListingPrefab;
    public List<GameObject> AvailableMetaBoyListings = new List<GameObject>();
    public bool bWillDebugCreate = false;

    private void Start()
    {
        if(bWillDebugCreate)
        {
            LoadDebugMetaBoys();
        }
    }

    private void ClearBoys()
    {
        if(AvailableMetaBoyListings.Count == 0)
        {
            return;
        }
        for(int BoyIndex = AvailableMetaBoyListings.Count - 1; BoyIndex >= 0; BoyIndex--)
        {
            Destroy(AvailableMetaBoyListings[BoyIndex]);
        }
        AvailableMetaBoyListings = new List<GameObject>();
    }

    // Use this to fill the list with metaboys. Take it from the wallet and go.
    public void LoadMetaBoys(List<MetaBoyData> InMetaBoys)
    {
        ClearBoys();
        foreach(MetaBoyData MetaBoy in InMetaBoys)
        {
            GameObject Listing = Instantiate(MetaBoyListingPrefab, Content);
            Listing.GetComponent<MetaBoyListing>().SetMetaBoy(MetaBoy);
            AvailableMetaBoyListings.Add(Listing);
            VisualizerLobby.SpawnInLobby(MetaBoy);
        }
    }

    public void LoadNftMetaBoys(List<metadataJson> Tokens)
    {
        List<MetaBoyData> NftBoys = new List<MetaBoyData>();
        foreach (metadataJson metadata in Tokens)
        {
            if(metadata == null)
            {
                Debug.LogError("FOUND NULL DATA.");
                continue;
            }
            MetaBoyData Data = new MetaBoyData(metadata);
            NftBoys.Add(Data);
        }
        LoadMetaBoys(NftBoys);
    }

    private void LoadDebugMetaBoys()
    {
        var MetaBoyArray = Resources.LoadAll<MetaBoyConfig>("MetaBoys");
        List<MetaBoyData> MetaData = new List<MetaBoyData>();
        foreach(MetaBoyConfig MetaBoy in MetaBoyArray)
        {
            MetaData.Add(MetaBoy.CreateData());
        }
        LoadMetaBoys(MetaData);
    }
}
