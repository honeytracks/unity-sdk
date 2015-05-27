using UnityEngine;
using System.Collections;
using HoneyTracks;
using System.Collections.Generic;
using System.Globalization;
using HoneyTracks.Helper;

namespace HoneyTracks
{
    /// <summary>
    /// Contains all the tracking calls that are possible
    /// </summary>
    public class Tracking : ITracking
    {
        private Transport transport;
        private Config config;

	    #region TrackFeatureUsage
	    /// <summary>
	    /// Tracks the usage of a single game feature (custom event) , e.g. make 
	    /// a game, fight a battle, start a construction, skill your character, 
	    /// etc.
	    /// </summary>
	    /// <param name="featureType">Defines the main type of the feature, e.g.: 
	    /// Fight, Building, Training, Quest, etc.</param>
	    /// <param name="featureSubType">Specifies the feature, e.g.: 
	    /// PvP, Headquarter, Strength, Tutorial, etc.</param>
	    /// <param name="featureSubSubType">Specifies the feature in a third 
	    /// level, e.g.: Won, Lost, etc.</param>
	    /// <param name="quantity">Defines the amount of usages</param>
	    public void TrackFeatureUsage(string featureType, string featureSubType,
		    string featureSubSubType, int quantity)
	    {
		    TrackFeatureUsage(featureType, featureSubType, featureSubSubType,
			    new GameCurrency(), quantity);
	    } // TrackFeatureUsage(featureType, featureSubType, featureSubSubType)

	    /// <summary>
	    /// Tracks the usage of a single game feature (custom event) , e.g. make 
	    /// a game, fight a battle, start a construction, skill your character, 
	    /// etc.
	    /// </summary>
	    /// <param name="featureType">Defines the main type of the feature, e.g.: 
	    /// Fight, Building, Training, Quest, etc.</param>
	    /// <param name="featureSubType">Specifies the feature, e.g.: 
	    /// PvP, Headquarter, Strength, Tutorial, etc.</param>
	    /// <param name="featureSubSubType">Specifies the feature in a third 
	    /// level, e.g.: Won, Lost, etc.</param>
	    /// <param name="gameCurrency">Used game curency as array depending on 
	    /// your game currencies, e.g.: 
	    /// <code>Packets[n][GameCurrency]={'gold':5}</code>
	    /// </param>
	    /// <param name="quantity">Defines the amount of usages</param>
	    public void TrackFeatureUsage(string featureType, string featureSubType,
		    string featureSubSubType, GameCurrency gameCurrency, int quantity)
	    {
		    List<KeyValuePair<string, string>> eventData = 
			    new List<KeyValuePair<string, string>>();

		    eventData.Add(new KeyValuePair<string, string>("FeatureType", 
			    featureType));
		    eventData.Add(new KeyValuePair<string, string>("FeatureSubType",
			    featureSubType));
		    eventData.Add(new KeyValuePair<string, string>("FeatureSubSubType",
			    featureSubSubType));
		    eventData.Add(new KeyValuePair<string, string>("GameCurrency", 
			    gameCurrency != null ? gameCurrency.ToJsonString(): ""));
		    eventData.Add(new KeyValuePair<string, string>("Quantity",
			    quantity.ToString()));

		    AddEvent("Feature::Usage", eventData);
	    } // TrackFeatureUsage(featureType, featureSubType, featureSubSubType)
	    #endregion

	    #region TrackClick
	    /// <summary>
	    /// Tracks a user click. This method is used for tracking user clicks on 
	    /// ads and will  be combined with marketing configured marketing costs. 
	    /// This method requires a unique identifier for the user, which has no 
	    /// user id set. This identifier has to be available within the 
	    /// TrackSignup-call to ensure that the click is assigned to the new 
	    /// user. Each call will create a click and is used for marketing costs 
	    /// calculations (e.g. for CPC campaigns).
	    /// </summary>
	    /// <param name="uniqueCustomerClickToken">An unique token which 
	    /// identifies the user till and within the TrackSignup()-call.</param>
	    /// <param name="marketingIdentifier">Marketing identifier token, this 
	    /// token is used to identify the corresponding ad, campaign and 
	    /// marketing partner</param>
	    /// <param name="landingPageId">An unique id for your different landing 
	    /// pages if available. You should set this value if you have several 
	    /// landing pages and want to get the different success rates.</param>
	    public void TrackClick(string uniqueCustomerClickToken,
		    string marketingIdentifier, string landingPageId)
	    {
		    List<KeyValuePair<string, string>> eventData = 
			    new List<KeyValuePair<string, string>>();

		    eventData.Add(new KeyValuePair<string, string>(
			    "UniqueCustomerClickToken", uniqueCustomerClickToken));
		    eventData.Add(new KeyValuePair<string, string>("MarketingIdentifier", 
			    marketingIdentifier));
		    eventData.Add(new KeyValuePair<string, string>("LandingPageId", 
			    landingPageId));

		    AddEvent("User::Click", eventData);
	    } // TrackClick(uniqueCustomerClickToken, marketingIdentifier, landingPa)
		
	    public void TrackClick(string uniqueCustomerClickToken,
		    string marketingIdentifier)
	    {
		    TrackClick (uniqueCustomerClickToken, marketingIdentifier, "");
	    } // TrackClick(uniqueCustomerClickToken, marketingIdentifier)

	    public void TrackClick(string uniqueCustomerClickToken)
	    {
		    TrackClick (uniqueCustomerClickToken, "", "");
	    } // TrackClick(uniqueCustomerClickToken)
	    #endregion

	    #region TrackLevelup
	    /// <summary>
	    /// Tracks the level up of an user. If your game has no user levels you 
	    /// can use any other metric / value, which tracks the progression of a 
	    /// user. Being able to analyze in-game data by levels or the progression 
	    /// of users  is very important for analytics purposes. Amongst other 
	    /// things this will allow you to optimize game-design for longer user 
	    /// retention.
	    /// </summary>
	    /// <example>A possible solution for a soccer game could be the league of 
	    /// the user, for strategic build & raid games could be the number of 
	    /// bases/planets or tech tree activations.
	    /// </example>
	    /// <param name="level">Defines the user level, should be called after a 
	    /// signup if the level of a new user starts not with 0 </param>
	    public void TrackLevelup(int level)
	    {
		    if (level > 0)
		    {
			    List<KeyValuePair<string, string>> eventData = 
				    new List<KeyValuePair<string, string>>();

			    eventData.Add(new KeyValuePair<string, string>("Level", 
				    level.ToString()));

			    AddEvent("User::Levelup", eventData);
		    } // if
	    } // TrackLevelup(level)
	    #endregion

	    #region TrackLogin
	    /// <summary>
	    /// Tracks the user login.
	    /// </summary>
	    public void TrackLogin()
	    {
		    AddEvent("User::Login");
	    } // TrackLogin()
	    #endregion

	    #region TrackLogout
	    /// <summary>
	    /// Tracks the user logout.
	    /// </summary>
	    public void TrackLogout()
	    {
		    AddEvent("User::Logout");
	    } // TrackLogout()
	    #endregion

	    #region TackUserProfile
	    #region TrackUserGender
	    /// <summary>
	    /// Tracks the gender of an user.
	    /// </summary>
	    /// <param name="gender">Defines the gender oft he user, if known, valid 
	    /// values are: female, male </param>
	    public void TrackUserGender(string gender)
	    {
		    if (gender == "female" ||
			    gender == "male")
		    {
			    TrackUserProfile("Gender", gender);
		    }
	    } // TrackUserGender(gender)
	    #endregion

	    #region TrackUserBirthyear
	    /// <summary>
	    /// Tracks the birthyear of an user.
	    /// </summary>
	    /// <param name="birthyear">Define s the birthyear of the user, e.g.: 
	    /// 1983, 1973, 1969</param>
	    public void TrackUserBirthyear(int birthyear)
	    {
		    TrackUserProfile("Birthyear", birthyear.ToString());
	    } // TrackUserBirthyear(birthyear)
	    #endregion

	    #region TrackUserCustomStaticClassification
	    /// <summary>
	    /// Track user custom static classification.
	    /// </summary>
	    /// <param name="userCustomStaticClassification">Defines a custom user 
	    /// classification, e.g. the player class, e.g. Elf, Tank, etc.. </param>
	    public void TrackUserCustomStaticClassification(string
		    userCustomStaticClassification)
	    {
		    TrackUserProfile("CustomStaticClassification",
			    userCustomStaticClassification);
	    } // TrackUserCustomStaticClassification(userCustomStaticClassification)
	    #endregion

	    #region TrackUserProfile (private)
	    /// <summary>
	    /// Tracks the user year of birth or gender if known or custom static 
	    /// classification. 
	    /// </summary>
	    /// <param name="type">Defines the type of user profile information: 
	    /// Gender, Birthyear, CustomStaticClassification</param>
	    /// <param name="value">Value as string</param>
	    private void TrackUserProfile(string type, string value)
	    {
		    List<KeyValuePair<string, string>> eventData =
				    new List<KeyValuePair<string, string>>();

		    eventData.Add(new KeyValuePair<string, string>("Type", type));
		    eventData.Add(new KeyValuePair<string, string>("Value", value));

		    AddEvent("User::Profile", eventData);
	    } // TrackUserProfile(type, value)
	    #endregion
	    #endregion

	    #region TrackSignup
	    /// <summary>
	    /// Tracks the user signup, if available with the corresponding marketing 
	    /// identifier token.
	    /// </summary>
	    public void TrackSignup()
	    {
		    TrackSignup("", "");
	    } // TrackSignup()

	    /// <summary>
	    /// Tracks the user signup, if available with the corresponding marketing 
	    /// identifier token.
	    /// </summary>
	    /// <param name="marketingIdentifier">Marketing identifier token, this 
	    /// token is used to identify the corresponding ad, campaign and 
	    /// marketing.</param>
	    public void TrackSignup(string marketingIdentifier)
	    {
		    TrackSignup(marketingIdentifier, "");
	    } // TrackSignup(marketingIdentifier)

	    /// <summary>
	    /// Tracks the user signup, if available with the corresponding marketing 
	    /// identifier token.
	    /// </summary>
	    /// <param name="marketingIdentifier">Marketing identifier token, this 
	    /// token is used to identify the corresponding ad, campaign and 
	    /// marketing.</param>
	    /// <param name="landingPage">An unique id for your different landing 
	    /// pages if available. You should set this value if you have several 
	    /// landing pages and want to get the different success rates.</param>
	    public void TrackSignup(string marketingIdentifier, string landingPage)
	    {
		    TrackSignup (marketingIdentifier, "", "", "", "", landingPage);
	    } // TrackSignup(marketingIdentifier, landingPage)

	    /// <summary>
	    /// Tracks the user signup, if available with the corresponding marketing 
	    /// identifier token.
	    /// </summary>
	    /// <param name="marketingIdentifier">Marketing identifier token, this 
	    /// token is used to identify the corresponding ad, campaign and 
	    /// marketing.</param>
	    /// <param name="marketingPartner">Marketing identifier token, this 
	    /// token is used to identify the corresponding ad, campaign and 
	    /// marketing.</param>
	    /// <param name="marketingCampaign">Marketing identifier token, this 
	    /// token is used to identify the corresponding ad, campaign and 
	    /// marketing.</param>
	    /// <param name="marketingAd">Marketing identifier token, this 
	    /// token is used to identify the corresponding ad, campaign and 
	    /// marketing.</param>
	    /// <param name="marketingKeyword">Marketing identifier token, this 
	    /// token is used to identify the corresponding ad, campaign and 
	    /// marketing.</param>
	    /// <param name="landingPage">An unique id for your different landing 
	    /// pages if available. You should set this value if you have several 
	    /// landing pages and want to get the different success rates.</param>
	    public void TrackSignup(
		    string marketingIdentifier,
		    string marketingPartner,
		    string marketingCampaign,
		    string marketingAd,
		    string marketingKeyword,
		    string landingPage
	    ) {
		    List<KeyValuePair<string, string>> eventData =
				    new List<KeyValuePair<string, string>>();

		    eventData.Add(new KeyValuePair<string, string>("MarketingIdentifier", 
			    marketingIdentifier));
		    eventData.Add(new KeyValuePair<string, string>("PartnerName", 
			    marketingPartner));
		    eventData.Add(new KeyValuePair<string, string>("CampaignName", 
			    marketingCampaign));
		    eventData.Add(new KeyValuePair<string, string>("AdName", 
			    marketingAd));
		    eventData.Add(new KeyValuePair<string, string>("Keyword", 
			    marketingKeyword));
		    eventData.Add(new KeyValuePair<string, string>("LandingPage", 
			    landingPage));
		
		    AddEvent("User::Signup", eventData);
	    } // TrackSignup(marketingIdentifier, landingPage)



	    /// <summary>
	    /// Tracks the user signup, if available with the corresponding marketing 
	    /// identifier token.
	    /// </summary>
	    /// <param name="uniqueCustomerClickToken">An unique token which 
	    /// identifies the user till and within the TrackSignup()-call.</param>
	    /// <param name="marketingIdentifier">Marketing identifier token, this 
	    /// token is used to identify the corresponding ad, campaign and 
	    /// marketing.</param>
	    /// <param name="marketingPartner">Marketing identifier token, this 
	    /// token is used to identify the corresponding ad, campaign and 
	    /// marketing.</param>
	    /// <param name="marketingCampaign">Marketing identifier token, this 
	    /// token is used to identify the corresponding ad, campaign and 
	    /// marketing.</param>
	    /// <param name="marketingAd">Marketing identifier token, this 
	    /// token is used to identify the corresponding ad, campaign and 
	    /// marketing.</param>
	    /// <param name="marketingKeyword">Marketing identifier token, this 
	    /// token is used to identify the corresponding ad, campaign and 
	    /// marketing.</param>
	    /// <param name="landingPage">An unique id for your different landing 
	    /// pages if available. You should set this value if you have several 
	    /// landing pages and want to get the different success rates.</param>
	    public void TrackSignup(
		    string uniqueCustomerClickToken,
		    string marketingIdentifier,
		    string marketingPartner,
		    string marketingCampaign,
		    string marketingAd,
		    string marketingKeyword,
		    string landingPage
		    ) {
		    List<KeyValuePair<string, string>> eventData =
			    new List<KeyValuePair<string, string>>();
			
		    eventData.Add(new KeyValuePair<string, string>("UniqueCustomerClickToken",
			                                                uniqueCustomerClickToken));
		    eventData.Add(new KeyValuePair<string, string>("MarketingIdentifier", 
			                                                marketingIdentifier));
		    eventData.Add(new KeyValuePair<string, string>("PartnerName", 
			                                                marketingPartner));
		    eventData.Add(new KeyValuePair<string, string>("CampaignName", 
			                                                marketingCampaign));
		    eventData.Add(new KeyValuePair<string, string>("AdName", 
			                                                marketingAd));
		    eventData.Add(new KeyValuePair<string, string>("Keyword", 
			                                                marketingKeyword));
		    eventData.Add(new KeyValuePair<string, string>("LandingPage", 
			                                                landingPage));
			
		    AddEvent("User::Signup", eventData);
	    } // TrackSignup(marketingIdentifier, landingPage)
	    #endregion

	    #region TrackViralityInvitation
	    /// <summary>
	    /// Tracks an invitation sent by an user. To analyze the success of 
	    /// different invitation types and messages the invitation type and a 
	    /// unique message token are neccessary. Invitation types itself defines 
	    /// for example a neighborhood invitation or gifting. Your application 
	    /// has to ensure that the target link of an invitation contains the 
	    /// InviteType, InviteMessageToken and the unique customer identifier 
	    /// token to get valid analysises of success rates grouped by levels and 
	    /// marketing cohorts.
	    /// </summary>
	    /// <param name="inviteType">Defines the type of invitation, e.g.: 
	    /// Neighborhood, Gifting, Achievement Wall Message, etc.</param>
	    /// <param name="inviteMessageToken">Defines the message, this should be 
	    /// unique token for  each message, e.g.: NH_INVITATION_MSG_1, 
	    /// GIFT_SENT_MSG_1, LEVELUP_WALL_MSG_1</param>
	    /// <param name="quantity">Defines the number of invitations sent</param>
	    public void TrackViralityInvitation(string inviteType,
		    string inviteMessageToken, int quantity)
	    {
		    List<KeyValuePair<string, string>> eventData =
				    new List<KeyValuePair<string, string>>();

		    eventData.Add(new KeyValuePair<string, string>("InviteType", 
			    inviteType));
		    eventData.Add(new KeyValuePair<string, string>("InviteMessageToken",
			    inviteMessageToken));
		    eventData.Add(new KeyValuePair<string, string>("Quantity", 
			    quantity.ToString()));

		    AddEvent("Virality::Invitation", eventData);
	    } // TrackViralityInvitation(inviteType, inviteMessageToken, quantity)
	    #endregion

	    #region TrackViralityInviteAcceptance
	    /// <summary>
	    /// Tracks the acceptance of an invitation. Your application has to 
	    /// ensure that the InvitationType, the InviteMessageToken and the unique 
	    /// customer identifier token is available after a user accepts an 
	    /// invitation, e.g. you have to ensure that your links which the users 
	    /// clicks has all the necessary. This method should be called after a 
	    /// signup if the corresponding invitation information are available 
	    /// within the first request to your landing.
	    /// </summary>
	    /// <param name="inviteType">Defines the type of invitation, e.g.: 
	    /// Neighborhood, Gifting, Achievement Wall Message, etc.</param>
	    /// <param name="inviteMessageToken">Defines the message, this should be 
	    /// unique token for  each message, e.g.: NH_INVITATION_MSG_1, 
	    /// GIFT_SENT_MSG_1, LEVELUP_WALL_MSG_1</param>
	    /// <param name="quantity">Defines the unique customer identifier token 
	    /// who sent the accepted invitation</param>
	    public void TrackViralityInviteAcceptance(string inviteType,
		    string inviteMessageToken, string sourceUniqueCustomerIdentifier)
	    {
		    List<KeyValuePair<string, string>> eventData =
				    new List<KeyValuePair<string, string>>();

		    eventData.Add(new KeyValuePair<string, string>("InviteType",
			    inviteType));
		    eventData.Add(new KeyValuePair<string, string>("InviteMessageToken",
			    inviteMessageToken));
		    eventData.Add(new KeyValuePair<string, string>("SourceUniqueCustomerIdentifier",
			    sourceUniqueCustomerIdentifier));

		    AddEvent("Virality::Invitation::Acceptance", eventData);
	    } // TrackViralityInviteAcceptance(inviteType, inviteMessageToken, sourc)
	    #endregion

	    #region TrackVirtualCurrenciesChargeback
	    /// <summary>
	    /// Tracks a chargeback of a virtual currency purchase, e.g. if a credit 
	    /// card transaction failed. The revenue and payout amounts have to be 
	    /// always positive.
	    /// </summary>
	    /// <param name="virtualCurrencyAmount">Defines the amount of virtual 
	    /// currency which were purchased</param>
	    /// <param name="virtualCurrencyName">The name of the virtual currency 
	    /// for the purchase</param>
	    /// <param name="paymentType">Defines the payment method which where 
	    /// used</param>
	    /// <param name="revenue">Defines the revenue amount for the 
	    /// purchase</param>
	    /// <param name="revenueCurrency">Defines the revenue currency for the 
	    /// purchase, have to be the ISO 4217 currency code, e.g.: EUR, GBP, USD, 
	    /// TRY, etc.</param>
	    /// <param name="payout">Defines the payout amount for the purchase. The 
	    /// payout amount ist he revenue amount without payment method 
	    /// transaction costs</param>
	    /// <param name="payoutCurrency">Defines the payout currency for the 
	    /// purchase, have to be the ISO 4217 currency code, e.g.: EUR, GBP, USD, 
	    /// TRY, etc.</param>
	    public void TrackVirtualCurrenciesChargeback (double virtualCurrencyAmount, string virtualCurrencyName,
		    string paymentType, double revenue, string revenueCurrency, 
		    double payout, string payoutCurrency)
	    {
		    List<KeyValuePair<string, string>> eventData =
				    new List<KeyValuePair<string, string>> ();

		    eventData.Add (new KeyValuePair<string, string> ("VirtualCurrencyAmount",
			    virtualCurrencyAmount.ToString (
			    CultureInfo.InvariantCulture.NumberFormat)
		    )
		    );
		    if (virtualCurrencyName != null) {
			    eventData.Add (new KeyValuePair<string, string> ("VirtualCurrencyName",
				    virtualCurrencyName)
			    );
		    }

		    eventData.Add(new KeyValuePair<string, string>("PaymentType",
			    paymentType));
		    eventData.Add(new KeyValuePair<string, string>("Revenue",
			    revenue.ToString(CultureInfo.InvariantCulture.NumberFormat)));
		    eventData.Add(new KeyValuePair<string, string>("RevenueCurrency",
			    revenueCurrency));
		    eventData.Add(new KeyValuePair<string, string>("Payout",
			    payout.ToString(CultureInfo.InvariantCulture.NumberFormat)));
		    eventData.Add(new KeyValuePair<string, string>("PayoutCurrency",
			    payoutCurrency));

		    AddEvent("VirtualCurrencies::Chargeback", eventData);
	    } // TrackVirtualCurrenciesChargeback(quantity, paymentType, value)
	    #endregion

	    #region TrackVirtualCurrencyPurchase
	    /// <summary>
	    /// Tracks a purchase of virtual currency. Your payment system has to 
	    /// ensure that the neccessary information is submitted back to your 
	    /// application.
	    /// </summary>
	    /// <param name="quantity">Defines the amount of virtual 
	    /// currency which were purchased</param>
	    /// <param name="virtualCurrencyName">The name of the virtual currency 
	    /// for the purchase</param>
	    /// <param name="paymentType">Defines the payment method which where 
	    /// used</param>
	    /// <param name="value">Defines the revenue amount for the 
	    /// purchase</param>
	    /// <param name="currency">Defines the revenue currency for the 
	    /// purchase, have to be the ISO 4217 currency code, e.g.: EUR, GBP, USD, 
	    /// TRY, etc.</param>
	    /// <param name="payout">Defines the payout amount for the purchase. The 
	    /// payout amount ist he revenue amount without payment method 
	    /// transaction costs</param>
	    /// <param name="payoutCurrency">Defines the payout currency for the 
	    /// purchase, have to be the ISO 4217 currency code, e.g.: EUR, GBP, USD, 
	    /// TRY, etc.</param>
	    public void TrackVirtualCurrencyPurchase(double virtualCurrencyAmount, string virtualCurrencyName,
		    string paymentType, double revenue, string revenueCurrency,
		    double payout, string payoutCurrency)
	    {
		    List<KeyValuePair<string, string>> eventData =
				    new List<KeyValuePair<string, string>>();

		    eventData.Add(new KeyValuePair<string, string>("VirtualCurrencyAmount",
			    virtualCurrencyAmount.ToString(
			    CultureInfo.InvariantCulture.NumberFormat)));
		    eventData.Add(new KeyValuePair<string, string>("PaymentType",
			    paymentType));
		    eventData.Add(new KeyValuePair<string, string>("Revenue",
			    revenue.ToString(CultureInfo.InvariantCulture.NumberFormat)));
		    eventData.Add(new KeyValuePair<string, string>("RevenueCurrency",
			    revenueCurrency));
		    eventData.Add(new KeyValuePair<string, string>("Payout",
			    payout.ToString(CultureInfo.InvariantCulture.NumberFormat)));
		    eventData.Add(new KeyValuePair<string, string>("PayoutCurrency",
			    payoutCurrency));

		    AddEvent("VirtualCurrencies::Buy", eventData);
	    } // TrackVirtualCurrencyPurchase(quantity, paymentType, value)
	    #endregion
		
	    #region TrackVirtualCurrencyChargeback
	    /// <summary>
	    /// Tracks a purchase of virtual currency. Your payment system has to 
	    /// ensure that the neccessary information is submitted back to your 
	    /// application.
	    /// </summary>
	    /// <param name="quantity">Defines the amount of virtual 
	    /// currency which were purchased</param>
	    /// <param name="virtualCurrencyName">The name of the virtual currency 
	    /// for the purchase</param>
	    /// <param name="paymentType">Defines the payment method which where 
	    /// used</param>
	    /// <param name="value">Defines the revenue amount for the 
	    /// purchase</param>
	    /// <param name="currency">Defines the revenue currency for the 
	    /// purchase, have to be the ISO 4217 currency code, e.g.: EUR, GBP, USD, 
	    /// TRY, etc.</param>
	    /// <param name="payout">Defines the payout amount for the purchase. The 
	    /// payout amount ist he revenue amount without payment method 
	    /// transaction costs</param>
	    /// <param name="payoutCurrency">Defines the payout currency for the 
	    /// purchase, have to be the ISO 4217 currency code, e.g.: EUR, GBP, USD, 
	    /// TRY, etc.</param>
	    public void TrackVirtualCurrencyChargeback(
		    double virtualCurrencyAmount,
		    string virtualCurrencyName,
		    string paymentType,
		    double revenue,
		    string revenueCurrency,
		    double payout,
		    string payoutCurrency
	    ) {
		    List<KeyValuePair<string, string>> eventData =
				    new List<KeyValuePair<string, string>>();

		    eventData.Add(new KeyValuePair<string, string>("VirtualCurrencyAmount",
			    virtualCurrencyAmount.ToString(
			    CultureInfo.InvariantCulture.NumberFormat)));
		    if (virtualCurrencyName != null) {
			    eventData.Add (new KeyValuePair<string, string> ("VirtualCurrencyName",
				    virtualCurrencyName)
			    );
		    }

		    eventData.Add(new KeyValuePair<string, string>("PaymentType",
			    paymentType));
		    eventData.Add(new KeyValuePair<string, string>("Revenue",
			    revenue.ToString(CultureInfo.InvariantCulture.NumberFormat)));
		    eventData.Add(new KeyValuePair<string, string>("RevenueCurrency",
			    revenueCurrency));
		    eventData.Add(new KeyValuePair<string, string>("Payout",
			    payout.ToString(CultureInfo.InvariantCulture.NumberFormat)));
		    eventData.Add(new KeyValuePair<string, string>("PayoutCurrency",
			    payoutCurrency));
		    eventData.Add(new KeyValuePair<string, string>("VirtualCurrencyName",
			    virtualCurrencyName));

		    AddEvent("VirtualCurrencies::Chargeback", eventData);
	    } // TrackVirtualCurrencyPurchase(quantity, paymentType, value)
	    #endregion

	    #region TrackVirtualGoodsItemPurchase
	    /// <summary>
	    /// Tracks the purchase of an item, e.g. a sword, a pant, a new building, 
	    /// larger piece of land. Of course this depends on the types of virtual 
	    /// goods / items, which can be purchased in your game.
	    /// </summary>
	    /// <param name="itemType">Defines the main type of the item, e.g.: Sword, 
	    /// Pant, Apartment building, etc.</param>
	    /// <param name="item">Specifies the item, the detailed item as array, 
	    /// e.g.: <code>Packets[n][Item]={'UniqueId':'SWORD_1','ImageUrl':
	    /// 'http://… ','Name':'Sword of fear'}</code></param>
	    /// <param name="virtualCurrencyAmount">The amount of virtual currency 
	    /// spent for the purchase</param>
	    /// <param name="quantity">Defines the amount of usages</param>
	    /// <param name="isFreeAction">Defines if the action was for free, e.g. 
	    /// for special promotions</param>
	    public void TrackVirtualGoodsItemPurchase(string itemType, Item item,
		    double virtualCurrencyAmount, int quantity, bool isFreeAction)
	    {
		    TrackVirtualGoodsItemPurchase(itemType, item, virtualCurrencyAmount, null,
			    new GameCurrency(), quantity, isFreeAction);
	    } // TrackVirtualGoodsItemPurchase(itemType, item, virtualCurrencyAmount)

	    /// <summary>
	    /// Tracks the purchase of an item, e.g. a sword, a pant, a new building, 
	    /// larger piece of land. Of course this depends on the types of virtual 
	    /// goods / items, which can be purchased in your game.
	    /// </summary>
	    /// <param name="itemType">Defines the main type of the item, e.g.: Sword, 
	    /// Pant, Apartment building, etc.</param>
	    /// <param name="item">Specifies the item, the detailed item as array, 
	    /// e.g.: <code>Packets[n][Item]={'UniqueId':'SWORD_1','ImageUrl':
	    /// 'http://… ','Name':'Sword of fear'}</code></param>
	    /// <param name="virtualCurrencyAmount">The amount of virtual currency 
	    /// spent for the purchase</param>
	    /// <param name="virtualCurrencyName">The name of the virtual currency 
	    /// for the purchase</param>
	    /// <param name="gameCurrency">Used game curency as array depending on 
	    /// your game currencies, e.g.: <code>Packets[n][GameCurrency]={‘gold’:5}
	    /// </code></param>
	    /// <param name="quantity">Defines the amount of usages</param>
	    /// <param name="isFreeAction">Defines if the action was for free, e.g. 
	    /// for special promotions</param>
	    public void TrackVirtualGoodsItemPurchase(string itemType, Item item,
		    double virtualCurrencyAmount, string virtualCurrencyName, GameCurrency gameCurrency, int quantity,
		    bool isFreeAction)
	    {
		    List<KeyValuePair<string, string>> eventData =
				    new List<KeyValuePair<string, string>>();

		    eventData.Add(new KeyValuePair<string, string>("ItemType", itemType));
		    eventData.Add(new KeyValuePair<string, string>("Item", 
			    item != null ? item.ToJsonString() : ""));
		    eventData.Add(new KeyValuePair<string, string>("VirtualCurrencyAmount",
			    virtualCurrencyAmount.ToString(
			    CultureInfo.InvariantCulture.NumberFormat)));
		    if (virtualCurrencyName != null) {
			    eventData.Add (new KeyValuePair<string, string> ("VirtualCurrencyName",
				    virtualCurrencyName)
			    );
		    }
		    eventData.Add(new KeyValuePair<string, string>("GameCurrency",
			    gameCurrency != null ? gameCurrency.ToJsonString() : ""));
		    eventData.Add(new KeyValuePair<string, string>("Quantity",
			    quantity.ToString()));
		    eventData.Add(new KeyValuePair<string, string>("IsFreeAction",
			    isFreeAction == true ? "true" : "false"));

		    AddEvent("VirtualGoods::Item::Buy::" + itemType, eventData);
	    } // TrackVirtualGoodsItemPurchase(itemType, item, virtualCurrencyAmount)
	    #endregion

	    #region TrackVirtualGoodsFeaturePurchase
	    /// <summary>
	    /// Tracks a purchase of a virtual goods feature, e.g. reset a block time 
	    /// for the next fight, finish a construction without waiting, etc..
	    /// </summary>
	    /// <param name="featureType">Defines the main type of the feature, e.g.: 
	    /// Fight, Building, Training, etc.</param>
	    /// <param name="featureSubType">Specifies the feature, e.g.: Reset Block 
	    /// Time, Finish, Strength, etc.</param>
	    /// <param name="virtualCurrencyAmount">The amount of virtual currency 
	    /// spent for the purchase</param>
	    /// <param name="quantity">Defines the amount of usages</param>
	    /// <param name="isFreeAction">Defines if the action was for free, e.g. 
	    /// for  special promotions</param>
	    public void TrackVirtualGoodsFeaturePurchase(string featureType,
		    string featureSubType, double virtualCurrencyAmount, int quantity,
		    bool isFreeAction)
	    {
		    TrackVirtualGoodsFeaturePurchase(featureType, featureSubType, 
			    virtualCurrencyAmount, null, new GameCurrency(), quantity, isFreeAction);
	    } // TrackVirtualGoodsFeaturePurchase(featureType, featureSubType, virtu)

	    /// <summary>
	    /// Tracks a purchase of a virtual goods feature, e.g. reset a block time 
	    /// for the next fight, finish a construction without waiting, etc..
	    /// </summary>
	    /// <param name="featureType">Defines the main type of the feature, e.g.: 
	    /// Fight, Building, Training, etc.</param>
	    /// <param name="featureSubType">Specifies the feature, e.g.: Reset Block 
	    /// Time, Finish, Strength, etc.</param>
	    /// <param name="virtualCurrencyAmount">The amount of virtual currency 
	    /// spent for the purchase</param>
	    /// <param name="virtualCurrencyName">The name of the virtual currency 
	    /// for the purchase</param>
	    /// <param name="gameCurrency">Used game curency as array depending on 
	    /// your game currencies, e.g.: 
	    /// <code>Packets[n][GameCurrency]={‘gold’:5}</code></param>
	    /// <param name="quantity">Defines the amount of usages</param>
	    /// <param name="isFreeAction">Defines if the action was for free, e.g. 
	    /// for  special promotions</param>
	    public void TrackVirtualGoodsFeaturePurchase(string featureType,
		    string featureSubType, double virtualCurrencyAmount, string virtualCurrencyName,
		    GameCurrency gameCurrency, int quantity, bool isFreeAction)
	    {
		    List<KeyValuePair<string, string>> eventData =
				    new List<KeyValuePair<string, string>>();

		    eventData.Add(new KeyValuePair<string, string>("FeatureType", 
			    featureType));
		    eventData.Add(new KeyValuePair<string, string>("FeatureSubType",
			    featureSubType));
            eventData.Add(new KeyValuePair<string, string>("Value",
			    virtualCurrencyAmount.ToString(
			    CultureInfo.InvariantCulture.NumberFormat)));
		    if (virtualCurrencyName != null) {
			    eventData.Add (new KeyValuePair<string, string> ("VirtualCurrencyName",
				    virtualCurrencyName)
			    );
		    }
		    eventData.Add(new KeyValuePair<string, string>("GameCurrency",
			    gameCurrency != null ? gameCurrency.ToJsonString() : ""));
		    eventData.Add(new KeyValuePair<string, string>("Quantity",
			    quantity.ToString()));
		    eventData.Add(new KeyValuePair<string, string>("IsFreeAction",
			    isFreeAction == true ? "true" : "false"));

		    AddEvent("VirtualGoods::" + featureType + "::" + 
			    featureSubType, eventData);
	    } // TrackVirtualGoodsFeaturePurchase(featureType, featureSubType, Virtu)

	    #endregion

        #region Non tracking methods

        private void AddEvent(string action)
        {
            AddEvent(action, new List<KeyValuePair<string, string>>());
        } // AddEvent(action)

        /// <summary>
        /// Add event
        /// </summary>
        private void AddEvent(string action,
            List<KeyValuePair<string, string>> eventData)
        {
            List<KeyValuePair<string, string>> combinedEventData =
                new List<KeyValuePair<string, string>>();

            combinedEventData.AddRange(eventData);
            combinedEventData.AddRange(config.GetParameterList(TimeHelper.GetCurrentTimestampSeconds()));

            transport.Queue(new TrackingEvent(action).SetData(combinedEventData));
        } // AddEvent(action, eventData)

        public Tracking(Config config, Transport transport)
        {
            this.transport = transport;
            this.config = config;
        }

        #endregion
    } // class Tracking
}

