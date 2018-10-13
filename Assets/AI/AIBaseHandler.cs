using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIBaseHandler : MonoBehaviour{

    public abstract int GetDevelopmentLevel();

    public abstract void Advance();

    public abstract void MakeAction();

    public abstract void SetPlayer(Player player);


}
