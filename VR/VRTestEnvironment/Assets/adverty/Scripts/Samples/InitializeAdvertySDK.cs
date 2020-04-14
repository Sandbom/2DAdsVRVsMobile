using UnityEngine;
using Adverty;

public class InitializeAdvertySDK : MonoBehaviour
{
    private void Start()
    {
        //Define data and then initialize Adverty SDK

        /*
        string apiKey = "YOUR_KEY";
        AdvertySettings.Mode mode = AdvertySettings.Mode.Mobile;
        bool restrictUserDataCollection = false;
        BirthData birthData = new BirthData(new System.DateTime(1980, 2, 3));
        UserData userData = new UserData(birthData, Gender.Undefined);
        AdvertySDK.Init(apiKey, mode, restrictUserDataCollection, userData);
        */

        //Or use predefined data from Adverty settings window
        AdvertySDK.Init();
    }
}
