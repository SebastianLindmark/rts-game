using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBaseFactory  {

    BaseObject CreateObject(BaseObject self,BaseObject type, Vector3 position);
}
