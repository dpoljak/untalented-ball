﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private List<IPlayerRespawnListener> _listeners; 
    public void Awake()
    {
        _listeners = new List<IPlayerRespawnListener>();
    }

    public void PlayerHitCheckpoint()
    {
        StartCoroutine(PlayerHitCheckpointCo(LevelManager.Instance.CurrentTimeBonus));

    }

    private IEnumerator PlayerHitCheckpointCo(int bonus)
    {
        FloatingText.Show("Checkpoint!", "CheckpointText", new CenteredTextPositioner(.3f));
        yield return new WaitForSeconds(.5f);
        FloatingText.Show(string.Format("+{0} time bonus!", bonus), "CheckpointText", new CenteredTextPositioner(.3f));
    }

    public void PlayerLeftCheckpoint()
    {
    }

    public void SpawnPlayer(Player player)
    {
        player.RespawnAt(transform);

        foreach (var listener in _listeners)
            listener.OnPlayerRespawnInThicCheckpoint(this, player);
        {
            
        }
    }

    public void AssignObjectToCheckpoint(IPlayerRespawnListener listener)
    {
        _listeners.Add(listener);
    }
}