using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SendRankingScore : MonoBehaviour
{
    [SerializeField] private Text _scoreText = default;

    public void UpdateRankingScore() {
        var statisticUpdate = new StatisticUpdate
        {
            // ランキングの統計情報名
            StatisticName = "テストランキング",

            //スコア(int)
            Value = int.Parse( _scoreText.text),
        };

        var request = new UpdatePlayerStatisticsRequest{
            Statistics = new List<StatisticUpdate>{ statisticUpdate },
        };

        Debug.Log($"スコアの更新開始, score:{int.Parse( _scoreText.text)}");
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnUpdatePlayerStatisticsSuccess, OnUpdatePlayerStatisticsFailure);
    }

    //スコア(統計情報)の更新成功
    private void OnUpdatePlayerStatisticsSuccess(UpdatePlayerStatisticsResult result){
        Debug.Log("スコアの更新が成功しました");
    }

    //スコア(統計情報)の更新失敗
    private void OnUpdatePlayerStatisticsFailure(PlayFabError error){
        Debug.LogError($"スコア更新に失敗しました\n{error.GenerateErrorReport()}");
    }
}
