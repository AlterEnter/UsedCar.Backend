using System;
using System.ComponentModel.DataAnnotations;
using UsedCar.Backend.Presentations.Functions.Users.Validations;

namespace UsedCar.Backend.Presentations.Functions.Users.Models
{
    public class UserUpdateRequest : IDoValidation
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string DisplayName { get; set;} = string.Empty;
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
        public string MailAddress { get; set;} = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string Zip { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Street1 { get; set; } = string.Empty;
        public string Street2 { get; set; } = string.Empty;

        /// <summary>
        /// 必須項目の妥当性確認
        /// </summary>
        /// <returns></returns>
        public bool IsFilledRequired()
        {
            if (string.IsNullOrWhiteSpace(FirstName) 
                && string.IsNullOrWhiteSpace(LastName)
                && string.IsNullOrWhiteSpace(MailAddress)
                && string.IsNullOrWhiteSpace(Zip)
                && string.IsNullOrWhiteSpace(State)
                && string.IsNullOrWhiteSpace(City)
                && string.IsNullOrWhiteSpace(Street1)
                && string.IsNullOrWhiteSpace(Street2)
                && string.IsNullOrWhiteSpace(PhoneNumber))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// DateTimeの妥当性確認
        /// </summary>
        /// <returns></returns>
        public bool IsUtc()
        {
            if (DateOfBirth.Kind != DateTimeKind.Utc)
            {
                return false;
            }

            return true;
        }
    }
}
