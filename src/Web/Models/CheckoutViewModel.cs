using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
	public class CheckoutViewModel
	{
		[Required(ErrorMessage = "{0} is required."), EmailAddress]
		public string Email { get; set; }
		[Required(ErrorMessage = "{0} is required."), MaxLength(90), Display(Name = "Credit Card Owner")]
		public string CCOwner { get; set; }
		[Required(ErrorMessage = "{0} is required."), MaxLength(16), Display(Name = "Credit Card Number")]
		public string CCNumber { get; set; }
		[Required(ErrorMessage = "{0} is required."), MaxLength(5), Display(Name = "Expiration Date")]
		public string CCExpiration { get; set; }
		[Required(ErrorMessage = "{0} is required."), MaxLength(4), Display(Name = "CCV")]
		public string CCCvv { get; set; }
	}
}