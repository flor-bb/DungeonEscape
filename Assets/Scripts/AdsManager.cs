using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsListener

{

    string gameId = "4434027"; // get this from your unity dashboard

    string placement = "Rewarded_Android";

    bool testMode = true;



    void Start()

    {

        Advertisement.AddListener(this);

        Advertisement.Initialize(gameId, testMode);

    }



    public void ShowAd()

    {

        Advertisement.Show(placement);

    }



    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)

    {

        switch (showResult)

        {

            case ShowResult.Finished:

                GameManager.Instance.player.AddGems(100);
                UIManager.Instance.OpenShop(GameManager.Instance.player.diamonds);
                Debug.Log("100 gems awarded to you");

                break;

            case ShowResult.Skipped:

                Debug.Log("You skipped ad no gems awarded to you");

                break;

            case ShowResult.Failed:

                Debug.Log("Ad video failed to play");

                break;

        }

    }



    public void OnUnityAdsDidError(string message)

    {



    }



    public void OnUnityAdsDidStart(string placementId)

    {



    }



    public void OnUnityAdsReady(string placementId)

    {



    }



} // end of AdsManager