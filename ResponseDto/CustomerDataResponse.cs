using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace sassy.bulk.ResponseDto
{
    public class CustomerDataResponse
    {
        [JsonProperty("dateOfBirth")]
        public DateTime DateOfBirth { get; set; }

        [JsonProperty("updateInfoIfAlreadyExist")]
        public bool UpdateInfoIfAlreadyExist { get; set; }

        [JsonProperty("purchaseHistories")]
        public List<PurchaseHistory> PurchaseHistories { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("role")]
        public List<string> Role { get; set; }

        [JsonProperty("street")]
        public string Street { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("postalCode")]
        public string PostalCode { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("custID")]
        public string CustID { get; set; }

        [JsonProperty("awardLevelLastUpdated")]
        public DateTime AwardLevelLastUpdated { get; set; }

        [JsonProperty("awardLevel")]
        public int AwardLevel { get; set; }

        [JsonProperty("purchaseLevelLastUpdated")]
        public DateTime PurchaseLevelLastUpdated { get; set; }

        [JsonProperty("purchaseLevel")]
        public decimal PurchaseLevel { get; set; }

        [JsonProperty("riskIndex")]
        public int RiskIndex { get; set; }

        [JsonProperty("notificationSettings")]
        public NotificationSettings NotificationSettings { get; set; }

        [JsonProperty("lastTransaction")]
        public LastTransaction LastTransaction { get; set; }

        [JsonProperty("lastPointsOnLastInvoice")]
        public decimal LastPointsOnLastInvoice { get; set; }

        [JsonProperty("rewardPoints")]
        public decimal RewardPoints { get; set; }

        [JsonProperty("lastPointsUpdated")]
        public DateTime LastPointsUpdated { get; set; }

        [JsonProperty("lastPointsRedeemed")]
        public decimal LastPointsRedeemed { get; set; }

        [JsonProperty("lastRedeemedAmount")]
        public decimal LastRedeemedAmount { get; set; }

        [JsonProperty("lastPointsRedeemedDate")]
        public DateTime LastPointsRedeemedDate { get; set; }

        [JsonProperty("isLoyal")]
        public bool IsLoyal { get; set; }

        [JsonProperty("loyaltyAllowed")]
        public bool LoyaltyAllowed { get; set; }

        [JsonProperty("flagged")]
        public bool Flagged { get; set; }

        [JsonProperty("status")]
        public bool Status { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("createdDate")]
        public DateTime CreatedDate { get; set; }
    }

    public class PurchaseHistory
    {
        
    }

    public class NotificationSettings
    {
        [JsonProperty("sendSMSAlert")]
        public bool SendSMSAlert { get; set; }

        [JsonProperty("sendSMSMarketing")]
        public bool SendSMSMarketing { get; set; }

        [JsonProperty("sendEmailAlert")]
        public bool SendEmailAlert { get; set; }

        [JsonProperty("sendEmailMarketing")]
        public bool SendEmailMarketing { get; set; }

        [JsonProperty("sendPushAlert")]
        public bool SendPushAlert { get; set; }

        [JsonProperty("sendPushMarketing")]
        public bool SendPushMarketing { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("createdDate")]
        public DateTime CreatedDate { get; set; }
    }

    public class LastTransaction
    {
        [JsonProperty("time")]
        public string Time { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("customer")]
        public string Customer { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }
    }

}
