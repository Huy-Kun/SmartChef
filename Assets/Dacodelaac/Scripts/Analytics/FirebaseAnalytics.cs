#if FIREBASE
using System;
using System.Collections.Generic;
using System.Linq;
using Dacodelaac.DebugUtils;
using Dacodelaac.Utils;
using Dacodelaac.Variables;
using Firebase.Analytics;
using SurvivorRoguelike;
using UnityEngine;

namespace Dacodelaac.Analytics
{
    [CreateAssetMenu(menuName = "Analytics/Firebase")]
    public class FirebaseAnalytics : AbstractAnalyticsProvider
    {
        public IntegerVariable gameMode; 
            
        bool ready;

        public override void Initialize(Action onCompleted)
        {
            ready = true;
            if (Application.platform == RuntimePlatform.Android)
            {
                // Android => We must resolve the dependencies first
                ready = false;
                Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
                {
                    var dependencyStatus = task.Result;
                    if (dependencyStatus == Firebase.DependencyStatus.Available)
                    {
                        var app = Firebase.FirebaseApp.DefaultInstance;
                        ready = true;
                        // fetch data in here
                        onCompleted?.Invoke();
                    }
                    else
                    {
                        Debug.LogErrorFormat("Firebase.FirebaseApp.CheckAndFixDependenciesAsync: Could not resolve all Firebase dependencies: {0}", dependencyStatus);
                    }
                });
            }
            else
            {
                var app = Firebase.FirebaseApp.DefaultInstance;
                onCompleted?.Invoke();
            }
        }

        public override void LogEvent(string eventName, Dictionary<string, object> param = null)
        {
            Dacoder.Log("Firebase.LogEvent", eventName, param.ToString<string, object>());

            if (param == null)
            {
                #if !UNITY_EDITOR
                Firebase.Analytics.FirebaseAnalytics.LogEvent(eventName);
                #endif
            }
            else
            {
                var parameters = param.Select(p => new Parameter(p.Key, p.Value.ToString())).ToArray();
                #if !UNITY_EDITOR
                Firebase.Analytics.FirebaseAnalytics.LogEvent(eventName, parameters);
                #endif
            }
        }

        public override void OnLevelStart(Dictionary<string, object> param = null)
        {
            var eventName = gameMode.Value switch
            {
                (int)GameMode.Normal => "NORMAL_LEVEL_START",
                (int)GameMode.Challenge => "CHALLENGE_LEVEL_START",
                (int)GameMode.Tournament => "TOURNAMENT_LEVEL_START",
                _ => throw new NotSupportedException()
            };
            if (param != null)
                param.Add("LEVEL_INDEX_NAME", $"Level_index-{param["index"]}_name-{param["name"]}");
            
            LogEvent(eventName, param);
        }

        public override void OnLevelCompleted(Dictionary<string, object> param = null)
        {
            var eventName = gameMode.Value switch
            {
                (int)GameMode.Normal => "NORMAL_LEVEL_COMPLETED",
                (int)GameMode.Challenge => "CHALLENGE_LEVEL_COMPLETED",
                (int)GameMode.Tournament => "TOURNAMENT_LEVEL_COMPLETED",
                _ => throw new NotSupportedException()
            };
            if (param != null)
                param.Add("LEVEL_INDEX_NAME", $"Level_index-{param["index"]}_name-{param["name"]}");
            
            LogEvent(eventName, param);
        }

        public override void OnLevelFailed(Dictionary<string, object> param = null)
        {
            var eventName = gameMode.Value switch
            {
                (int)GameMode.Normal => "NORMAL_LEVEL_FAILED",
                (int)GameMode.Challenge => "CHALLENGE_LEVEL_FAILED",
                (int)GameMode.Tournament => "TOURNAMENT_LEVEL_FAILED",
                _ => throw new NotSupportedException()
            };
            if (param != null)
                param.Add("LEVEL_INDEX_NAME", $"Level_index-{param["index"]}_name-{param["name"]}");
            
            LogEvent(eventName, param);
        }

        public override void OnLevelRestart(Dictionary<string, object> param = null)
        {
            var eventName = gameMode.Value switch
            {
                (int)GameMode.Normal => "NORMAL_LEVEL_RESTART",
                (int)GameMode.Challenge => "CHALLENGE_LEVEL_RESTART",
                (int)GameMode.Tournament => "TOURNAMENT_LEVEL_RESTART",
                _ => throw new NotSupportedException()
            };
            if (param != null)
                param.Add("LEVEL_INDEX_NAME", $"Level_index-{param["index"]}_name-{param["name"]}");
            
            LogEvent(eventName, param);
        }

        public override void RollSkill()
        {
            LogEvent("ADS_ROLL_SKILL");
        }

        public override void OnAdPaid(Dictionary<string, object> param = null)
        {
            Dacoder.Log("Firebase.OnAdPaid", param.ToString<string, object>());
            if (param != null)
            {
                var impressionParameters = new[]
                {
                    new Parameter("ad_platform", param["mediation"].ToString()),
                    new Parameter("ad_source", param["network"].ToString()),
                    new Parameter("ad_unit_name", param["unit"].ToString()),
                    new Parameter("ad_format", param["adFormat"].ToString()),
                    new Parameter("value", (double)param["revenue"]),
                    new Parameter("currency", param["currency"].ToString())
                };
                Firebase.Analytics.FirebaseAnalytics.LogEvent("ad_impression", impressionParameters);
            }
        }

        public override void OnAdRewardedCompleted(
            Dictionary<string, object> param)
        {
            LogEvent("ON_AD_REWARDED_COMPLETED", param);
        }

        public override void OnPurchaseProduct(Dictionary<string, object> param = null)
        {
#if UNITY_EDITOR
            Dacoder.Log("Editor_Firebase.LogEvent", "IN_APP_PURCHASING", param.ToString<string, object>());
            if(param != null) Dacoder.Log("Editor_Firebase.LogEvent", param["BUY_PRODUCT"].ToString());
#else
            LogEvent("IN_APP_PURCHASING", param);
            if (param != null) LogEvent(param["BUY_PRODUCT"].ToString());
#endif
        }

        public override void TutorialUpLevel1(Dictionary<string, object> param = null)
        {
            if (param != null)
            {
                param.Add("LEVEL_INDEX_NAME", $"Level_index-{param["index"]}_name-{param["name"]}");
            }
            LogEvent("TutorialUpLevel1", param);
        }

        public override void TutorialUpLevel2(Dictionary<string, object> param = null)
        {
            if (param != null)
            {
                param.Add("LEVEL_INDEX_NAME", $"Level_index-{param["index"]}_name-{param["name"]}");
            }
            LogEvent("TutorialUpLevel2", param);
        }

        public override void TutorialUpLevel3(Dictionary<string, object> param = null)
        {
            if (param != null)
            {
                param.Add("LEVEL_INDEX_NAME", $"Level_index-{param["index"]}_name-{param["name"]}");
            }
            LogEvent("TutorialUpLevel3", param);
        }

        public override void TutorialUseUltimate(Dictionary<string, object> param = null)
        {
            if (param != null)
            {
                param.Add("LEVEL_INDEX_NAME", $"Level_index-{param["index"]}_name-{param["name"]}");
            }
            LogEvent("TutorialUseUltimate", param);
        }

        public override void KillMiniBoss1(Dictionary<string, object> param = null)
        {
            if (param != null)
            {
                param.Add("LEVEL_INDEX_NAME", $"Level_index-{param["index"]}_name-{param["name"]}");
            }
            LogEvent("KillMiniBoss1", param);
        }

        public override void KillMiniBoss2(Dictionary<string, object> param = null)
        {
            if (param != null)
            {
                param.Add("LEVEL_INDEX_NAME", $"Level_index-{param["index"]}_name-{param["name"]}");
            }
            LogEvent("KillMiniBoss2", param);
        }

        public override void KillBoss1(Dictionary<string, object> param = null)
        {
            if (param != null)
            {
                param.Add("LEVEL_INDEX_NAME", $"Level_index-{param["index"]}_name-{param["name"]}");
            }
            LogEvent("KillBoss1", param);
        }

        public override void KillBoss2(Dictionary<string, object> param = null)
        {
            if (param != null)
            {
                param.Add("LEVEL_INDEX_NAME", $"Level_index-{param["index"]}_name-{param["name"]}");
            }
            LogEvent("KillBoss2", param);
        }

        public override void LoseInFirstGame(Dictionary<string, object> param = null)
        {
            if (param != null)
            {
                param.Add("LEVEL_INDEX_NAME", $"Level_index-{param["index"]}_name-{param["name"]}_time-{param["failed_time"]}");
            }
            LogEvent("LoseInFirstGame", param);
        }

        public override void PlayChapter2(Dictionary<string, object> param = null)
        {
            if (param != null)
            {
                param.Add("LEVEL_INDEX_NAME", $"Level_index-{param["index"]}_name-{param["name"]}");
            }
            LogEvent("PlayChapter2", param);
        }

        public override void WinChapter2(Dictionary<string, object> param = null)
        {
            if (param != null)
            {
                param.Add("LEVEL_INDEX_NAME", $"Level_index-{param["index"]}_name-{param["name"]}");
            }
            LogEvent("WinChapter2", param);
        }

        public override void ClickDailyReward(Dictionary<string, object> param = null)
        {
            if (param != null)
            {
                param.Add("LEVEL_INDEX_NAME", $"Level_index-{param["index"]}_name-{param["name"]}");
            }
            LogEvent("ClickDailyReward", param);
        }

        public override void ClaimDailyReward(Dictionary<string, object> param = null)
        {
            if (param != null)
            {
                param.Add("LEVEL_INDEX_NAME", $"Level_index-{param["index"]}_name-{param["name"]}");
            }
            LogEvent("ClaimDailyReward", param);
        }

        public override void OpenRareChestFirstTime(Dictionary<string, object> param = null)
        {
            if (param != null)
            {
                param.Add("LEVEL_INDEX_NAME", $"Level_index-{param["index"]}_name-{param["name"]}");
            }
            LogEvent("OpenRareChestFirstTime", param);
        }

        public override void OpenGoldChestFirstTime(Dictionary<string, object> param = null)
        {
            if (param != null)
            {
                param.Add("LEVEL_INDEX_NAME", $"Level_index-{param["index"]}_name-{param["name"]}");
            }
            LogEvent("OpenGoldChestFirstTime", param);
        }

        public override void UpgradeHeroFirstTime(Dictionary<string, object> param = null)
        {
            if (param != null)
            {
                param.Add("LEVEL_INDEX_NAME", $"Level_index-{param["index"]}_name-{param["name"]}");
            }
            LogEvent("UpgradeHeroFirstTime", param);
        }

        public override void ClickBattlePass(Dictionary<string, object> param = null)
        {
            if (param != null)
            {
                param.Add("LEVEL_INDEX_NAME", $"Level_index-{param["index"]}_name-{param["name"]}");
            }
            LogEvent("ClickBattlePass", param);
        }

        public override void TakeQuickAfk()
        {
            LogEvent("TAKE_QUICK_AFK");
        }

        public override void TakeAllDailyBattlePass()
        {
            LogEvent("TAKE_ALL_DAILY_BATTLE_PASS_MISSION");
        }

        public override void TakeAllWeekyBattlePass()
        {
            LogEvent("TAKE_ALL_WEEKLY_BATTLE_PASS_MISSION");
        }

        public override void UnlockLevelBattlePass()
        {
            LogEvent("UNLOCK_NEW_LEVEL_BATTLEPASS");
        }

        public override void EvoleHero()
        {
            LogEvent("EVOLVE_HERO");
        }

        public override void UpgradeHero()
        {
            LogEvent("UPGRADE_HERO");
        }

        public override void UpgradeArtifact()
        {
            LogEvent("UPGRADE_ARTIFACT");
        }

        public override void TakeDiamond()
        {
            LogEvent("ADS_GEM_PACK");
        }

        public override void TakeCoin()
        {
            LogEvent("ADS_COIN_PACK");
        }

        public override void TakeAllDailyMission()
        {
            LogEvent("TAKE_ALL_DAILY_MISSION");
        }

        public override void TakeAllWeeklyMisson()
        {
            LogEvent("TAKE_ALL_WEEKLY_MISSION");
        }

        public override void BuyBattlePass(Dictionary<string, object> param = null)
        {
             if (param != null)
            {
                param.Add("LEVEL_INDEX_NAME", $"Level_index-{param["index"]}_name-{param["name"]}");
            }
            LogEvent("BuyBattlePass", param);
        }
    }
}
#endif