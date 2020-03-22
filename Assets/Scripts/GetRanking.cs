using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetRanking : MonoBehaviour
{
    [SerializeField] private Text _rankingText = default;

    public void GetLeaderboard() { 
        Debug.Log($"ランキング(リーダーボード)の取得開始");

        // トップランキング
        //GetLeaderboardRequestのインスタンスを生成
        var request = new GetLeaderboardRequest {
            StatisticName   = "テストランキング", //ランキング名(統計情報名)
            StartPosition   = 0,                 //何位以降のランキングを取得するか
            MaxResultsCount = 5,                 //ランキングデータを何件取得するか(最大100)
        };
        //ランキング(リーダーボード)を取得
        PlayFabClientAPI.GetLeaderboard(request, OnGetLeaderboardSuccess, OnGetLeaderboardFailure);

/*        
        // 周辺のランキング
        //GetLeaderboardAroundPlayerRequestのインスタンスを生成
        var request = new GetLeaderboardAroundPlayerRequest {
            StatisticName = "テストランキング", // 統計情報名を指定します。
            MaxResultsCount = 3 // 自分と+-5位をあわせて合計5件を取得します。
        };
        //ランキング(リーダーボード)を取得
        PlayFabClientAPI.GetLeaderboardAroundPlayer(request, OnGetLeaderboardAroundPlayerSuccess, OnGetLeaderboardFailure);
*/
    }

    private void OnGetLeaderboardSuccess(GetLeaderboardResult result){
        Debug.Log($"ランキング(リーダーボード)の取得に成功しました");

        //result.Leaderboardに各順位の情報(PlayerLeaderboardEntry)が入っている
        _rankingText.text = "";
        foreach (var entry in result.Leaderboard) {
            _rankingText.text += $"\n順位 : {entry.Position}, スコア : {entry.StatValue}, 名前 : {entry.DisplayName}";
        }
    }

    private void OnGetLeaderboardAroundPlayerSuccess(GetLeaderboardAroundPlayerResult result){
        Debug.Log($"自分の順位周辺のランキング(リーダーボード)の取得に成功しました");

        //result.Leaderboardに各順位の情報(PlayerLeaderboardEntry)が入っている
        _rankingText.text = "";
        foreach (var entry in result.Leaderboard) {
            _rankingText.text += $"\n順位 : {entry.Position}, スコア : {entry.StatValue}, 名前 : {entry.DisplayName}";
        }
    }

    private void OnGetLeaderboardFailure(PlayFabError error){
        Debug.LogError($"ランキング(リーダーボード)の取得に失敗しました\n{error.GenerateErrorReport()}");
    }
}
