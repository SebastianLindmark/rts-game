using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBaseFactory  {

    BaseObject CreateUnit(BaseObject self,BaseObject type);
}
