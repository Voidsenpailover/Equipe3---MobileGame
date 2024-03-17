using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using UnityEngine;

public class GooglePlayManager : MonoBehaviour
{
    private void Awake()
    {
        PlayGamesPlatform.Activate();
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }

    public void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    public static void ProcessAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            // Continue with Play Games Services
        }
        else
        {
            // Disable your integration with Play Games Services or show a login button
            // to ask users to sign-in. Clicking it should call
            // PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication).
        }
    }

    public bool DoGrandAchievement(string _achievement)
    {
        bool result = false;
        Social.ReportProgress(_achievement,
            100.00f,
            (bool success) =>
            {
                if (success)
                {
                    result = success;
                }
                else
                {
                    result = success;
                }
            });
        return result;
    }
    
    public void ShowAchievementUI()
    {
        Social.ShowAchievementsUI();
    }

    public void AchievementAmongUS()
    {
        DoGrandAchievement(GPGSIds.achievement_parmi_nous);
    }

}
