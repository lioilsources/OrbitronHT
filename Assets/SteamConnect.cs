using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using UnityEngine.UI;

public class SteamConnect : MonoBehaviour {

    public static SteamConnect _I;

    public Text PlayerName_TXT;
    public Image PlayerAwatar_IMG;

    public Text Comunication_TXT;



    public string[] DataString;


    // prijem zprav
    private Callback<P2PSessionRequest_t> _p2PSessionRequestCallback;

    // Use this for initialization
    void Start () {
        if (!SteamManager.Initialized) return;

        _I = this;

        // Callback pro prijem dat
        _p2PSessionRequestCallback = Callback<P2PSessionRequest_t>.Create(OnP2PSessionRequest);

        PlayerName_TXT.text = SteamFriends.GetPersonaName();


        SteamUserStats.SetStat("stars", 1);
        SteamUserStats.StoreStats();

        int outData;
        SteamUserStats.GetStat("stars", out outData);

        Debug.Log("outData: " + outData.ToString());

        StartCoroutine("FetchAwatar");
        // Set Awatar image :D



        CSteamID receiver = SteamUser.GetSteamID();
        string hello = "Hello!";

        // allocate new bytes array and copy string characters as bytes
        byte[] bytes = new byte[hello.Length * sizeof(char)];
        System.Buffer.BlockCopy(hello.ToCharArray(), 0, bytes, 0, bytes.Length);

        SteamNetworking.SendP2PPacket(receiver, bytes, (uint)bytes.Length, EP2PSend.k_EP2PSendReliable);

    }

    public void SendData( string msg )
    {
        byte[] bytes = new byte[msg.Length * sizeof(char)];
        System.Buffer.BlockCopy(msg.ToCharArray(), 0, bytes, 0, bytes.Length);

        SteamNetworking.SendP2PPacket(SteamUser.GetSteamID(), bytes, (uint)bytes.Length, EP2PSend.k_EP2PSendReliable);
    }


    private int AwatarID;
    public Texture2D AwatarTexture;


    IEnumerator FetchAwatar()
    {
        uint width, height;

        AwatarID = SteamFriends.GetLargeFriendAvatar(SteamUser.GetSteamID() );

        while( AwatarID == -1 )
        {
            yield return null;
        }

        if( AwatarID > 0 )
        {
            SteamUtils.GetImageSize(AwatarID, out width, out height);

            if( width > 0 && height > 0 )
            {
                Debug.Log("Have Awatar image");

                byte[] awatarStream = new byte[4 * (int)width * (int)height];
                SteamUtils.GetImageRGBA( AwatarID, awatarStream, 4 * (int)width * (int)height);

                AwatarTexture = new Texture2D((int)width, (int)height, TextureFormat.ARGB32, false);
                AwatarTexture.LoadRawTextureData(awatarStream);
                AwatarTexture.Apply();

                PlayerAwatar_IMG.sprite = Sprite.Create(AwatarTexture, new Rect(0, 0, 184, 184), new Vector2(0.5f, 0.5f));

            }
        }
    }

    public bool ExpectingClient(CSteamID clientID )
    {
        if (clientID == SteamUser.GetSteamID()) return true;

        return false;
    }

    void OnP2PSessionRequest(P2PSessionRequest_t request)
    {
        CSteamID clientId = request.m_steamIDRemote;
        if (ExpectingClient(clientId))
        {
            SteamNetworking.AcceptP2PSessionWithUser(clientId);
        }
        else
        {
            Debug.LogWarning("Unexpected session request from " + clientId);
        }
    }

    // Update is called once per frame
    void Update () {
        uint size;

        // repeat while there's a P2P message available
        // will write its size to size variable
        while (SteamNetworking.IsP2PPacketAvailable(out size))
        {
            // allocate buffer and needed variables
            var buffer = new byte[size];
            uint bytesRead;
            CSteamID remoteId;

            // read the message into the buffer
            if (SteamNetworking.ReadP2PPacket(buffer, size, out bytesRead, out remoteId))
            {
                // convert to string
                char[] chars = new char[bytesRead / sizeof(char)];
                System.Buffer.BlockCopy(buffer, 0, chars, 0, buffer.Length);

                string message = new string(chars, 0, chars.Length);
                if(remoteId == SteamUser.GetSteamID() )
                {
                    Comunication_TXT.text += SteamFriends.GetPersonaName() + " > " + message + System.Environment.NewLine;
                }
                else
                    Comunication_TXT.text += SteamFriends.GetPlayerNickname(remoteId) + " > " + message + System.Environment.NewLine;
            }
        }
    }
}
