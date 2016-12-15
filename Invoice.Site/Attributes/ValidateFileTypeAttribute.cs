using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Invoice.Site.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ValidateFileTypeAttribute : ValidationAttribute, IClientValidatable
    {
        private const string ErrorMessageDefault = "Only these types are valid: {0}";
        private IEnumerable<string> ValidTypes { get; set; }

        public ValidateFileTypeAttribute(string validTypes)
        {
            ValidTypes = validTypes.ToLower().Split(',');
            ErrorMessage = string.Format(ErrorMessageDefault, validTypes);
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ValidationType = "filetype",
                ErrorMessage = ErrorMessageString
            };
            rule.ValidationParameters.Add("validtypes", string.Join(",", ValidTypes));
            yield return rule;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            HttpPostedFileBase file = value as HttpPostedFileBase;
            if (file != null)
            {
                var isValid = ValidTypes.Any(e => file.FileName.ToLower().EndsWith(e));
                if (!isValid)
                {
                    return new ValidationResult(ErrorMessageString);
                }
            }
            return ValidationResult.Success;
        }
    }
}