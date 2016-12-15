using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Invoice.Site.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class MaxFileLengthAttribute : ValidationAttribute, IClientValidatable
    {
        private const string ErrorMessageDefault = "Maximum file size is: {0} mb";
        public int MaxLength { get; set; }

        public MaxFileLengthAttribute(int lengthMb)
        {
            MaxLength = lengthMb * 1024 * 1024;
            ErrorMessage = string.Format(ErrorMessageDefault, lengthMb);
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ValidationType = Invoice.Definitions.Consts.ClientValidationType.FILE_LENGTH,
                ErrorMessage = ErrorMessageString
            };

            rule.ValidationParameters.Add(Invoice.Definitions.Consts.ClientValidationParam.FILE_MAX_LENGTH, MaxLength);
            yield return rule;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            HttpPostedFileBase file = value as HttpPostedFileBase;
            if (file != null)
            {
                var isValid = file.ContentLength <= MaxLength;
                if (!isValid)
                {
                    return new ValidationResult(ErrorMessageString);
                }
            }
            return ValidationResult.Success;
        }
    }
}