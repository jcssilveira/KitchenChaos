using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetStaticDataManager : MonoBehaviour
{

    void Start()
    {
        CuttingCounter.ResetStaticData();
        TrashCounter.ResetStaticData();
    }


}
