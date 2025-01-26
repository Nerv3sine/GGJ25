using UnityEngine;
using UnityEngine.SceneManagement;

public class UIEvents : MonoBehaviour
{
    [SerializeField] int MMI;

    [SerializeField] GameObject[] mainMenuAssets;
    [SerializeField] GameObject[] creditsAssets;
    [SerializeField] GameObject[] optionsMenuObjects;


    public void ExitTheGame()
    {
        Application.Quit();
    }

    public void StartTheGame()
    {
        
        
        SceneManager.LoadScene(MMI);
    }

    public void EnableMainMenuAssetsFromOptions( )
    {
        foreach (GameObject menuObject in optionsMenuObjects)
        {
            menuObject.SetActive(false);

        }
        foreach (GameObject menuObject in mainMenuAssets)
        {
            menuObject.SetActive(true);
        }
    }
    public void EnableOptionsMenuAssets()
    {
        foreach (GameObject menuObject in mainMenuAssets)
        {
            menuObject.SetActive(false);

        }
        foreach (GameObject menuObject in optionsMenuObjects)
        {
            menuObject.SetActive(true);
        }
    }
    public void EnableCreditsMenuAssets()
    {
        foreach (GameObject menuObject in mainMenuAssets)
        {
            menuObject.SetActive(false);

        }
        foreach (GameObject menuObject in creditsAssets)
        {
            menuObject.SetActive(true);
        }
    }
    public void EnableMainMenuAssetsfromCredits()
    {
        foreach (GameObject menuObject in creditsAssets)
        {
            menuObject.SetActive(false);

        }
        foreach (GameObject menuObject in mainMenuAssets)
        {
            menuObject.SetActive(true);
        }
    }
}
