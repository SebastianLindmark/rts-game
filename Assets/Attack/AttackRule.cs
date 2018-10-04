using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface AttackRule {


    bool canAttack(Player player, BaseObject targetObject);

    bool canAttack(BaseObject player, BaseObject targetObject);



}
