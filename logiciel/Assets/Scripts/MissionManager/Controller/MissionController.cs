using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionController : MonoBehaviour
{
    public List<Mission> missionsList = new List<Mission>();
    void Start(){
        MissionInitializer mi = new MissionInitializer();
        mi.Init(); 
        missionsList = mi.missionList;
    }
}