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
    public interface ITracking 
    {
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
	    void TrackFeatureUsage(string featureType, string featureSubType,
		    string featureSubSubType, int quantity);

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
	    void TrackFeatureUsage(string featureType, string featureSubType,
		    string featureSubSubType, GameCurrency gameCurrency, int quantity);

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
	    void TrackClick(string uniqueCustomerClickToken,
		    string marketingIdentifier, string landingPageId);
		
	    void TrackClick(string uniqueCustomerClickToken,
		    string marketingIdentifier);

	    void TrackClick(string uniqueCustomerClickToken);

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
	    void TrackLevelup(int level);

	    #endregion

	    #region TrackLogin
	    /// <summary>
	    /// Tracks the user login.
	    /// </summary>
	    void TrackLogin();

	    #endregion

	    #region TrackLogout
	    /// <summary>
	    /// Tracks the user logout.
	    /// </summary>
	    void TrackLogout();

	    #endregion

	    #region TackUserProfile
	    #region TrackUserGender
	    /// <summary>
	    /// Tracks the gender of an user.
	    /// </summary>
	    /// <param name="gender">Defines the gender oft he user, if known, valid 
	    /// values are: female, male </param>
	    void TrackUserGender(string gender);

	    #endregion

	    #region TrackUserBirthyear
	    /// <summary>
	    /// Tracks the birthyear of an user.
	    /// </summary>
	    /// <param name="birthyear">Define s the birthyear of the user, e.g.: 
	    /// 1983, 1973, 1969</param>
	    void TrackUserBirthyear(int birthyear);

	    #endregion

	    #region TrackUserCustomStaticClassification
	    /// <summary>
	    /// Track user custom static classification.
	    /// </summary>
	    /// <param name="userCustomStaticClassification">Defines a custom user 
	    /// classification, e.g. the player class, e.g. Elf, Tank, etc.. </param>
	    void TrackUserCustomStaticClassification(string
		    userCustomStaticClassification);

	    #endregion

	    #endregion

	    #region TrackSignup
	    /// <summary>
	    /// Tracks the user signup, if available with the corresponding marketing 
	    /// identifier token.
	    /// </summary>
	    void TrackSignup();

	    /// <summary>
	    /// Tracks the user signup, if available with the corresponding marketing 
	    /// identifier token.
	    /// </summary>
	    /// <param name="marketingIdentifier">Marketing identifier token, this 
	    /// token is used to identify the corresponding ad, campaign and 
	    /// marketing.</param>
	    void TrackSignup(string marketingIdentifier);

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
	    void TrackSignup(string marketingIdentifier, string landingPage);

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
	    void TrackSignup(
		    string marketingIdentifier,
		    string marketingPartner,
		    string marketingCampaign,
		    string marketingAd,
		    string marketingKeyword,
		    string landingPage
	    );


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
	    void TrackSignup(
		    string uniqueCustomerClickToken,
		    string marketingIdentifier,
		    string marketingPartner,
		    string marketingCampaign,
		    string marketingAd,
		    string marketingKeyword,
		    string landingPage
		    );

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
	    void TrackViralityInvitation(string inviteType,
		    string inviteMessageToken, int quantity);

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
	    void TrackViralityInviteAcceptance(string inviteType,
		    string inviteMessageToken, string sourceUniqueCustomerIdentifier);

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
	    void TrackVirtualCurrenciesChargeback (double virtualCurrencyAmount, string virtualCurrencyName,
		    string paymentType, double revenue, string revenueCurrency, 
		    double payout, string payoutCurrency);

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
	    void TrackVirtualCurrencyPurchase(double virtualCurrencyAmount, string virtualCurrencyName,
		    string paymentType, double revenue, string revenueCurrency,
		    double payout, string payoutCurrency);

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
	    void TrackVirtualCurrencyChargeback(
		    double virtualCurrencyAmount,
		    string virtualCurrencyName,
		    string paymentType,
		    double revenue,
		    string revenueCurrency,
		    double payout,
		    string payoutCurrency
	    );

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
	    void TrackVirtualGoodsItemPurchase(string itemType, Item item,
		    double virtualCurrencyAmount, int quantity, bool isFreeAction);

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
	    void TrackVirtualGoodsItemPurchase(string itemType, Item item,
		    double virtualCurrencyAmount, string virtualCurrencyName, GameCurrency gameCurrency, int quantity,
		    bool isFreeAction);

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
	    void TrackVirtualGoodsFeaturePurchase(string featureType,
		    string featureSubType, double virtualCurrencyAmount, int quantity,
		    bool isFreeAction);

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
	    void TrackVirtualGoodsFeaturePurchase(string featureType,
		    string featureSubType, double virtualCurrencyAmount, string virtualCurrencyName,
		    GameCurrency gameCurrency, int quantity, bool isFreeAction);

	    #endregion
    }
}

