using Skender.Stock.Indicators;
using SpotBot.Server.Api.Requests;
using SpotBot.Server.Api.Responses;
using SpotBot.Server.Api.Responses.Shapes;
using SpotBot.Server.Database.Records;
using SpotBot.Server.Domain.Trading.Models;
using SpotBot.Server.Exchange.RestApi.Responses;
using SpotBot.Server.Exchange.RestApi.Responses.Shapes;
using SpotBot.Server.Exchange.Websockets.Publications.Public;

namespace SpotBot.Server.Core
{
    internal static class Mappers
    {

        public static AccountShape ToShape(this AccountExchangeShape accountExchangeShape)
        {
            var accountShape = new AccountShape();
            accountShape.Available = accountExchangeShape.Available;
            accountShape.Balance = accountExchangeShape.Balance;
            accountShape.Currency = accountExchangeShape.Currency;
            accountShape.Holds = accountExchangeShape.Holds;
            accountShape.Id = accountExchangeShape.Id;
            accountShape.Type = accountExchangeShape.Type;
            return accountShape;
        }
        public static GetAccountsResponse? ToResponse(this GetAccountsExchangeResponse? getAccountsExchangeResponse)
        {
            if (getAccountsExchangeResponse == null)
            {
                return null;
            }
            var getAccountsResponse = new GetAccountsResponse();
            getAccountsResponse.Accounts = getAccountsExchangeResponse.Accounts.Select(ToShape).ToList();
            return getAccountsResponse;
        }
        public static Candle ToDomainModel(this KLineExchangeShape shape)
        {
            var model = new Candle();
            model.Open = shape.Open;
            model.High = shape.High;
            model.Low = shape.Low;
            model.Close = shape.Close;
            model.Volume = shape.Volume;
            model.Date = DateTimeOffset.FromUnixTimeSeconds(shape.Time).DateTime;
            return model;
        }
        public static List<Candle> ToDomainModels(this IEnumerable<KLineExchangeShape>? shapes)
        {
            var models = shapes?.ToList().Select(ToDomainModel).ToList();
            models ??= new List<Candle>();
            return models;
        }
        public static Quote ToQuote(this Candle candle)
        {
            var quote = new Quote();
            quote.Open = candle.Open;
            quote.High = candle.High;
            quote.Low = candle.Low;
            quote.Close = candle.Close;
            quote.Volume = candle.Volume;
            quote.Date = candle.Date;
            return quote;
        }
        public static List<Quote> ToQuotes(this IEnumerable<Candle>? candles)
        {
            var quotes = candles?.ToList().Select(ToQuote).ToList();
            quotes ??= new List<Quote>();
            return quotes;
        }
        public static GetExchangeResponse? ToResponse(this ExchangeRecord? exchangeRecord)
        {
            if (exchangeRecord == null)
            {
                return null;
            }
            var getExchangeResponse = new GetExchangeResponse();
            getExchangeResponse.ApiPublicKey = exchangeRecord.ApiPublicKey;
            getExchangeResponse.ApiPrivateKey = exchangeRecord.ApiPrivateKey;
            getExchangeResponse.ApiKeyPassphrase = exchangeRecord.ApiKeyPassphrase;
            getExchangeResponse.ApiVersion = exchangeRecord.ApiVersion;
            getExchangeResponse.UserId = exchangeRecord.UserId;
            getExchangeResponse.Id = exchangeRecord.Id;
            return getExchangeResponse;
        }

        public static ExchangeRecord ToRecord(this PostExchangeRequest postExchangeRequest, int userId)
        {
            var exchangeRecord = new ExchangeRecord();
            exchangeRecord.ApiPublicKey = postExchangeRequest.ApiPublicKey;
            exchangeRecord.ApiPrivateKey = postExchangeRequest.ApiPrivateKey;
            exchangeRecord.ApiKeyPassphrase = postExchangeRequest.ApiKeyPassphrase;
            exchangeRecord.ApiVersion = postExchangeRequest.ApiVersion;
            exchangeRecord.UserId = userId;
            return exchangeRecord;
        }

        //public static UserTokenRecord ToRecord(this PostUserCredentialsRequest postUserCredentialsRequest)
        //{
        //    var userTokenRecord = new UserTokenRecord();
        //    userTokenRecord.Id = postUserCredentialsRequest.Id;
        //    userTokenRecord.Token = postUserCredentialsRequest.Token;
        //    userTokenRecord.UserId = postUserCredentialsRequest.UserId;
        //    userTokenRecord.CreationTime = postUserCredentialsRequest.CreationTime;
        //    userTokenRecord.ExpirationTime = postUserCredentialsRequest.ExpirationTime;
        //    userTokenRecord.LastActivityTime = postUserCredentialsRequest.LastActivityTime;
        //    userTokenRecord.UserAgent = postUserCredentialsRequest.UserAgent;
        //    userTokenRecord.IpAddress = postUserCredentialsRequest.IpAddress;
        //}

        public static GetUserTokenResponse? ToResponse(this UserTokenRecord? userTokenRecord)
        {
            if (userTokenRecord == null)
            {
                return null;
            }
            var getUserTokenResponse = new GetUserTokenResponse();
            getUserTokenResponse.Id = userTokenRecord.Id;
            getUserTokenResponse.UserId = userTokenRecord.UserId.GetValueOrDefault();
            getUserTokenResponse.CreationTime = userTokenRecord.CreationTime.GetValueOrDefault();
            getUserTokenResponse.ExpirationTime = userTokenRecord.ExpirationTime.GetValueOrDefault();
            getUserTokenResponse.LastActivityTime = userTokenRecord.LastActivityTime.GetValueOrDefault();
            getUserTokenResponse.Token = userTokenRecord.Token;
            getUserTokenResponse.UserAgent = userTokenRecord.UserAgent;
            getUserTokenResponse.IpAddress = userTokenRecord.IpAddress;
            return getUserTokenResponse;
        }

        public static PostUserCredentialsResponse? ToPostUserCredentialsResponse(this UserTokenRecord? userTokenRecord)
        {
            if (userTokenRecord == null)
            {
                return null;
            }
            var getUserTokenResponse = new PostUserCredentialsResponse();
            getUserTokenResponse.Id = userTokenRecord.Id;
            getUserTokenResponse.UserId = userTokenRecord.UserId.GetValueOrDefault();
            getUserTokenResponse.CreationTime = userTokenRecord.CreationTime.GetValueOrDefault();
            getUserTokenResponse.ExpirationTime = userTokenRecord.ExpirationTime.GetValueOrDefault();
            getUserTokenResponse.LastActivityTime = userTokenRecord.LastActivityTime.GetValueOrDefault();
            getUserTokenResponse.Token = userTokenRecord.Token;
            getUserTokenResponse.UserAgent = userTokenRecord.UserAgent;
            getUserTokenResponse.IpAddress = userTokenRecord.IpAddress;
            return getUserTokenResponse;
        }

        public static GetEncryptionKeyResponse? ToResponse(this EncryptionKeyRecord? encryptionKeyRecord)
        {
            if (encryptionKeyRecord == null)
            {
                return null;
            }
            var getEncryptionKeyResponse = new GetEncryptionKeyResponse();
            getEncryptionKeyResponse.Id = encryptionKeyRecord.Id;
            getEncryptionKeyResponse.UserId = encryptionKeyRecord.UserId;
            getEncryptionKeyResponse.Value = encryptionKeyRecord.ObfuscatedValue;
            return getEncryptionKeyResponse;
        }

        public static ExchangeRecord ToRecord(this PatchExchangeRequest patchExchangeRequest, int userId)
        {
            var exchangeRecord = new ExchangeRecord();
            exchangeRecord.Id = patchExchangeRequest.Id;
            exchangeRecord.ApiVersion = patchExchangeRequest.ApiVersion;
            exchangeRecord.ApiKeyPassphrase = patchExchangeRequest.ApiKeyPassphrase;
            exchangeRecord.ApiPrivateKey = patchExchangeRequest.ApiPrivateKey;
            exchangeRecord.ApiPublicKey = patchExchangeRequest.ApiPublicKey;
            exchangeRecord.UserId = userId;
            return exchangeRecord;
        }


    }
}
