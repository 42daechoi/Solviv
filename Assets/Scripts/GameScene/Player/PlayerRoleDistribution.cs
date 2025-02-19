using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum PlayerRole
{
    Citizen,
    Mannequin
}
public class PlayerRoleDistribution : MonoBehaviour
{
    public PlayerRole role;

    void Start()
    {
        PlayerRoleDistribution[] players = FindObjectsOfType<PlayerRoleDistribution>();
        if (players.Length == 0)
        {
            Debug.LogWarning("플레이어가 없는데요.");
            return;
        }
        int mannequinIndex = Random.Range(0, players.Length);

        for (int i = 0; i < players.Length; i++)
        {
            if (i == mannequinIndex)
            {
                players[i].role = PlayerRole.Mannequin;
            }
            else
            {
                players[i].role = PlayerRole.Citizen;
            }
            Debug.Log("나의 역할: "+role);
        }
    }
}
