using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPatent : MonoBehaviour {

    public GameObject groundCheck;
    public GameObject player;
    void Start()
    {
        groundCheck.transform.SetParent(player.transform); 
    }
}
