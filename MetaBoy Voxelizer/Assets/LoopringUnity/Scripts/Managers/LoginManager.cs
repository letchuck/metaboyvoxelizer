using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using WalletConnectSharp.Unity;

public class LoginManager : MonoBehaviour
{    
    //public GameObject panel_Metamask;
    //public GameObject panel_Walletconnect;

    // These are the Right-Hand side Information panel that will be displayed based on selected wallet
    public GameObject loginPanelMetaMask;
    public GameObject loginPanelGameStop;
    public GameObject loginPanelWalletConnect;

    public GameObject unlockPanel;

    private UnlockManager unlockManager;

    // Start is called before the first frame update
    void Start()
    {
        // This is necessary, the WalletConnect panel needs to be active at load time for the QR code to be generated.
        // Could explore other options, such as having the QR Code sprite off screen
        // Or, have them all active in the Hierarchy, and then use Alpha Tween to display the panel to use.
        loginPanelMetaMask.SetActive(false);
        loginPanelMetaMask.SetActive(false);
        loginPanelWalletConnect.SetActive(false);

        unlockManager = gameObject.GetComponent<UnlockManager>();
    }

    private void Update()
    {
        
        if (Constants.WALLET != "" && StateMachine.currentState == Constants.WALLET_DISCONNECT)
        {
            // Wallet is connected, display the Unlock Panel
            CompleteLoginStage();
        }
        /*
        if (panel_Walletconnect.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Button_Metamask();
            }
        }
        */
    }

    async public void Button_Login_MM()
    {
        // Process login for MetaMask
        Debug.Log("Start MetaMask Login");

        // Display MetaMask login instructions panel
        loginPanelMetaMask.SetActive(true);
        // Hide all other login instruction panels
        loginPanelGameStop.SetActive(false);
        loginPanelWalletConnect.SetActive(false);

        // Connect to Web3 Wallet
#if UNITY_WEBGL
        await WalletConnector.ConnectToWeb3Wallet("m");
        StateMachine.SetWallet(Constants.WALLET_METAMASK);
#endif
    }

    async public void Button_Login_GSW()
    {
        // Process login for GameStop Wallet
        Debug.Log("Start GameStop Wallet Login");

        // Display GameStop Wallet login instructions panel;
        loginPanelGameStop.SetActive(true);
        // Hide all other login instruction panels
        loginPanelMetaMask.SetActive(false);
        loginPanelWalletConnect.SetActive(false);

        // Connect to Web3 Wallet
#if UNITY_WEBGL
        await WalletConnector.ConnectToWeb3Wallet("gme");
        StateMachine.SetWallet(Constants.WALLET_GME);
#endif
    }

    public void Button_Login_WC()
    {
        // Process login for WalletConnect
        Debug.Log("Start WalletConnect Login");

        // Display WalletConnect login (QR Code) panel
        loginPanelWalletConnect.SetActive(true);
        // Hide all other login instruction panels
        loginPanelMetaMask.SetActive(false);
        loginPanelGameStop.SetActive(false);

        // Connect handled by WalletConnect QR Code
    }

    // Callback for WalletConnect Manager
    public void WalletConnectConnected()
    {
        Constants.WALLET = WalletConnect.ActiveSession.Accounts[0].ToString();
        StateMachine.SetWallet(Constants.WALLET_WALLETCONNECT);
    }

    // Call with Session Data
    public void WalletConnectConnected(WalletConnect.WalletConnectEventWithSessionData session)
    {
        Debug.Log(session.ToString());
    }

    public void WalletConnectDisconnected()
    {
        Constants.WALLET = "";
        StateMachine.SetWallet(0);
    }

    public void WalletConnectDisconnected(WalletConnect.WalletConnectEventWithSessionData session)
    {
        Debug.Log(session.ToString());
        Constants.WALLET = "";
        StateMachine.SetWallet(0);
    }
    private void CompleteLoginStage()
    {
        Debug.Log("Wallet Connect Phase Complete: Start Unlock Phase");
        StateMachine.currentState = Constants.WALLET_CONNECT;

        // Display Unlock Panel;
        unlockPanel.SetActive(true);
        // Deactivate active Login Instruction panel
        if (loginPanelMetaMask.activeInHierarchy){
            loginPanelMetaMask.SetActive(false);
        }
        if (loginPanelGameStop.activeInHierarchy)
        {
            loginPanelGameStop.SetActive(false);
        }
        if (loginPanelWalletConnect.activeInHierarchy)
        {
            loginPanelWalletConnect.SetActive(false);
        }
        // Initialize UnlockManager
        unlockManager.InitUnlock();
    }
}
