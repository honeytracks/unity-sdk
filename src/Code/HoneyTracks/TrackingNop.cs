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
    public class TrackingNop : ITracking
    {
        public void TrackFeatureUsage(string featureType, string featureSubType, string featureSubSubType, int quantity)
        {
            // just to nothing
        }

        public void TrackFeatureUsage(string featureType, string featureSubType, string featureSubSubType, GameCurrency gameCurrency, int quantity)
        {
            // just to nothing
        }

        public void TrackClick(string uniqueCustomerClickToken, string marketingIdentifier, string landingPageId)
        {
            // just to nothing
        }

        public void TrackClick(string uniqueCustomerClickToken, string marketingIdentifier)
        {
            // just to nothing
        }

        public void TrackClick(string uniqueCustomerClickToken)
        {
            // just to nothing
        }

        public void TrackLevelup(int level)
        {
            // just to nothing
        }

        public void TrackLogin()
        {
            // just to nothing
        }

        public void TrackLogout()
        {
            // just to nothing
        }

        public void TrackUserGender(string gender)
        {
            // just to nothing
        }

        public void TrackUserBirthyear(int birthyear)
        {
            // just to nothing
        }

        public void TrackUserCustomStaticClassification(string userCustomStaticClassification)
        {
            // just to nothing
        }

        public void TrackSignup()
        {
            // just to nothing
        }

        public void TrackSignup(string marketingIdentifier)
        {
            // just to nothing
        }

        public void TrackSignup(string marketingIdentifier, string landingPage)
        {
            // just to nothing
        }

        public void TrackSignup(string marketingIdentifier, string marketingPartner, string marketingCampaign, string marketingAd, string marketingKeyword, string landingPage)
        {
            // just to nothing
        }

        public void TrackSignup(string uniqueCustomerClickToken, string marketingIdentifier, string marketingPartner, string marketingCampaign, string marketingAd, string marketingKeyword, string landingPage)
        {
            // just to nothing
        }

        public void TrackViralityInvitation(string inviteType, string inviteMessageToken, int quantity)
        {
            // just to nothing
        }

        public void TrackViralityInviteAcceptance(string inviteType, string inviteMessageToken, string sourceUniqueCustomerIdentifier)
        {
            // just to nothing
        }

        public void TrackVirtualCurrenciesChargeback(double virtualCurrencyAmount, string virtualCurrencyName, string paymentType, double revenue, string revenueCurrency, double payout, string payoutCurrency)
        {
            // just to nothing
        }

        public void TrackVirtualCurrencyPurchase(double virtualCurrencyAmount, string virtualCurrencyName, string paymentType, double revenue, string revenueCurrency, double payout, string payoutCurrency)
        {
            // just to nothing
        }

        public void TrackVirtualCurrencyChargeback(double virtualCurrencyAmount, string virtualCurrencyName, string paymentType, double revenue, string revenueCurrency, double payout, string payoutCurrency)
        {
            // just to nothing
        }

        public void TrackVirtualGoodsItemPurchase(string itemType, Item item, double virtualCurrencyAmount, int quantity, bool isFreeAction)
        {
            // just to nothing
        }

        public void TrackVirtualGoodsItemPurchase(string itemType, Item item, double virtualCurrencyAmount, string virtualCurrencyName, GameCurrency gameCurrency, int quantity, bool isFreeAction)
        {
            // just to nothing
        }

        public void TrackVirtualGoodsFeaturePurchase(string featureType, string featureSubType, double virtualCurrencyAmount, int quantity, bool isFreeAction)
        {
            // just to nothing
        }

        public void TrackVirtualGoodsFeaturePurchase(string featureType, string featureSubType, double virtualCurrencyAmount, string virtualCurrencyName, GameCurrency gameCurrency, int quantity, bool isFreeAction)
        {
            // just to nothing
        }
    }
}

