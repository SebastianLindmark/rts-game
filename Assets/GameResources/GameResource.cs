using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResource {


    int availableResource = 0;


    public GameResource(int startResources) {
        AddResource(startResources);
    }

    public void AddResource(int amount) {
        availableResource += amount;

        if (availableResource > 9999)
        {
            availableResource = 9999;
        }
    }

    public void RemoveResource(int amount) {
        availableResource -= amount;

        if (availableResource <= 0) {
            availableResource = 0;
        }
    }

    public int GetAvailableResources() {
        return availableResource;
    }

}
