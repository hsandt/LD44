using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using TMPro;

public enum TutorialKey
{
    Welcome,
    MorningDelivery,
    MorningDeliveryDone,
    ItemSetup,
    ItemSetupFront,
    ItemSetupPull,
    AutoSales,
}

public class Tutorial : MonoBehaviour
{
    [Tooltip("Tutorial Text")]
    public TextMeshProUGUI text;

    [SerializeField, TextArea, Tooltip("Tutorial strings")]
    private string[] tutorialStrings =
    {
        // Welcome
        "Welcome to your own family business! You must buy wholesale items and sell them to customers " +
        "in order to <s>get rich</s> survive!",
        // MorningDelivery
        "This is your inventory. " +
        "Every morning, you receive items you ordered from a wholesale trader the night before.",
        // MorningDeliveryDone
        "These new items are now yours. Click the <i>Set up items</i> button when you're ready.",
        // ItemSetup
        "Before the customers arrive, place items from your inventory to " +
        "free slots in the store by clicking on them.",
        // ItemSetupFront
        "Good! The item just behind the glass in the Front Display item. " +
        "Depending on its nature, it will attract certain types of customers, so try different ones!",
        // ItemSetupPull
        "You can also click on items placed on slots to pull them back to the inventory. " +
        "When you're ready, click on <i>Open Store</i>",
        // AutoSales
        "Well, the developer didn't have enough time to make the next phase, " +
        "so this is the end of this tutorial and of this demo. Thank you for playing!"
    };

    [Tooltip("For tutorial that require a click on Continue, the Continue button must be displayed")]
    public Button buttonContinue;
    
    [Tooltip("Playable during delivery to pause for explanations")]
    public PlayableDirector deliveryDirector;

    [Tooltip("Playable during item setup to pause for explanations")]
    public PlayableDirector itemSetupDirector;

    
    /* State */

    private TutorialKey currentTutorialKey;
        
    
    void Awake()
    {
        buttonContinue.gameObject.SetActive(false);
    }
    
    public void ShowTutorialText(TutorialKey key)
    {
        currentTutorialKey = key;
        string tutorialString = tutorialStrings[(int) key];
        text.text = tutorialString;

        OnShowTutorial(key);
    }

    public void HideSelfTutorial()
    {
        gameObject.SetActive(false);
    }

    private void ShowContinueButton()
    {
        buttonContinue.gameObject.SetActive(true);
    }
    
    private void HideContinueButton()
    {
        buttonContinue.gameObject.SetActive(false);
    }
    
    /// Continue current tutorial. Called on Button click.
    /// Only for tutorials that are not completed via a game action.
    /// Must have a key after the current one.
    public void Continue()
    {
        HideContinueButton();
        OnContinueTutorialFrom(currentTutorialKey);
        HideSelfTutorial();
    }
    
    private void OnShowTutorial(TutorialKey key)
    {
        switch (key)
        {
            case TutorialKey.Welcome:
                deliveryDirector.Pause();
                ShowContinueButton();
                break;
            case TutorialKey.MorningDelivery:
                deliveryDirector.Pause();
                ShowContinueButton();
                break;
            case TutorialKey.MorningDeliveryDone:
                break;
            case TutorialKey.ItemSetup:
                break;
            case TutorialKey.ItemSetupFront:
                break;
            case TutorialKey.ItemSetupPull:
                break;
            case TutorialKey.AutoSales:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(key), key, null);
        }
    }
    
    private void OnContinueTutorialFrom(TutorialKey key)
    {
        switch (key)
        {
            case TutorialKey.Welcome:
                deliveryDirector.Resume();
                break;
            case TutorialKey.MorningDelivery:
                deliveryDirector.Resume();
                break;
            case TutorialKey.MorningDeliveryDone:
                break;
            case TutorialKey.ItemSetup:
                break;
            case TutorialKey.ItemSetupFront:
                break;
            case TutorialKey.ItemSetupPull:
                break;
            case TutorialKey.AutoSales:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(key), key, null);
        }
    }
}
