using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAPManager : MonoBehaviour, IStoreListener
{
    public static IAPManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            Debug.LogError("More than one IAPManager exists");
            return;
        }

        instance = this;
    }

    // Private Variables
    private static IStoreController storeController;
    private static IExtensionProvider storeExtensionProvider;

    public static string kRemoveAds = "removeads";

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        if (storeController == null)
        {
            InitializePurchasing();
        }
    }

    public void InitializePurchasing()
    {
        if (IsInitialized())
            return;

        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        builder.AddProduct(kRemoveAds, ProductType.Consumable);

        UnityPurchasing.Initialize(this, builder);
    }

    public void BuyProductID(string productId)
    {
        if (IsInitialized())
        {
            Product product = storeController.products.WithID(productId);
            if (product != null && product.availableToPurchase)
            {
                Debug.Log("Purchasing Product");
                storeController.InitiatePurchase(product);
            }
            else
            {
                Debug.Log("Product either is not found or not available for purchase");
            }
        }
        else
        {
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        if (string.Equals(args.purchasedProduct.definition.id, kRemoveAds, System.StringComparison.Ordinal))
        {
            Debug.Log("Purchasing Product");

            PlayerPrefs.SetInt("noAdsPurchased", 1);
            //mainMenuScript.noAdsButton.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Unrecognized Product");
        }
        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }


    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("OnInitialized: PASS");
        storeController = controller;
        storeExtensionProvider = extensions;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }

    private bool IsInitialized()
    {
        return storeController != null && storeExtensionProvider != null;
    }

}
