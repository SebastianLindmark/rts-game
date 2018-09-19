using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBaseObject {
    void SetOwner(Player p);
    Player GetOwner();
}
