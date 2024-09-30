using System;

namespace sassy.bulk.Dtos
{
    public class ElectronicTenderDto
    {
        public ElectronicTenderDto()
        {
            Id = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();
        }
        public string Id { get; set; }
        public string InvoiceNumber { get; set; }
        public bool IsVoid { get; set; }
        public string ResponseOrigin { get; set; }
        public string DSIXReturnCode { get; set; }
        public string CmdStatus { get; set; }
        public string TextResponse { get; set; }
        public string SequenceNo { get; set; }
        public string UserTrace { get; set; }
        public string MerchantID { get; set; }
        public string AcctNo { get; set; }
        public string CardType { get; set; }
        public string TranCode { get; set; }
        public string AuthCode { get; set; }
        public string CaptureStatus { get; set; }
        public string RefNo { get; set; }
        public string InvoiceNo { get; set; }
        public string OperatorID { get; set; }
        public decimal? Purchase { get; set; }
        public decimal? Authorize { get; set; }
        public decimal CCFee { get; set; }
        public string AcqRefData { get; set; }
        public string RecordNo { get; set; }
        public string EntryMethod { get; set; }
        public DateTime? Date { get; set; }
        public TimeSpan? Time { get; set; }
        public Guid UID { get; set; }
        public string AID { get; set; }
        public string ApplicationLabel { get; set; }
        public string TVR { get; set; }
        public string IAD { get; set; }
        public string TSI { get; set; }
        public string ARC { get; set; }
        public string CVM { get; set; }
        public string AVSResult { get; set; }
        public string CVVResult { get; set; }
        public string Signature { get; set; }
        public string Receipt { get; set; }
        public string InvoiceId { get; set; }
        public string ProcessData { get; set; }
        public string PinPadType { get; set; }
        public string AddlRspData { get; set; }
        public string ApprovedCashBackAmount { get; set; }
        public string ApprovedMerchantFee { get; set; }
        public string ApprovedTaxAmount { get; set; }
        public string ApprovedTipAmount { get; set; }
        public string AuthorizationResponse { get; set; }
        public string BogusAccountNum { get; set; }
        public string CardInfo { get; set; }
        public string DebitAccountType { get; set; }
        public string ECRTransID { get; set; }
        public string EDCType { get; set; }
        public string ExtData { get; set; }
        public string ExtraBalance { get; set; }
        public string FleetCard { get; set; }
        public string GatewayTransactionID { get; set; }
        public string GiftCardType { get; set; }
        public string HostCode { get; set; }
        public string HostDetailedMessage { get; set; }
        public string HostResponse { get; set; }
        public string IssuerResponseCode { get; set; }
        public string MaskedPAN { get; set; }
        public string Message { get; set; }
        public string MultiMerchant { get; set; }
        public string PaymentAccountReferenceID { get; set; }
        public string PaymentService2000 { get; set; }
        public string RawResponse { get; set; }
        public string RemainingBalance { get; set; }
        public string Restaurant { get; set; }
        public string RetrievalReferenceNumber { get; set; }
        public string SigFileName { get; set; }
        public string SignData { get; set; }
        public string TORResponseInfo { get; set; }
        public string Timestamp { get; set; }
        public string Track1Data { get; set; }
        public string Track2Data { get; set; }
        public string Track3Data { get; set; }
        public string TransactionIntegrityClass { get; set; }
        public string TransactionRemainingAmount { get; set; }
        public string VASResponseInfo { get; set; }
        public decimal TipAmount { get; set; }
    }
}
