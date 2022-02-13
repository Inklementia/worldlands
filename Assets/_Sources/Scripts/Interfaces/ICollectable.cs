using System.Collections;
using System.Collections.Generic;
using _Sources.Scripts.Player.PlayerFiniteStateMachine;
using UnityEngine;

public interface ICollectable
{
    void ApplyTo(PlayerEntity playerEntity);
}
