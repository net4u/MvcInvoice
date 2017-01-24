using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Definitions
{
    public static class Consts
    {
        public static class ViewDataType
        {
            public const string SEARCH_MODEL = "SearchModel";
            public const string TOOLBAR_MODEL = "ToolbalModel";
        }

        public static class Param
        {
            public const string BANK_CURRENCY_EXCHANGE_URL = "BANK_CURRENCY_EXCHANGE_URL";
        }

        public static class ClientValidationType
        {
            public const string FILE_TYPE = "filetype";
            public static string FILE_LENGTH = "filelength";
        }

        public static class ClientValidationParam
        {
            public const string FILE_VALID_TYPES = "validtypes";
            public const string FILE_MAX_LENGTH = "maxlength";
        }

        public static class MvcPattern
        {
            public const string MODEL = "model";
            public const string VIEW = "view";
            public const string CONTROLLER = "controller";
            public const string ACTION = "action";
        }

        public static class ErrorViewName
        {
            public const string Error = "Error";
        }
    }
}
