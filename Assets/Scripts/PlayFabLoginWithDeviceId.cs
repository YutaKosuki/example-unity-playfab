using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFabLoginWithDeviceId : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Note: Setting title Id here can be skipped if you have set the value in Editor Extensions already.
        if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId)){
            PlayFabSettings.staticSettings.TitleId = "XXXX"; // Please change this value to your own titleId from PlayFab Game Manager
        }
        var deviceId = this.GetDeviceId();
        
#if UNITY_IPHONE
        var request = new LoginWithIOSDeviceIDRequest{DeviceId = deviceId, CreateAccount = true};
        PlayFabClientAPI.LoginWithIOSDeviceID(request, OnLoginSuccess, OnLoginFailure);
#elif UNITY_ANDROID
        var request = new LoginWithAndroidDeviceIDRequest{DeviceId = deviceId, CreateAccount = true};
        PlayFabClientAPI.LoginWithAndroidDeviceID(request, OnLoginSuccess, OnLoginFailure);
#else
        var request = new LoginWithCustomIDRequest { CustomId = deviceId, CreateAccount = true};
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
#endif
    }

    private string GetDeviceId() {
#if UNITY_IPHONE
        return UnityEngine.iOS.Device.vendorIdentifier;
#else
        // Android : AndroidId
        // Windows Standalone : hash from the concatenation of strings taken from Computer System Hardware Classes
        return SystemInfo.deviceUniqueIdentifier;
#endif
    }

    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Congratulations, you made your first successful API call!");
    }

    private void OnLoginFailure(PlayFabError error)
    {
        Debug.LogWarning("Something went wrong with your first API call.  :(");
        Debug.LogError("Here's some debug information:");
        Debug.LogError(error.GenerateErrorReport());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
