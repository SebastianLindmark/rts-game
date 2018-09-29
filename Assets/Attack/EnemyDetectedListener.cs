using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface EnemyDetectedListener {

    void EnemyInRangeDetected(BaseObject enemy);

    void HitByEnemyDetected(BaseObject enemy);
}
