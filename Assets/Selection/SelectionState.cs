using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface SelectionState {

    void Select(float x, float y, List<BaseObject> selectedObjects);
    void Unselect(List<BaseObject> objs);

}
