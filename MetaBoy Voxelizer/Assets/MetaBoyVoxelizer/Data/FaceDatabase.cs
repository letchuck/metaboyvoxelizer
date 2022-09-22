using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

[Serializable]
public class FaceDefinition
{
    public string FaceName;
    public Material MaterialOverride;
    public Texture2D FaceTexture;
    public Texture2D WhiteFaceTexture;
}

[Serializable]
[CreateAssetMenu(fileName = "FaceDB", menuName = "Thadeus' Last Stand/Data Base/Faces", order = 0)]
public class FaceDatabase : ScriptableObject
{
    [SerializeField] public List<FaceDefinition> AvailableFaces = new List<FaceDefinition>();

    public bool GetFaceMaterial(string InFaceName, ref Material OutFaceMaterial, bool bIsWhiteFace = false)
    {
        if (InFaceName == null)
        {
            return false;
        }
        string NormalizedName = InFaceName.ToUpper();
        if (NormalizedName.EndsWith(" "))
        {
            NormalizedName = NormalizedName.Substring(0, NormalizedName.Length - 1);
        }
        foreach (FaceDefinition DefinedFace in AvailableFaces)
        {
            if(DefinedFace.FaceName == NormalizedName)
            {
                if(DefinedFace.MaterialOverride != null)
                {
                    OutFaceMaterial = new Material(DefinedFace.MaterialOverride);
                }
                if(bIsWhiteFace && DefinedFace.WhiteFaceTexture != null)
                {
                    OutFaceMaterial.mainTexture = DefinedFace.WhiteFaceTexture;
                    return true;
                }
                OutFaceMaterial.mainTexture = DefinedFace.FaceTexture;
                return true;
            }
        }
        return false;
    }
}

public class FaceManager
{
    public static FaceManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new FaceManager();
                instance.Load();
            }
            return instance;
        }
    }
    private static FaceManager instance;

    public static void Cache()
    {
        if (instance == null)
        {
            instance = new FaceManager();
            instance.Load();
        }
    }

    public FaceDatabase Faces;

    public void Load()
    {
        Faces = Resources.Load<FaceDatabase>("Data/FaceDatabase");
        if (Faces == null)
        {
            Debug.LogError("NO FACES!");
        }
    }

    public FaceManager()
    {
        Load();
    }
}