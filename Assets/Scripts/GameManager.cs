using System;
using System.Collections.Generic;
using System.Linq;
using ExitGames.Client.Photon;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager Instance;
    public Transform spawnPoint;
    public Text scoreText;
    public InputField inputField;
    public GameObject connectButton;
    public GameObject connectingText;
    public Text hitByText;
    public Text killText;
    public GameObject gameStartPanel;
    public GameObject respawnPanel;

    public GameObject playerPrefab;

    private void Awake()
    {
        Instance = this;
        gameStartPanel.SetActive(true);
        respawnPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
        
        Dictionary<string, int> playerKills = new Dictionary<string, int>();
        
        foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
        {
            int kills = 0;
            player.CustomProperties.TryGetValue("kills", out object value);
            if (value != null)
            {
                kills = (int) value;
            }
            
            playerKills.Add(player.NickName, kills);
        }
        
        scoreText.text = "";
        
        foreach (KeyValuePair<string, int> playerKill in playerKills.OrderByDescending(key => key.Value))
        {
            scoreText.text += playerKill.Key + ": " + playerKill.Value + "\n";
        }
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinRandomOrCreateRoom();
    }

    public override void OnJoinedRoom()
    {
        Spawn();
    }

    public void OnButtonClick()
    {
        if (inputField.text.Trim().Length == 0) return;

        PhotonNetwork.NickName = inputField.text.Trim();
        inputField.gameObject.SetActive(false);
        connectButton.gameObject.SetActive(false);
        connectingText.gameObject.SetActive(true);
        
        PhotonNetwork.ConnectUsingSettings();
    }

    public void OnDie()
    {
        Player lastHitBy = PunchPlayer.Instance.lastHitBy;
        if (lastHitBy != null) {
            hitByText.text = lastHitBy.NickName + "에게 죽음!";
        } else {
            hitByText.text = "자살함";
        }

        Cursor.lockState = CursorLockMode.None;
        respawnPanel.SetActive(true);
        PhotonNetwork.Destroy(PunchPlayer.Instance.GetComponent<PhotonView>());
    }
    
    public void OnKill(string victimName)
    {
        killText.gameObject.SetActive(true);
        killText.text = victimName + "을(를) 처치함";
        CancelInvoke(nameof(HideKillText));
        Invoke(nameof(HideKillText), 3);
    }

    public void HideKillText()
    {
        killText.gameObject.SetActive(false);
    }

    public void Spawn()
    {
        Cursor.lockState = CursorLockMode.Locked;
        gameStartPanel.SetActive(false);
        respawnPanel.SetActive(false);

        PhotonNetwork.Instantiate("PunchPlayer", spawnPoint.position, spawnPoint.rotation);
    }
}