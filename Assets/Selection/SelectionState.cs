using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface SelectionState {

    void Select(Vector3 clickVector, List<BaseObject> selectedObjects);
    void Unselect(List<BaseObject> objs);

}
