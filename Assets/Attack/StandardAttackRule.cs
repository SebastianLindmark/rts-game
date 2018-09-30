﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardAttackRule : AttackRule
{
    public bool canAttack(Player player, BaseObject targetObject)
    {
        return player.getPlayerId() != targetObject.GetPlayer().getPlayerId();
    }
}
