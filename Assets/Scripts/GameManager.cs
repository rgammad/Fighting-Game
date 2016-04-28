using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public GameObject player1, player2;
    
    struct PlayerStruct
    {
        public GameObject player;
        public PlayerController playerScript;
        public string attackButton, jumpButton;
        public string horizontal, vertical;
        public PlayerStruct(GameObject player, string attack, string jump, string xAxis, string yAxis)
        {
            this.player = player;
            playerScript = player.GetComponent<PlayerController>();
            attackButton = attack;
            jumpButton = jump;
            horizontal = xAxis;
            vertical = yAxis;
        }
    }
    PlayerStruct p1Struct;
    PlayerStruct p2Struct;

    void Awake()
    {
        p1Struct = new PlayerStruct(player1, "p1Attack1", "p1Jump", "p1Horizontal", "p1Vertical");
        p2Struct = new PlayerStruct(player2, "p2Attack1", "p2Jump", "p2Horizontal", "p2Vertical");
    }

    void Update()
    {
        PlayerHandler(p1Struct);
        PlayerHandler(p2Struct);
    }

    void PlayerHandler(PlayerStruct player) {


    }
}
