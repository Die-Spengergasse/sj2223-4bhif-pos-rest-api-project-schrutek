using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Domain.Model
{
    public enum States { ACTIVE = 0, INPAYMENT, SENT }

    public class ShoppingCart
    {
        public ShoppingCart(States state, Guid guid)
        {
            State = state;
            Guid = guid;
        }

        public int Id { get; set; }
        public States State { get; set; }
        public Guid Guid { get; set; }

        // Wird zu FK in der Datenbank
        public int CustomerNavigationId { get; set; }
        public Customer CustomerNavigation { get; set; } = default!;


        //private List<ShoppingCartItem> _shoppingCartItems = new();
        //public IReadOnlyList<ShoppingCartItem> ShoppingCartItems => _shoppingCartItems;

        //public void AddShoppingCartItem(ShoppingCartItem entity)
        //{
        //    // * Stock-Anzahl verändern
        //    // * Verfügbarkeit prüfen
        //    // * ...
        //    if (entity is not null)
        //    {
        //        _shoppingCartItems.Add(entity);
        //    }
        //}

        //public void RemoveShoppingCartItem(ShoppingCartItem entity)
        //{
        //    if (entity is not null)
        //    {
        //        _shoppingCartItems.Remove(entity);
        //    }
        //}
    }
}
