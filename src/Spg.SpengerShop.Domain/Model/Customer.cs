using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Domain.Model
{
    public enum GenderTypes { NA = 0, FEMALE = 1, MALE = 2 }

    public class Customer
    {
        public int Id { get; private set; }
        public GenderTypes Gender { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string EMail { get; set; } = string.Empty;
        public Guid Guid { get; set; }
        public DateTime? RegistrationDateTime { get; set; }
        public PhoneNumber? PhoneNumber { get; set; }
        public string SocialSecurityNumber { get; set; } = string.Empty;

        private List<ShoppingCart> _schoppingCarts = new();
        public IReadOnlyList<ShoppingCart> ShoppingCarts => _schoppingCarts;

        public Customer()
        { }

        public Customer(GenderTypes gender, string firstName, string lastName, string eMail, Guid guid, DateTime? registrationDateTime)
        {
            Gender = gender;
            FirstName = firstName;
            LastName = lastName;
            EMail = eMail;
            Guid = guid;
            RegistrationDateTime = registrationDateTime;
        }

        public void AddShoppingCart(ShoppingCart entity)
        {
            _schoppingCarts.Add(entity);
        }
    }
}
