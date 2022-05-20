using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.Purchasing;


public class Store : MonoBehaviour, IStoreListener
{
	public Button[] product;
	public Text[] productMoney;
	public bool[] BuyProduct;
	public int[] GetMoney;

	private static IStoreController m_StoreController;          // The Unity Purchasing system.
	private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.

	public static string kProductID1000coin = "com.serafim.keybo.walking_grandma.1000coins";   
	public static string kProductID3300coin = "com.serafim.keybo.walking_grandma.3300coins";
	public static string kProductID6000coin = "com.serafim.keybo.walking_grandma.6000coins"; 
	public static string kProductID15000coin = "com.serafim.keybo.walking_grandma.15000coins";
	public static string kProductID36000coin = "com.serafim.keybo.walking_grandma.36000coins"; 
	public static string kProductID60000coin = "com.serafim.keybo.walking_grandma.60000coins"; 

	void Start () {
		for (int i = 0; i < 6; i++) {
			productMoney [i].text = GetMoney [i].ToString ();
		}
		if (m_StoreController == null)
		{
			InitializePurchasing();
		}
	}

	private bool IsInitialized()
	{
		return m_StoreController != null && m_StoreExtensionProvider != null;
	}

	public void InitializePurchasing() 
	{
		if (IsInitialized())
		{
			return;
		}

		var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

		builder.AddProduct(kProductID1000coin, ProductType.Consumable);
		builder.AddProduct(kProductID3300coin, ProductType.Consumable);
		builder.AddProduct(kProductID6000coin, ProductType.Consumable);
		builder.AddProduct(kProductID15000coin, ProductType.Consumable);
		builder.AddProduct(kProductID36000coin, ProductType.Consumable);
		builder.AddProduct(kProductID60000coin, ProductType.Consumable);

		UnityPurchasing.Initialize(this, builder);
	}

	void Update () 
	{
		for (int i = 0; i < 6; i++) 
		{
			if (BuyProduct [i] == true) 
			{
				Score.money = Score.money + GetMoney [i];
				BuyProduct [i] = false;
			}
		}
	}

	public void Buy(int i)
	{
		switch (i) {
		case 0:	
			BuyProductID (kProductID1000coin);
			break;
		case 1:
			BuyProductID (kProductID3300coin);
			break;
		case 2:
			BuyProductID (kProductID6000coin);
			break;
		case 3:
			BuyProductID (kProductID15000coin);
			break;
		case 4:
			BuyProductID (kProductID36000coin);
			break;
		case 5:
			BuyProductID (kProductID60000coin);
			break;
		default:
			break;
		}
	}

	void BuyProductID(string productId)
	{
		if (IsInitialized())
		{
			Product product = m_StoreController.products.WithID(productId);

			if (product != null && product.availableToPurchase)
			{
				Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
				KeyboConnectInterface.Log (string.Format ("Purchasing product asychronously: '{0}'", product.definition.id));
				m_StoreController.InitiatePurchase(product);
			}
			else
			{
				Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
				KeyboConnectInterface.Log ("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
			}
		}
		else
		{
			Debug.Log("BuyProductID FAIL. Not initialized.");
			KeyboConnectInterface.Log ("BuyProductID FAIL. Not initialized.");
		}
	}

	public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
	{
		// Purchasing has succeeded initializing. Collect our Purchasing references.
		Debug.Log("OnInitialized: PASS");

		// Overall Purchasing system, configured with products for this application.
		m_StoreController = controller;
		// Store specific subsystem, for accessing device-specific store features.
		m_StoreExtensionProvider = extensions;
	}

	public void OnInitializeFailed(InitializationFailureReason error)
	{
		// Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
		Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
	}

	public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
	{
		if (String.Equals (args.purchasedProduct.definition.id, kProductID1000coin, StringComparison.Ordinal)) {
			Debug.Log (string.Format ("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
			KeyboConnectInterface.Log (string.Format ("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
			BuyProduct [0] = true;
		} else if (String.Equals (args.purchasedProduct.definition.id, kProductID3300coin, StringComparison.Ordinal)) {
			Debug.Log (string.Format ("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
			KeyboConnectInterface.Log (string.Format ("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
			BuyProduct [1] = true;
		} else if (String.Equals (args.purchasedProduct.definition.id, kProductID6000coin, StringComparison.Ordinal)) {
			Debug.Log (string.Format ("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
			KeyboConnectInterface.Log (string.Format ("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
			BuyProduct [2] = true;
		} else if (String.Equals (args.purchasedProduct.definition.id, kProductID15000coin, StringComparison.Ordinal)) {
			Debug.Log (string.Format ("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
			KeyboConnectInterface.Log (string.Format ("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
			BuyProduct [3] = true;
		} else if (String.Equals (args.purchasedProduct.definition.id, kProductID36000coin, StringComparison.Ordinal)) {
			Debug.Log (string.Format ("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
			KeyboConnectInterface.Log (string.Format ("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
			BuyProduct [4] = true;
		} else if (String.Equals (args.purchasedProduct.definition.id, kProductID60000coin, StringComparison.Ordinal)) {
			Debug.Log (string.Format ("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
			KeyboConnectInterface.Log (string.Format ("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
			BuyProduct [5] = true;
		} else {
			Debug.Log (string.Format ("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
			KeyboConnectInterface.Log (string.Format ("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
		}
		return PurchaseProcessingResult.Complete;
	}
	public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
	{
		Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
	}
}