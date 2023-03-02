using Spg.SpengerShop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Domain.Model
{
    public enum Genders
    {
        Male = 0,
        Female = 1,
        Other = 2
    }

    public class Customer : EntityBase, IFindableByGuid
    {
        public Guid Guid { get; private set; }
        public Genders Gender { get; set; }
        public long CustomerNumber { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string EMail { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public DateTime RegistrationDateTime { get; set; }
        public Address? Address { get; set; } = default!;
        public string? PhoneNumber { get; set; }

        private List<ShoppingCart> _shoppingCarts = new();
        public virtual IReadOnlyList<ShoppingCart> ShoppingCarts => _shoppingCarts;

        public Customer()
        { }
        public Customer(
            Guid guid,
            Genders gender,
            long customerNumber,
            string firstName,
            string lastName,
            string eMail,
            DateTime birthDate,
            DateTime registrationDateTime,
            Address? Address)
        {
            Guid = guid;
            Gender = gender;
            CustomerNumber = customerNumber;
            FirstName = firstName;
            LastName = lastName;
            EMail = eMail;
            BirthDate = birthDate;
            RegistrationDateTime = registrationDateTime;
            Address = Address;
        }

        public void AddShoppingCart(ShoppingCart entity)
        {
            _shoppingCarts.Add(entity);
        }
    }
}
