using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ObjectLifecycleListener {

    void onCreated(BaseObject baseObject);

    void onRemoved(BaseObject baseObject);
	
}
