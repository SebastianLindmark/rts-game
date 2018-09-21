using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardAttackRule : AttackRule
{
    public bool canAttack(Player player, BaseObject targetObject)
    {
        Debug.Log("Called");
        return player.getPlayerId() != targetObject.GetOwner().getPlayerId();
    }
}
