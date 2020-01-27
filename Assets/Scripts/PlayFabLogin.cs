using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using System.Collections.Generic;

public class PlayFabLogin : MonoBehaviour
{

    /*
    #########################
    Login
    #########################
    */
    public void Start()
    {
        // Note: Setting title Id here can be skipped if you have set the value in Editor Extensions already.
        if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId)){
            PlayFabSettings.staticSettings.TitleId = "XXXX"; // Please change this value to your own titleId from PlayFab Game Manager
        }
        var request = new LoginWithCustomIDRequest { CustomId = "GettingStartedGuide", CreateAccount = true};
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
    }

    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Congratulations, you made your first successful API call!");
        // GetTitleData();
        // GetUserData();
        // UpdateUserData();
    }

    private void OnLoginFailure(PlayFabError error)
    {
        Debug.LogWarning("Something went wrong with your first API call.  :(");
        Debug.LogError("Here's some debug information:");
        Debug.LogError(error.GenerateErrorReport());
    }

    /*
    #########################
    TitleData(MasterData)
    #########################
    */
    public static void GetTitleData()
    {
        var request = new GetTitleDataRequest();
        PlayFabClientAPI.GetTitleData(request, OnSuccess, OnError);

        void OnSuccess(GetTitleDataResult result)
        {
            Debug.Log("GetTitleData: Success!");

            var loginMessage = result.Data["LoginMessage"];
            Debug.Log(loginMessage);

            var gachaMaster = Utf8Json.JsonSerializer.Deserialize<GachaMaster[]>(result.Data["GachaMaster"]);
            foreach (var master in gachaMaster)
            {
                Debug.Log(master.Name);
            }
        }

        void OnError(PlayFabError error)
        {
            Debug.Log("GetTitleData: Fail...");
            Debug.Log(error.GenerateErrorReport());
        }
    }

    public class GachaMaster
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Rank { get; set; }
        public int Rate { get; set; }
    }

    /*
    #########################
    UserData(PlayerData)
    #########################
    */
    public static void GetUserData()
    {
        var request = new GetUserDataRequest();
        PlayFabClientAPI.GetUserData(request, OnSuccess, OnError);

        void OnSuccess(GetUserDataResult result)
        {
            Debug.Log("GetUserData: Success!");
            Debug.Log($"My name is {result.Data["Name"].Value}");
        }

        void OnError(PlayFabError error)
        {
            Debug.Log("GetUserData: Fail...");
            Debug.Log(error.GenerateErrorReport());
        }
    }

    public static void UpdateUserData()
    {
        var request = new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>
            {
                { "Name", "Jim" },
                //{ "Job", "Engineer" }
            }
        };

        PlayFabClientAPI.UpdateUserData(request, OnSuccess, OnError);

        void OnSuccess(UpdateUserDataResult result)
        {
            Debug.Log("UpdateUserData: Success!");
            GetUserData();
        }

        void OnError(PlayFabError error)
        {
            Debug.Log("UpdateUserData: Fail...");
            Debug.Log(error.GenerateErrorReport());
        }
    }
}
