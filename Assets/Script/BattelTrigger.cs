using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattelTrigger : MonoBehaviour
{
    Player player;
    public void init(Player _player) 
    {
        player = _player;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(LayerName.Monster.ToString())) 
        {
            Monster monster = other.gameObject.GetComponent<Monster>();
            monster.AiStateUpdate(AiState.Reset);
        }
    }
}
