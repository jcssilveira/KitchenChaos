using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlateCounterVisual : MonoBehaviour
{
    [SerializeField] private GameObject plate;
    [SerializeField] private PlateCounter plateCounter;
    private Stack<GameObject> plateStack;
    private Vector3 plateOffset = new Vector3(0, 0.1f, 0);

    private void Start() {
        plateStack=new Stack<GameObject>();
        plateCounter.OnPlateSpawned+=PlateCounter_OnPlateSpawned;
        plateCounter.OnPlateRemoved+=PlateCounter_OnPlateRemoved;
    }

    public void PlateCounter_OnPlateSpawned(object sender, EventArgs e) {
        //set counterTopPoint as the parent of the plate + a little offset from the top plate
        if(plateStack.Count!=0) {
            //instantiate a new plate on the top of the stack + the offset
            plate.transform.position=plateStack.Peek().transform.position+plateOffset;

        }
        else {
            //set plate on the counter spawn point (use getobjectspawnpoint)
            plate.transform.position = plateCounter.GetObjectSpawnPoint().position;
        }
        GameObject newPlate = Instantiate(plate);
        plateStack.Push(newPlate);
    }
    private void PlateCounter_OnPlateRemoved(object sender, EventArgs e) {
        Destroy(plateStack.Peek());
        plateStack.Pop();
    }

}
