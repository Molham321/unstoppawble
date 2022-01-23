using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPos : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        transform.position = gameManager.lastCheckPointPos;
    }

}
