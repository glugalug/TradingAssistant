using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Newtonsoft.Json;

namespace TradingAssistant.JsonResponses
{
    internal class CoinDeskTopList
    {
        public class Item
        {
            // CoinDesk ID for the asset.
            [Display(AutoGenerateField = false, Description = "The unique identifier for the asset entry")]
            public int ID { get; set; }

            [Display(Description = "Internal mapped symbol for a specific asset")]
            public required string SYMBOL { get; set; }

            [Display(Description = "The uri path that this asset will be found on / url-slug")]
            public string? URI { get; set; }

            [Display(Description = "The asset class/type")]
            public string? ASSET_TYPE { get; set; }

            [Display(Description = "The full name of the asset, e.g. Bitcoin.")]
            public string? NAME { get; set; }

            public string? LOGO_URL { get; set; }
            public string? ISO_NUMERIC_CODE { get; set; }
            [Display(Description = "The Asset Symbol Glyph represents the visual or typographic mark associated with an asset, such as \"$\" for" +
                                   "USD or \"\u20bf\" for Bitcoin. It is distinct from the asset symbol (e.g., USD, BTC) and provides a recognizable " +
                                   "representation used in financial contexts, UIs, and documentation.")]
            public string? ASSET_SYMBOL_GLYPH { get; set; }

            [Display(Description =
                "This refers to the base, parent, or main asset to which a token is linked or pegged, signifying that the token " +
                "acts as a representation of the parent asset. When a token loses its connection to a parent asset due to events " +
                "such as hacks or the issuing entity's decision to not honor the peg—similar to how TerraUSD detached from its USD " +
                "peg—the PARENT_ASSET_SYMBOL is removed because the token no longer serves as a true representation of the parent " +
                "asset. In order to remove the parent we need clear communication from the company who is in charge of keeping the " +
                "peg. We add add a plublic notice and the include the communication in the Other Document URLs.")]
            public string? PARENT_ASSET_SYMBOL { get; set; }

            [Display(Description = "Can you build smart contracts on top of this?")]
            public bool? HAS_SMART_CONTRACT_CAPABILITIES { get; set; }
            [Display(Description =
                "This field classifies digital assets based on their level of smart contract support, ranging from assets with no " +
                "verifiable smart contract capabilities to those offering full autonomy. This categorization helps users and systems " +
                "understand an asset's technical capacity for smart contract execution, crucial for assessing its utility and potential " +
                "applications.")]
            public string? SMART_CONTRACT_SUPPORT_TYPE { get; set; }

            public string? MKT_CAP_EXCLUSION_REASON { get; set; }
            [Display(Description = "The link for the official project website.")]
            public string? WEBSITE_URL { get; set; }
            [Display(Description = "The link for the official blog.")]
            public string? BLOG_URL { get; set; }
            [Display(Description = "A white paper, also written as \"whitepaper\", a document released by the project that gives investors technical information about its concept, its purpose, how it works, etc.")]
            public string? WHITE_PAPER_URL { get; set; }
            public string? INDEX_FACTSHEET { get; set; }
            public string? PROSPECTUS { get; set; }
            [Display(Description = "The shortest form description text only for this asset. This is a lot more limited than the summary. Generally this is a one or maximum two sentences.")]
            public string? ASSET_DESCRIPTION_SNIPPET { get; set; }
            [Display(Description =
                "The total decimal places this asset can be divided into. E.g. 8 for BTC (1 Satoshi), 18 for ETH (1 Wei). " +
                "Generally blockchains store all units as integers and this is the number you need to divide the lowest unit " +
                "of accounting by to get the common unit of measure used for the asset.")]
            public int? ASSET_DECIMAL_POINTS { get; set; }
            public double? SUPPLY_MAX { get; set; }
            public double? SUPPLY_ISSUED { get; set; }
            public double? SUPPLY_TOTAL { get; set; }
            public double? SUPPLY_CIRCULATING { get; set; }
            public double? SUPPLY_FUTURE { get; set; }
            public double? SUPPLY_LOCKED { get; set; }
            public double? SUPPLY_BURNT { get; set; }
            public double? SUPPLY_STAKED { get; set; }
            [Display(Description = "New asset parts (coins/tokens) expected to be used to incetivise new block issuance. On tokens / chains that have no underlying asset infation, this will eventually be 0.")]
            public double? TARGET_BLOCK_MINT { get; set; }
            [Display(Description = "Target time span in seconds to produce a new block.")]
            public double? TARGET_BLOCK_TIME { get; set; }
            [Display(Description =
                "The total penalty applied to the mkt cap due to liquidity or quality of data. Comment example: The value is reduced " +
                "to 0.01% of the original due to low volume on B+ ranked exchanges or because it is only trading on a limited number " +
                "of exchanges.")]
            public double? MKT_CAP_PENALTY { get; set; }
            public int? ROOT_ASSET_ID { get; set; }
            public string? ROOT_ASSET_SYMBOL { get; set; }
            public string? ROOT_ASSET_TYPE { get; set; }
            public double? PRICE_USD { get; set; }
            public string? PRICE_USD_SOURCE { get; set; }
            public double? PRICE_USD_LAST_UPDATE_TS { get; set; }
            public double? PRICE_CONVERSION_RATE { get; set; }
            public double? PRICE_CONVERSION_VALUE { get; set; }
            public string? PRICE_CONVERSION_SOURCE { get; set; }
            public double? PRICE_CONVERSION_LAST_UPDATE_TS { get; set; }
            public double? CIRCULATING_MKT_CAP_USD { get; set; }
            public double? TOTAL_MKT_CAP_USD { get; set; }
            public double? CIRCULATING_MKT_CAP_CONVERSION { get; set; }
            public double? TOTAL_MKT_CAP_CONVERSION { get; set; }
            public double? SPOT_MOVING_24_HOUR_QUOTE_VOLUME_TOP_TIER_DIRECT_USD { get; set; }
            public double? SPOT_MOVING_24_HOUR_QUOTE_VOLUME_DIRECT_USD { get; set; }
            public double? SPOT_MOVING_24_HOUR_QUOTE_VOLUME_TOP_TIER_USD { get; set; }
            public double? SPOT_MOVING_24_HOUR_QUOTE_VOLUME_USD { get; set; }
            public double? SPOT_MOVING_24_HOUR_QUOTE_VOLUME_TOP_TIER_CONVERSION { get; set; }
            public double? SPOT_MOVING_24_HOUR_QUOTE_VOLUME_CONVERSION { get; set; }
            public double? SPOT_MOVING_24_HOUR_CHANGE_USD { get; set; }
            public double? SPOT_MOVING_24_HOUR_CHANGE_PERCENTAGE_USD { get; set; }
            public double? SPOT_MOVING_24_HOUR_CHANGE_CONVERSION { get; set; }
            public double? SPOT_MOVING_24_HOUR_CHANGE_PERCENTAGE_CONVERSION { get; set; }
            public double? SPOT_MOVING_7_DAY_QUOTE_VOLUME_TOP_TIER_DIRECT_USD { get; set; }
            public double? SPOT_MOVING_7_DAY_QUOTE_VOLUME_DIRECT_USD { get; set; }
            public double? SPOT_MOVING_7_DAY_QUOTE_VOLUME_TOP_TIER_USD { get; set; }
            public double? SPOT_MOVING_7_DAY_QUOTE_VOLUME_USD { get; set; }
            public double? SPOT_MOVING_7_DAY_QUOTE_VOLUME_TOP_TIER_CONVERSION { get; set; }
            public double? SPOT_MOVING_7_DAY_QUOTE_VOLUME_CONVERSION { get; set; }
            public double? SPOT_MOVING_7_DAY_CHANGE_USD { get; set; }
            public double? SPOT_MOVING_7_DAY_CHANGE_PERCENTAGE_USD { get; set; }
            public double? SPOT_MOVING_7_DAY_CHANGE_CONVERSION { get; set; }
            public double? SPOT_MOVING_7_DAY_CHANGE_PERCENTAGE_CONVERSION { get; set; }
            public double? SPOT_MOVING_30_DAY_QUOTE_VOLUME_TOP_TIER_DIRECT_USD { get; set; }
            public double? SPOT_MOVING_30_DAY_QUOTE_VOLUME_DIRECT_USD { get; set; }
            public double? SPOT_MOVING_30_DAY_QUOTE_VOLUME_TOP_TIER_USD { get; set; }
            public double? SPOT_MOVING_30_DAY_QUOTE_VOLUME_USD { get; set; }
            public double? SPOT_MOVING_30_DAY_QUOTE_VOLUME_TOP_TIER_CONVERSION { get; set; }
            public double? SPOT_MOVING_30_DAY_QUOTE_VOLUME_CONVERSION { get; set; }
            public double? SPOT_MOVING_30_DAY_CHANGE_USD { get; set; }
            public double? SPOT_MOVING_30_DAY_CHANGE_PERCENTAGE_USD { get; set; }
            public double? SPOT_MOVING_30_DAY_CHANGE_CONVERSION { get; set; }
            public double? SPOT_MOVING_30_DAY_CHANGE_PERCENTAGE_CONVERSION { get; set; }
            public int? TOTAL_ENDPOINTS_OK { get; set; }
            public int? TOTAL_ENDPOINTS_WITH_ISSUES { get; set; }
        }
        /**
         * 
         * Currently unused fields
         * 
        // Numeric value, doesn't seem useful.
        [Display(AutoGenerateField = false, Description = "Specifies the type or category of the message or data being handled. This is a unique number / id for each message type.")]
        public string TYPE { get; set; }

        [Display(AutoGenerateField = false, Description = "The legacy previous asset management system ID")]
        public int? ID_LEGACY { get; set; }

        [Display(AutoGenerateField = false)]
        public int? ID_PARENT_ASSET { get; set; }
        [Display(AutoGenerateField = false)]
        public int? ID_ASSET_ISSUER { get; set; }

        [Display(AutoGenerateField = false, Description = "Any internal comments you might have for this asset")]
        public string? COMMENT { get; set; }

        [Display(AutoGenerateField = false, Description = "This is flagged to false when assets are deleted/hidden")]
        public bool? IS_PUBLIC { get; set; }

        [Display(AutoGenerateField = false)]
        public int? ASSIGNED_TO { get; set; }

        [Display(AutoGenerateField = false)]
        public string? ASSIGNED_TO_USERNAME { get; set; }

        [Display(AutoGenerateField = false, Description = "Asset internal creation unix ts in our system")]
        public long? CREATED_ON { get; set; }

        [JsonIgnore]
        public DateTime? CREATION_DATE
        {
            get { return CREATED_ON != null ? DateTimeOffset.FromUnixTimeSeconds(CREATED_ON.Value).UtcDateTime : null; }
        }

        [Display(AutoGenerateField = false)]
        public int? CREATED_BY { get; set; }
        [Display(AutoGenerateField = false)]
        public string? CREATED_BY_USERNAME { get; set; }

        [Display(AutoGenerateField = false, Description = "Asset internal last updated unix ts in our system")]
        public long? UPDATED_ON { get; set; }

        [JsonIgnore]
        public DateTime? UPDATE_DATE
        {
            get { return UPDATED_ON != null ? DateTimeOffset.FromUnixTimeSeconds(UPDATED_ON.Value).UtcDateTime : null; }
        }

        [Display(AutoGenerateField = false)]
        public int? UPDATED_BY { get; set; }
        [Display(AutoGenerateField = false)]
        public string? UPDATED_BY_USERNAME { get; set; }
        [Display(AutoGenerateField = false)]
        public string? PUBLIC_NOTICE { get; set; }
        [Display(AutoGenerateField = false, Description = "The internal CoinDesk Indices API market name (index family) for this index.")]
        public string? INDEX_MARKET_NAME { get; set; }

        [Display(
            AutoGenerateField = false,
            Description = "The launch date of the asset is indicated as (yyyy-mm-dd). However, if the asset was initially established as a token " +
                          "before being integrated into a blockchain, the launch date is reset to the creation of the first block when the blockchain " +
                          "is launched for the token.")]
        public long? LAUNCH_DATE { get; set; }

        [JsonIgnore]
        public DateTime? LaunchDate
        {
            get { return LAUNCH_DATE != null ? DateTimeOffset.FromUnixTimeSeconds(LAUNCH_DATE.Value).UtcDateTime : null; }
        }

        [Display(AutoGenerateField = false, Description =
            "This field identifies the original creator of the asset. It provides essential information about the entity, " +
            "individual or contract rules responsible for issuing the asset initially and/or maintaining the supply. In the " +
            "case of of bridged assets, this is the bridge operator and the parent will have its own issuer. You can go up " +
            "the parent chain and figure out what counterparty risk you are exposed to when trading a specific asset. This " +
            "clarification ensures that users can directly trace the origin of the asset, understanding its issuance history " +
            "and the primary issuer's credentials.")]
        public string? ASSET_ISSUER_NAME { get; set; }

        public class ReservesBreakdownItem
        {
            public string? RESERVE_TYPE { get; set; }
            public class HoldingAddress
            {
                public string? BLOCKCHAIN { get; set; }
                public string? ADDRESS { get; set; }
            }
            public HoldingAddress[]? HOLDING_ADDRESSES { get; set; }
            public double? PERCENTAGE { get; set; }
            public string? DESCRIPTION { get; set; }
            public string? COMMENTS { get; set; }
        }
        [Display(AutoGenerateField = false)]
        public ReservesBreakdownItem[]? RESERVES_BREAKDOWN { get; set; }

        public class PreviousAssetSymbol
        {
            [Display(Description = "A symbol this asset was previously associated with.")]
            public string SYMBOL { get; set; }
            public long? SYMBOL_USAGE_START_DATE { get; set; }
            public long? SYMBOL_USAGE_END_DATE { get; set; }
            public string? DESCRIPTION { get; set; }
        }
        [Display(AutoGenerateField = false)]
        public PreviousAssetSymbol[]? PREVIOUS_ASSET_SYMBOLS { get; set; }

        [Display(AutoGenerateField = false)]
        public bool? IS_EXCLUDED_FROM_PRICE_TOPLIST { get; set; }
        [Display(AutoGenerateField = false)]
        public bool? IS_EXCLUDED_FROM_VOLUME_TOPLIST { get; set; }
        [Display(AutoGenerateField = false)]
        public bool? IS_EXCLUDED_FROM_MKT_CAP_TOPLIST { get; set; }

        [Display(AutoGenerateField = false)]
        public string? INDEX_METHODOLOGY { get; set; }

        public class IndexLinkedProduct
        {
            public string? NAME { get; set; }
            public string? SYMBOL { get; set; }
            public string? URL { get; set; }
            public string? COMMENTS { get; set; }
        }
        [Display(AutoGenerateField = false)]
        public IndexLinkedProduct[]? INDEX_LINKED_PRODUCTS { get; set; }

        public class DocumentUrl
        {
            public string? TYPE { get; set; }
            public int? VERSION { get; set; }
            public string? URL { get; set; }
            public string? COMMENT { get; set; }
        }
        [Display(AutoGenerateField = false)]
        public DocumentUrl[]? OTHER_DOCUMENT_URLS { get; set; }

        public class FullNameAddressComments
        {
            public string? FULL_NAME { get; set; }
            public string? ADDRESS { get; set; }
            public string? COMMENTS { get; set; }
        }
        public class ProjectLeader : FullNameAddressComments
        {
            public string? LEADER_TYPE { get; set; }
            public string? CONTACT_MEDIUM { get; set; }
        }
        [Display(AutoGenerateField = false)]
        public ProjectLeader[]? PROJECT_LEADERS { get; set; }
        public class NameContainer { public string? NAME { get; set; } }
        [Display(AutoGenerateField = false)]
        public class NameDescriptionContainer : NameContainer
        {
            public string? DESCRIPTION { get; set; }
        }
        public NameContainer[]? ASSET_CUSTODIANS { get; set; }

        public class AssetSecurityMetric
        {
            public string? NAME { get; set; }
            public double? OVERALL_SCORE { get; set; }
            public int? OVERALL_RANK { get; set; }
            public double? UPDATED_AT { get; set; }
        }
        [Display(AutoGenerateField = false)]
        public AssetSecurityMetric[]? ASSET_SECURITY_METRICS { get; set; }
        public class AssetIndustry
        {
            public string? ASSET_INDUSTRY { get; set; }
            public string? JUSTIFICATION { get; set; }
        }
        [Display(AutoGenerateField = false)]
        public AssetIndustry[]? ASSET_INDUSTRIES { get; set; }
        public class AssetAlternativeId
        {
            public string? NAME { get; set; }
            public string? ID { get; set; }
        }
        [Display(AutoGenerateField = false)]
        public AssetAlternativeId[]? ASSET_ALTERNATIVE_IDS { get; set; }
        [Display(AutoGenerateField = false, Description = "The long form description in markdown for this asset.")]
        public string? ASSET_DESCRIPTION { get; set; }
        [Display(AutoGenerateField = false, Description = "The short form description text only for this asset.")]
        public string? ASSET_DESCRIPTION_SUMMARY { get; set; }

        [Display(AutoGenerateField = false)]
        public NameContainer[]? CONSENSUS_MECHANISMS { get; set; }
        [Display(AutoGenerateField = false)]
        public NameDescriptionContainer[]? CONSENSUS_ALGORITHM_TYPES { get; set; }
        [Display(AutoGenerateField = false)]
        public NameContainer[]? HASHING_ALGORITHM_TYPES { get; set; }

        [Display(AutoGenerateField = false, Description = "The latest block number issued by the network.")]
        public long? LAST_BLOCK_NUMBER { get; set; }
        [Display(AutoGenerateField = false, Description = "The unix timestamp of the most recently issued block.")]
        public long? LAST_BLOCK_TIMESTAMP { get; set; }
        [Display(AutoGenerateField = false, Description = "Time spent in seconds to produce the most recently issued block.")]
        public double? LAST_BLOCK_TIME { get; set; }
        [Display(AutoGenerateField = false, Description = "The size in bytes of the most recently issued block.")]
        public long? LAST_BLOCK_SIZE { get; set; }
        [Display(AutoGenerateField = false)]
        public string? LAST_BLOCK_ISSUER { get; set; }
        [Display(AutoGenerateField = false)]
        public double? LAST_BLOCK_MINT { get; set; }
        [Display(AutoGenerateField = false)]
        public double? LAST_BLOCK_BURN { get; set; }
        [Display(AutoGenerateField = false)]
        public double? LAST_BLOCK_TRANSACTION_FEE_TOTAL { get; set; }
        [Display(AutoGenerateField = false)]
        public long? LAST_BLOCK_TRANSACTION_COUNT { get; set; }
        [Display(AutoGenerateField = false)]
        public double? LAST_BLOCK_HASHES_PER_SECOND { get; set; }
        [Display(AutoGenerateField = false)]
        public double? LAST_BLOCK_DIFFICULTY { get; set; }

        public class ExplorerAddress
        {
            public string URL { get; set; }
        }
        [Display(AutoGenerateField = false)]
        public ExplorerAddress[]? EXPLORER_ADDRESSES { get; set; }
        public class RpcOperator
        {
            public string? OPERATOR_NAME { get; set; }
            public string? OPERATOR_URL { get; set; }
            public bool? REQUIRES_API_KEY { get; set; }
            public string? DOCUMENTATION_URL { get; set; }
            public string? API_KEY_PARAMETER_NAME { get; set; }
            public string? API_KEY_PARAMETER_LOCATION { get; set; }
        }
        [Display(AutoGenerateField = false)]
        public RpcOperator[]? RPC_OPERATORS { get; set; }
        public class SupplyAddress
        {
            public string? NAME { get; set; }
            public string? BLOCKCHAIN { get; set; }
            public string? ADDRESS { get; set; }
            public string? DESCRIPTION { get; set; }
        }
        [Display(AutoGenerateField = false)]
        public SupplyAddress[]? BURN_ADDRESESS { get; set; }
        [Display(AutoGenerateField = false)]
        public SupplyAddress[]? LOCKED_ADDRESSES { get; set; }
        public class ControlledAddress : SupplyAddress
        {
            public string? ADDRESS_PURPOSE { get; set; }
            public string? CONTROL_TYPE { get; set; }
        }
        [Display(AutoGenerateField = false)]
        public ControlledAddress[]? CONTROLLED_ADDRESSES { get; set; }
        [Display(AutoGenerateField = false)]
        public NameContainer[]? SUPPORTED_STANDARDS { get; set; }
        public class SupportedPlatform
        {
            public string? BLOCKCHAIN { get; set; }
            public int? BLOCKCHAIN_ASSET_ID { get; set; }
            public string? TOKEN_STANDARD { get; set; }
            public string? EXPLORER_URL { get; set; }
            public string? SMART_CONTRACT_ADDRESS { get; set; }
            public long? LAUNCH_DATE { get; set; }
            public long? RETIRE_DATE { get; set; }
            public string? TRADING_AS { get; set; }
            public int? DECIMALS { get; set; }
            public bool? IS_INHERITED { get; set; }
        }
        [Display(AutoGenerateField = false)]
        public SupportedPlatform[]? SUPPORTED_PLATFORMS { get; set; }
        public class LayerTwoSolution
        {
            public string? NAME { get; set; }
            public string? WEBSITE_URL { get; set; }
            public string? DESCRIPTION { get; set; }
            public string? CATEGORY { get; set; }
            public class PermissionedAddress
            {
                public string? NAME { get; set; }
                public string? ADDRESS { get; set; }
                public string? ACCOUNT_TYPE { get; set; }
                public string? DESCRIPTION { get; set; }
            }
            public PermissionedAddress[]? PERMISSIONED_ADDRESSES { get; set; }
            public class SmartContract
            {
                public string? NAME { get; set; }
                public string? ADDRESS { get; set; }
                public bool? IS_UPGRADABLE { get; set; }
                public string? DESCRIPTION { get; set; }
            }
            public SmartContract[]? SMART_CONTRACTS_INVOLVED { get; set; }
        }
        [Display(AutoGenerateField = false)]
        public LayerTwoSolution[]? LAYER_TWO_SOLUTIONS { get; set; }
        public class PrivacySolution
        {
            public string? NAME { get; set; }
            public string? WEBSITE_URL { get; set; }
            public string? DESCRIPTION { get; set; }
            public NameContainer[]? PRIVACY_SOLUTION_FEATURES { get; set; }
            public string? PRIVACY_SOLUTION_TYPE { get; set; }
        }
        [Display(AutoGenerateField = false)]
        public PrivacySolution[]? PRIVACY_SOLUTIONS { get; set; }
        [Display(AutoGenerateField = false)]
        public string? SEO_TITLE { get; set; }
        [Display(AutoGenerateField = false)]
        public string? SEO_DESCRIPTION { get; set; }
        [Display(AutoGenerateField = false)]
        public string? OPEN_GRAPH_IMAGE_URL { get; set; }
        [Display(AutoGenerateField = false)]
        public string? ASSET_DESCRIPTION_EXTENDED_SEO { get; set; }
        public class Endpoint
        {
            public string? URL { get; set; }
            public string? TYPE { get; set; }
            public double? LAST_CALL { get; set; }
            public double? LAST_CALL_SUCCESS { get; set; }
            public string? EXTERNAL_CACHE_KEY { get; set; }
        }
        public class CodeRepository
        {
            public string? URL { get; set; }
            public bool? MAKE_3RD_PARTY_REQUEST { get; set; }
            public int? OPEN_ISSUES { get; set; }
            public int? CLOSED_ISSUES { get; set; }
            public int? OPEN_PULL_REQUESTS { get; set; }
            public int? CLOSED_PULL_REQUESTS { get; set; }
            public int? CONTRIBUTORS { get; set; }
            public int? FORKS { get; set; }
            public int? STARS { get; set; }
            public int? SUBSCRIBERS { get; set; }
            public double? LAST_UPDATED_TS { get; set; }
            public double? CREATED_AT { get; set; }
            public double? UPDATED_AT { get; set; }
            public double? LAST_PUSH_TS { get; set; }
            public long? CODE_SIZE_IN_BYTES { get; set; }
            public bool? IS_FORK { get; set; }
            public string? LANGUAGE { get; set; }
            public class ForkedAssetData
            {
                public int? ID { get; set; }
                public string? SYMBOL { get; set; }
                public string? CODE_REPOSITORY_URL { get; set; }
            }
            public ForkedAssetData? FORKED_ASSET_DATA { get; set; }
            public Endpoint[]? ENDPOINTS_USED { get; set; }
        }
        [Display(AutoGenerateField = false)]
        public CodeRepository[]? CODE_REPOSITORIES { get; set; }
        public class Social
        {
            public string? URL { get; set; }
            public bool? MAKE_3RD_PARTY_REQUEST { get; set; }
            public string? NAME { get; set; }
            public string? USERNAME { get; set; }
            public long? CURRENT_ACTIVE_USERS { get; set; }
            public double? LAST_UPDATED_TS { get; set; }
            public Endpoint[]? ENDPOINTS_USED { get; set; }
        }
        public class RedditSocial : Social
        {
            public double? AVERAGE_POSTS_PER_DAY { get; set; }
            public double? AVERAGE_POSTS_PER_HOUR { get; set; }
            public double? AVERAGE_COMMENTS_PER_DAY { get; set; }
            public double? AVERAGE_COMMENTS_PER_HOUR { get; set; }
            public long? SUBSCRIBERS { get; set; }
            public double? COMMUNITY_CREATED_AT { get; set; }
        }
        [Display(AutoGenerateField = false)]
        public RedditSocial[]? SUBREDDITS { get; set; }
        public class TwitterSocial : Social
        {
            public bool? VERIFIED { get; set; }
            public string? VERIFIED_TYPE { get; set; }
            public long? FOLLOWING { get; set; }
            public long? FOLLOWERS { get; set; }
            public int? FAVOURITES { get; set; }
            public int? LISTS { get; set; }
            public int? STATUSES { get; set; }
            public double? ACCOUNT_CREATED_AT { get; set; }
        }
        [Display(AutoGenerateField = false)]
        public TwitterSocial[]? TWITTER_ACCOUNTS { get; set; }
        public class DiscorderServer : Social
        {
            public long? TOTAL_MEMBERS { get; set; }
            public long? PREMIUM_SUBSCRIBERS { get; set; }
        }
        [Display(AutoGenerateField = false)]
        public DiscorderServer[] DISCORD_SERVERS { get; set; }
        public class TelegramGroup : Social
        {
            public string? MEMBERS { get; set; }
        }
        [Display(AutoGenerateField = false)]
        public TelegramGroup[]? TELEGRAM_GROUPS { get; set; }
        public class ContactDetails : FullNameAddressComments
        {
            public string? CONTACT_TYPE { get; set; }
            public string? CONTACT_MEDIUM { get; set; }
        }
        [Display(AutoGenerateField = false)]
        public ContactDetails[]? ASSOCIATED_CONTACT_DETAILS { get; set; }
        [Display(AutoGenerateField = false)]
        public Social[]? OTHER_SOCIAL_NETWORKS { get; set; }
        [Display(AutoGenerateField = false)]
        public bool? HELD_TOKEN_SALE { get; set; }
        public class TokenSale
        {
            public string? TOKEN_SALE_TYPE { get; set; }
            public long? TOKEN_SALE_DATE_START { get; set; }
            public long? TOKEN_SALE_DATE_END { get; set; }
            public string? TOKEN_SALE_DESCRIPTION { get; set; }
            public class TokenSaleTeamMember : FullNameAddressComments
            {
                public string? TYPE { get; set; }
            }
            public TokenSaleTeamMember[]? TOKEN_SALE_TEAM_MEMBERS { get; set; }
            public string? TOKEN_SALE_WEBSITE_URL { get; set; }
            public double? TOKEN_SALE_SUPPLY { get; set; }
            public double? TOKEN_SALE_RESERVE_SUPPLY { get; set; }
            public double? TOKEN_SALE_SUPPLY_ADDED { get; set; }
            public double? TOKEN_SALE_PRE_SALE_SUPPLY { get; set; }
            public string? TOKEN_SUPPLY_POST_SALE { get; set; }
            public string? TOKEN_SALE_PAYMENT_METHOD_TYPE { get; set; }
            public double? TOKEN_SALE_START_PRICE { get; set; }
            public string? TOKEN_SALE_START_PRICE_CURRENCY { get; set; }
            public double? TOKEN_SALE_FUNDING_CAP { get; set; }
            public string? TOKEN_SALE_FUNDING_CAP_CURRENCY { get; set; }
            public double? TOKEN_SALE_FUNDING_TARGET { get; set; }
            public string? TOKEN_SALE_FUNDING_TARGET_CURRENCY { get; set; }
            public class TokenSaleFundsRaised
            {
                public string? CURRENCY { get; set; }
                public double? TOTAL_VALUE { get; set; }
                public string? DESCRIPTION { get; set; }
            }
            public TokenSaleFundsRaised[]? TOKEN_SALE_FUNDS_RAISED { get; set; }
            double? TOKEN_SALE_FUNDS_RAISED_USD { get; set; }
            public class TokenSaleInvestorsSplit
            {
                public string? CATEGORY { get; set; }
                public double? TOTAL_TOKENS { get; set; }
                public string? DESCRIPTION { get; set; }
            }
            public TokenSaleInvestorsSplit[]? TOKEN_SALE_INVESTORS_SPLIT { get; set; }
            public class TokenSaleReserveSplit
            {
                public string? CATEGORY { get; set; }
                public double? TOTAL_TOKENS { get; set; }
                public string? ADDRESS { get; set; }
                public string? DESCRIPTION { get; set; }

            }
            public TokenSaleReserveSplit[]? TOKEN_SALE_RESERVE_SPLIT { get; set; }
            public TokenSaleInvestorsSplit[]? TOKEN_SALE_NOTABLE_INVESTORS { get; set; }
            public NameContainer[]? TOKEN_SALE_LAUNCHPADS { get; set; }
            public NameContainer[]? TOKEN_SALE_JURISDICTIONS { get; set; }
            public NameContainer[]? TOKEN_SALE_REGULATORY_FRAMEWORKS { get; set; }
            public NameContainer[]? TOKEN_SALE_LEGAL_ADVISERS { get; set; }
            public NameContainer[]? TOKEN_SALE_LEGAL_FORMS { get; set; }
            public class TokenSaleSecurityAuditCompany : NameContainer
            {
                public string? AUDIT_DOCUMENT { get; set; }
            }
            public TokenSaleSecurityAuditCompany[]? TOKEN_SALE_SECURITY_AUDIT_COMPANIES { get; set; }
        }
        [Display(AutoGenerateField = false)]
        public TokenSale[]? TOKEN_SALES { get; set; }
        [Display(AutoGenerateField = false)]
        public bool? HELD_EQUITY_SALE { get; set; }
        public class EquitySale
        {
            public string? EQUITY_SALE_STAGE { get; set; }
            public string? EQUITY_SALE_ENTITY_NAME { get; set; }
            public long? EQUITY_SALE_ANNOUNCEMENT_DATE { get; set; }
            public long? EQUITY_SALE_CLOSE_DATE { get; set; }
            public string? EQUITY_SALE_DESCRIPTION { get; set; }
            public class EquitySaleTeamMember : FullNameAddressComments
            {
                public string? JOB_TITLE { get; set; }
            }
            public EquitySaleTeamMember[]? EQUITY_SALE_TEAM_MEMBERS { get; set; }
            public string? EQUITY_SALE_ENTITY_URL { get; set; }
            public double? EQUITY_SALE_SUPPLY { get; set; }
            public double? TOTAL_EQUITY_SUPPLY_POST_RAISE { get; set; }
            public double? EQUITY_SALE_FUNDING_TARGET { get; set; }
            public string? EQUITY_SALE_FUNDING_TARGET_CURRENCY { get; set; }
            public class EquitySaleFundsRaised
            {
                public string? CURRENCY { get; set; }
                public double? TOTAL_VALUE { get; set; }
                public double? TOTAL_EQUITY { get; set; }
                public string? DESCRIPTION { get; set; }
            }
            public EquitySaleFundsRaised[]? EQUITY_SALE_FUNDS_RAISED { get; set; }
            public double? EQUITY_SALE_FUNDS_RAISED_USD { get; set; }
            public class EquitySaleNotableInvestor
            {
                public string? NAME { get; set; }
                public double? TOTAL_EQUITY_RECEIVED { get; set; }
                public double? INVESTMENT_VALUE { get; set; }
                public string? INVESTMENT_CURRENCY { get; set; }
                public bool? IS_LEAD_INVESTOR { get; set; }
                public string? DESCRIPTION { get; set; }
            }
            public EquitySaleNotableInvestor[]? EQUITY_SALE_NOTABLE_INVESTORS { get; set; }
            public NameContainer[]? EQUITY_SALE_JURISDICTIONS { get; set; }
            public NameContainer[]? EQUITY_SALE_REGULATORY_FRAMEWORKS { get; set; }
            public NameContainer[]? EQUITY_SALE_LEGAL_ADVISERS
            {
                get; set;
            }
            [Display(AutoGenerateField = false)]
            public EquitySale[]? EQUITY_SALES { get; set; }

                public class PriceConversionAsset
                {
                    public string? ID { get; set; }
                    public string? SYMBOL { get; set; }
                    public string? ASSET_TYPE { get; set; }
                }
                [Display(AutoGenerateField = false)]
                public PriceConversionAsset? PRICE_CONVERSION_ASSET { get; set; }

                public class ToplistBaseRank
                {
                    public long? CREATED_ON { get; set; }
                    public long? LAUNCH_DATE { get; set; }
                    public double? PRICE_USD { get; set; }
                    public double? CIRCULATING_MKT_CAP_USD { get; set; }
                    public double? TOTAL_MKT_CAP_USD { get; set; }
                    public double? SPOTMOVING_24_HOUR_QUOTE_VOLUME_TOP_TIER_DIRECT_USD { get; set; }
                    public double? SPOTMOVING_24_HOUR_QUOTE_VOLUME_DIRECT_USD { get; set; }
                    public double? SPOTMOVING_24_HOUR_QUOTE_VOLUME_TOP_TIER_USD { get; set; }
                    public double? SPOTMOVING_24_HOUR_QUOTE_VOLUME_USD { get; set; }
                    public double? SPOTMOVING_24_HOUR_CHANGE_USD { get; set; }
                    public double? SPOTMOVING_24_HOUR_CHANGE_PERCENTAGE_USD { get; set; }
                    public double? SPOTMOVING_7_DAY_QUOTE_VOLUME_TOP_TIER_DIRECT_USD { get; set; }
                    public double? SPOTMOVING_7_DAY_QUOTE_VOLUME_DIRECT_USD { get; set; }
                    public double? SPOTMOVING_7_DAY_QUOTE_VOLUME_TOP_TIER_USD { get; set; }
                    public double? SPOTMOVING_7_DAY_QUOTE_VOLUME_USD { get; set; }
                    public double? SPOTMOVING_7_DAY_CHANGE_USD { get; set; }
                    public double? SPOTMOVING_7_DAY_CHANGE_PERCENTAGE_USD { get; set; }
                    public double? SPOTMOVING_30_DAY_QUOTE_VOLUME_TOP_TIER_DIRECT_USD { get; set; }
                    public double? SPOTMOVING_30_DAY_QUOTE_VOLUME_DIRECT_USD { get; set; }
                    public double? SPOTMOVING_30_DAY_QUOTE_VOLUME_TOP_TIER_USD { get; set; }
                    public double? SPOTMOVING_30_DAY_QUOTE_VOLUME_USD { get; set; }
                    public double? SPOTMOVING_30_DAY_CHANGE_USD { get; set; }
                    public double? SPOTMOVING_30_DAY_CHANGE_PERCENTAGE_USD { get; set; }
                }
                public ToplistBaseRank? TOPLIST_BASE_RANK { get; set; }

        // internal fields not pullable
            public bool? IS_USED_IN_DEFI { get; set; }
            public bool? IS_USED_IN_NFT { get; set; }
         * 
         **/
        public class TData
        {
            public class Stats
            {
                public int PAGE { get; set; }
                public int PAGE_SIZE { get; set; }
                public int TOTAL_ASSETS { get; set; }
            }
            public required Stats STATS { get; set; }

            public required Item[] LIST { get; set; }
        }
        public TData? Data { get; set; }
        public required CoinDeskErr Err { get; set; }
    }
}
