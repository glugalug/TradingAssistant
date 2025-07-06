using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingAssistant.JsonResponses
{
    internal class CoinDeskMarkets
    {
        // Only bare minimal fields being used are are included here.  More can be added as needed.
        public class MarketData
        {
            public string EXCHANGE_STATUS { get; set; }
            public string EXCHANGE_INTERNAL_NAME { get; set; }
        }

        public Dictionary<string, MarketData> Data { get; set; }
        public CoinDeskErr Err { get; set; }
    }
}

/*
                public long ID { get; set; }
                public string TYPE { get; set; }
                public long? MAPPED_INSTRUMENTS_TOTAL { get; set; }
                public long? UNMAPPED_INSTRUMENTS_TOTAL { get; set; }
                public class InstrumentStatus
                {
                    public long ACTIVE { get; set; }
                    public long IGNORED { get; set; }
                    public long RETIRED { get; set; }
                    public long EXPIRED { get; set; }
                }
                public InstrumentStatus? INSTRUMENT_STATUS { get; set; }
                public long? TOTAL_TRADES_SPOT { get; set; }
                public bool? HAS_ORDERBOOK_L2_MINUTE_SNAPSHOTS_ENABLED { get; set; }
                public string? URI { get; set; }
                public string? COMMENT { get; set; }
                public bool? IS_PUBLIC { get; set; }
                public long? ASSIGNED_TO { get; set; }
                public string? ASSIGNED_TO_USERNAME { get; set; }
                public double? CREATED_ON { get; set; }
                public double? CREATED_BY { get; set; }
                public string? CREATED_BY_USERNAME { get; set; }
                public long? UPDATED_ON { get; set; }
                public long? UPDATED_BY { get; set; }
                public string? UPDATED_BY_USERNAME { get; set; }
                public long? ASSIGNED_TO_INTEGRATION_MAIN { get; set; }
                public string? ASSIGNED_TO_USERNAME_INTEGRATION_MAIN { get; set; }
                public long? ASSIGNED_TO_INTEGRATION_BACKUP { get; set; }
                public string? ASSIGNED_TO_USERNAME_INTEGRATION_BACKUP { get; set; }
                public long? ASSIGNED_TO_BUSINESS_OR_CONTRACT { get; set; }
                public string? ASSIGNED_TO_USERNAME_BUSINESS_OR_CONTRACT { get; set; }
                public string? PUBLIC_NOTICE { get; set; }
                public string? EXCHANGE_SUSPENSION_REASON { get; set; }
                public double? TRADING_PERMANENTLY_SUSPENDED_DATE { get; set; }
                public string? NAME { get; set; }
                public string? LOGO_URL { get; set; }
                public double? LAUNCH_DATE { get; set; }
                public bool? HAS_SPOT_TRADING { get; set; }

                public class SpotTradingMechanism
                {
                    public string NAME { get; set; }
                }
                public SpotTradingMechanism[]? SPOT_TRADING_MECHANISMS { get; set; }

                public class SpotTradingRoleName {
                    public string ROLE_NAME {  get; set; }
                }
                public SpotTradingRoleName[]? SPOT_API_ACCESS_PERMITTED_ROLES { get; set; }
                public bool? HAS_FUTURES_TRADING { get; set;}
                public bool? HAS_INDEX_PUBLISHING { get; set;}
                public bool? HAS_OPTIONS_TRADING { get; set;}
                public bool? HAS_DEX_TRADING { get; set;}
                public string? WEBSITE_URL { get; set; }
                public string? BLOG_URL {  get; set; }
                public string? INCORPORATION_DOCUMENT_URL { get; set; }

                public class DocumentUrls
                {
                    public string? TYPE { get; set; }
                    public long? VERSION { get; set; }
                    public string? URL { get; set; }
                    public string? ORIGINAL_SOURCE_URL { get; set; }
                    public string? COMMENT {  get; set; }
                }
                public DocumentUrls[]? OTHER_DOCUMENT_URLS { get; set; }
                public class ExchangeCertification
                {
                    public string? NAME { get; set; }
                    public string? SUB_TYPE { get; set; }
                    public string? ID { get; set; }
                    public long? ISSUE_DATE { get; set; }
                    public string? URL { get; set; }
                    public string? COMMENTS { get; set; }
                }
                public ExchangeCertification[]? EXCHANGE_CERTIFICATIONS { get; set; }
                public class SupportContactInfo
                {
                    public string? CONTACT_MEDIUM { get; set; }
                    public string? DETAILS { get; set; }
                    public string? COMMENTS { get; set; }
                }
                public SupportContactInfo[]? SUPPORT_CONTACT_INFORMATION { get; set; }

                CONTROLLED_ADDRESSES: {
                type: "array"
                description: "The list of designated addresses used to manage and store assets within an investment portfolio or on behalf of clients. This includes addresses where cryptocurrencies, securities, or other assets are held, reflecting the diverse nature of modern investment strategies. It encompasses addresses used by exchanges, ETFs, and companies to maintain their investment reserves or operational funds."
                items: {
                type: "object"
                properties: {}
                }
                x-cc-api-group: "BASIC"
                }
                IS_INCLUDED_IN_CADLI: {
                type: "boolean"
                description: "Indicates whether the asset is part of the CADLI index, which calculates the price of an asset in USD. This field is crucial for tracking asset inclusion in CADLI, aiding in data analysis and decision-making processes related to asset pricing."
                x-cc-api-group: "INTERNAL"
                }
                EXCHANGE_ALTERNATIVE_IDS: {
                type: "array"
                description: "A collection of alternative identification data for exchanges as recognized by various data platforms."
                items: {
                type: "object"
                properties: {}
                }
                x-cc-api-group: "BASIC"
                }
                EXCHANGE_DESCRIPTION: {
                type: "string"
                description: "The long form description in markdown for this exchange."
                x-cc-api-group: "DESCRIPTION"
                }
                EXCHANGE_DESCRIPTION_SUMMARY: {
                type: "string"
                description: "The short form description text only for this exchange."
                x-cc-api-group: "DESCRIPTION_SUMMARY"
                }
                EXCHANGE_DESCRIPTION_SNIPPET: {
                type: "string"
                description: "The shortest form description text only for this exchange. This is a lot more limited than the summary. Generally this is a one or maximum two sentences."
                x-cc-api-group: "BASIC"
                }
                IS_HIDDEN: {
                type: "boolean"
                description: "Indicates whether the exchange is hidden."
                x-cc-api-group: "INTERNAL"
                }
                SPOT_INTEGRATION_COMMENTS: {
                type: "string"
                description: "Any internal comments for the spot integration, this is used by both the order book team and the nodejs team."
                x-cc-api-group: "INTEGRATION_SPOT"
                }
                SPOT_TRADING_LAUNCH_DATE: {
                type: "number"
                description: "The launch date of the exchange is indicated as (yyyy-mm-dd)."
                x-cc-api-group: "BASIC"
                }
                SPOT_TRADES_INTEGRATION_STAGE: {
                type: "string"
                description: "The current stage of the exchange integration process"
                x-cc-api-group: "INTEGRATION_SPOT"
                }
                SPOT_TRADES_INTEGRATION_DATE: {
                type: "number"
                description: "The integration date of the exchange is indicated as (yyyy-mm-dd)."
                x-cc-api-group: "INTEGRATION_SPOT"
                }
                HAS_SPOT_TRADES_POLLING: {
                type: "boolean"
                description: ""
                x-cc-api-group: "INTEGRATION_SPOT"
                }
                HAS_SPOT_TRADES_POLLING_BACKFILL: {
                type: "boolean"
                description: ""
                x-cc-api-group: "INTEGRATION_SPOT"
                }
                HAS_SPOT_TRADES_STREAMING: {
                type: "boolean"
                description: ""
                x-cc-api-group: "INTEGRATION_SPOT"
                }
                SPOT_ORDER_BOOK_INTEGRATION_STAGE: {
                type: "string"
                description: "The current stage of the exchange integration process"
                x-cc-api-group: "INTEGRATION_SPOT"
                }
                SPOT_ORDER_BOOK_INTEGRATION_DATE: {
                type: "number"
                description: "The integration date of the exchange is indicated as (yyyy-mm-dd)."
                x-cc-api-group: "INTEGRATION_SPOT"
                }
                HAS_SPOT_ORDER_BOOK_POLLING: {
                type: "boolean"
                description: ""
                x-cc-api-group: "INTEGRATION_SPOT"
                }
                HAS_SPOT_ORDER_BOOK_STREAMING: {
                type: "boolean"
                description: ""
                x-cc-api-group: "INTEGRATION_SPOT"
                }
                CCDATA_LATEST_SPOT_BENCHMARK_SCORE: {
                type: "number"
                description: "This is a measure of the total number of benchmark score out of 100. It is the sum of all the individual section score in the latest benchmark report."
                x-cc-api-group: "BENCHMARK"
                }
                CCDATA_LATEST_SPOT_BENCHMARK_GRADE: {
                type: "string"
                description: "This is a measure of the grade the exchange has based on the benchmark score. We classify anything over B to be a top tier exchange."
                x-cc-api-group: "BENCHMARK"
                }
                CCDATA_HISTORICAL_SPOT_BENCHMARK_REPORTS: {
                type: "array"
                description: "An array holding the series of benchmark reports data for the exchange. Each element in the array corresponds to a set of data from a specific report, collectively providing a comprehensive historical record of the exchange's performance metrics over time."
                items: {
                type: "object"
                properties: {}
                }
                x-cc-api-group: "BENCHMARK"
                } */
