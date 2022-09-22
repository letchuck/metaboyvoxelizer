using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Threading.Tasks;
using System.Numerics;
using PoseidonSharp;
using WalletConnectSharp.Unity;

public class UnlockManager : MonoBehaviour
{
    // Expose the NFT contract address in the Inspector (instead of hard-coding it..)
    public string contractAddress;
    //Adds UI Gameobjects from the scene.
    public Text text_Wallet;
    public Button button_Unlock;
    public Button button_Proceed;


    //Creates an object of the API caller class, so we can make API calls.
    APICaller m_cli;

    //Querying Looprings API returns a JSON, the responses are stored as an object (JsonOBJS folder).
    apiKeyJson m_ApiKey;
    accountJson m_Account;
    TokenJson m_UserTokens;             // List of tokens that the player has owned (includes for some reason ones that the account has sold/traded)
    List<metadataJson> m_TokenMetadata;       // Array of IPFS links for the player's Tokens
    resolvedENS m_ens;

    void Start()
    {
        //Creates new object to hold users L2 NFTs.
        m_UserTokens = new TokenJson();
        // Initialize list of Metadata
        m_TokenMetadata = new List<metadataJson>();

        //Sets up buttons on Unlock scene.
        button_Unlock.interactable = true;
        button_Proceed.interactable = false;
    }

    public async void InitUnlock()
    {
        Debug.Log("Initialize Unlock Wallet");
        if (StateMachine.currentWallet != Constants.WALLET_NONE && StateMachine.currentState == Constants.WALLET_CONNECT)
        {
            m_cli = gameObject.AddComponent<APICaller>();
            await SetAccount();
            text_Wallet.text = Constants.WALLET;
            await SetENS();
            if (Constants.ENS != "")
            {
                text_Wallet.text = Constants.ENS;
            }
        }
    }
    //UI Button functions:

    public async void UnlockButton()
    {
        Debug.Log("Unlock Button Called");
        if (StateMachine.currentState == Constants.WALLET_CONNECT)
        {
            Debug.Log("Current State: Wallet_Connect. Start Unlock");
            //Signs Unlock Message.
            await SignMessageFunction();

            //Changes Button Text to "Unlocked" when signed (doesnt verify signature).
            button_Unlock.GetComponentInChildren<Text>().text = "Unlocked";

            button_Unlock.interactable = false;
            //Gets Users NFT data after it has unlocked.
            await GetTokens();
            button_Proceed.interactable = true;
        }

    }
    public async void ProceedButton()
    {
        if (StateMachine.currentState == Constants.WALLET_READY)
        {
            // Wallet Is now unlocked, proceed to Game
            Debug.Log("Wallet Unlock Phase Complete: Start Game");
            //await GetTokensData();
            //GetCIDv0(m_UserTokens.data[0].nftId);
            await GetMetadata();
        }
        //SceneManager.LoadScene("Main");

        // GUS: Hiding the load screen after loading.
        GameObject LoadScreen = GameObject.Find("LoadingScreen");
        if(LoadScreen)
        {
            LoadScreen.SetActive(false);
        }
        // GUS: Connecting to the interface in the visualizer.
        if(VisualizerManager.Instance)
        {
            VisualizerManager.Instance.TurnGameOn(m_TokenMetadata);
        }
    }



    //All API calls and Queries below:

    async Task SignMessageFunction()
    {
        try
        {
            //Keyseed is manually set as a string as sometimes
            //m_Account.keySeed is null. Not sure why, may be an await/async issue.
            //string _keyseedMsg = "Sign this message to allow MetaBoy Voxelizer 3000 to find your MetaBoys: 0x0BABA1Ad5bE3a5C0a66E7ac838a129Bf948f1eA4 with key nonce: 0";
            // Keep this until we figure out how to change the signature of this message.
            string _keyseedMsg = "Sign this message to access Loopring Exchange: 0x0BABA1Ad5bE3a5C0a66E7ac838a129Bf948f1eA4 with key nonce: 0";            //Sets empty string to hold the response of the signed message.
            string response = "";


            //Checks which wallet the user has, as WalletConnect has a different signing method,
            //and then signs the keySeed message.
#if UNITY_WEBGL
            switch (StateMachine.currentWallet)
            {
                case Constants.WALLET_METAMASK:
                    response = await WalletConnector.SignWeb3Message("m", Constants.WALLET, _keyseedMsg);
                    break;
                case Constants.WALLET_WALLETCONNECT:
                    response = await PersonalSignWALLETCONNECT(_keyseedMsg);
                    break;
                case Constants.WALLET_GME:
                    response = await WalletConnector.SignWeb3Message("gme", Constants.WALLET, _keyseedMsg);
                    break;
            }
#endif
            //The response is an ECDSA signature, for L2 we need EDDSA,
            //GetApiKeyEDDSASig returns an EDDSA signature from taking in the ECDSA sig,
            //Be aware it is only tailored for getting the Loopring API key.
            await GetApiKey(GetApiKeyEDDSASig(response));

            // Update State
            StateMachine.currentState = Constants.WALLET_READY;
        }
        catch (Exception e)
        {
            Debug.LogException(e, this);
        }
    }

    public async Task SetAccount()
    {
        //Queries the user account
        m_Account = await m_cli.GetAccountIDTask<accountJson>(Constants.WALLET);

        if (m_Account.accountId != null)
        {
            Constants.ACCOUNTID = m_Account.accountId;
        }
    }

    public async Task SetENS()
    {
        //Queries for Loopring ENS
        m_ens = await m_cli.ResolveEns<resolvedENS>(Constants.WALLET);

        //If m_ens.data is not null or empty, Constants.ENS gets it value.
        if(m_ens.data !=null || m_ens.data != "")
        {
            Constants.ENS = m_ens.data;
        }

    }

    async Task GetTokens()
    {
        // Debug all the GetTokenBalance parameters
        Debug.Log("User Account ID: " + m_Account.accountId);
        Debug.Log("User API Key: " + PlayerPrefs.GetString("APIKEY"));
        Debug.Log("Token Contract Address: " + contractAddress);
        //Queries user NFT data
        m_UserTokens = await m_cli.GetTokenBalance<TokenJson>(m_Account.accountId, PlayerPrefs.GetString("APIKEY"), contractAddress);

        //If m_Tokens.data is not null or empty, Constants.Tokens gets it value.
        if (m_UserTokens.data != null)
        {
            Constants.Tokens = m_UserTokens;

            //This can be deleted, but here you can cycle through all of the queried NFTs
            foreach (Data d in m_UserTokens.data)
            {
                Debug.Log("NFT Data: " + d.nftData);
                Debug.Log("NFT ID: " + d.nftId);
                Debug.Log("NFT Token ID: " + d.tokenId);
                Debug.Log("NFT Owner (account ID): " + d.accountId);
            }
        }
    }

    async Task GetMetadata()
    {
        // Retrieve nftIDs from Token Data
        List<String> nftIds = new List<string>();

        foreach (Data d in m_UserTokens.data)
        {
            nftIds.Add(d.nftId);
        }

        // Rencode nftIds as CIDv0
        List<String> nftCIDs = new List<string>();

        foreach (String s in nftIds)
        {
            nftCIDs.Add(GetCIDv0(s));
        }
        // Query IPFS for Metadata
        foreach (String i in nftCIDs)
        {
            var data = await m_cli.GetMetadata<metadataJson>(i);
        
            m_TokenMetadata.Add(data);
        }

        // Test debug display Metadata
        foreach (metadataJson metadata in m_TokenMetadata)
        {
            if(metadata == null)
            {
                Debug.LogError("Meta data was null!!!");
                continue;
            }
            Debug.Log("Name: " + metadata.name);
            Debug.Log("Image: " + metadata.image);
            foreach(var attribute in metadata.attributes)
            {
                Debug.Log("Property - " + attribute.trait_type + ": " + attribute.value);
            }
        }
    }
    async Task GetApiKey(string _xapisig)
    {
        //Queries users API Key
        m_ApiKey = await m_cli.GetAPIKey<apiKeyJson>(m_Account.accountId, _xapisig);


        if (m_ApiKey.apiKey == "" || m_ApiKey.apiKey == null)
        {
            Debug.Log("Failed to get API key check response");
        }
        else
        {
            //If successful, it is stored in PlayerPrefs (but cleared in next scene).
            PlayerPrefs.SetString("APIKEY", m_ApiKey.apiKey);
        }

    }

    //Sign a message with WalletConnect
    public async Task<string> PersonalSignWALLETCONNECT(string message, int addressIndex = 0)
    {
        var address = WalletConnect.ActiveSession.Accounts[addressIndex];
        var results = await WalletConnect.ActiveSession.EthPersonalSign(address, message);

        return results;
    }

    //This function takes an ECDSA signature, uses Fudgeys PoseidonSharp
    //to get an EDDSA signature. See PosedionHelper.cs
    public string GetApiKeyEDDSASig(string _signedEcDSA)
    {
        var _keyDeets = PoseidonHelper.GetL2PKFromMetaMask(_signedEcDSA);

        string _api_signatureBase = "GET&https%3A%2F%2Fapi3.loopring.io%2Fapi%2Fv3%2FapiKey&accountId%3D" + m_Account.accountId;
        BigInteger _sigbaseToBitInt = SHA256Helper.CalculateSHA256HashNumber(_api_signatureBase);
        Eddsa eddsa = new Eddsa(_sigbaseToBitInt, _keyDeets.secretKey); //Put in the calculated poseidon hash in order to Sign
        string signedMessage = eddsa.Sign();


        return signedMessage;
    }

    public string GetCIDv0(string _nftId)
    {
        // First strip 0x from head of 32 byte hex string
        string rawnftId = _nftId.Substring(2, _nftId.Length - 2);
        Debug.Log("Raw 32byte NFTId: " + rawnftId);
        // Add 1220 to the raw id
        rawnftId = "1220" + rawnftId;
        Debug.Log("Base58 Prep: " + rawnftId);
        // Convert to array of bytes
        var bytes = StringToByteArray(rawnftId);
        //Encode in base58
        var cidAdd = Merkator.BitCoin.Base58Encoding.Encode(bytes);
        Debug.Log("CIDv0: " + cidAdd);

        return cidAdd;
    }

    public static byte[] StringToByteArray(String hex)
    {
        int NumberChars = hex.Length;
        byte[] bytes = new byte[NumberChars / 2];
        for (int i = 0; i < NumberChars; i += 2)
            bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
        return bytes;
    }
}
