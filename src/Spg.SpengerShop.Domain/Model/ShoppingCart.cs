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

        public int Id { get; private set; }
        public Guid Guid { get; private set; }
        public States State { get; private set; }

        // Wird zu FK in der Datenbank
        public virtual int CustomerNavigationId { get; set; }
        public virtual Customer CustomerNavigation { get; set; } = default!;


        private List<ShoppingCartItem> _shoppingCartItems = new();
        public IReadOnlyList<ShoppingCartItem> ShoppingCartItems => _shoppingCartItems;

        public void AddShoppingCartItem(ShoppingCartItem entity)
        {
            // * Stock-Anzahl verändern
            // * Verfügbarkeit prüfen???
            // * Pieces in Cart inkrementiern
            if (entity is not null)
            {
                if (entity.Pieces <= entity.ProductNavigation.Stock) // UnitTest
                {
                    entity.ProductNavigation.Stock = entity.ProductNavigation.Stock - entity.Pieces; // UnitTest
                    try
                    {
                        ShoppingCartItem? existingShoppinCartItem = _shoppingCartItems
                            .SingleOrDefault(s => s.ProductNavigation.Name == entity.ProductNavigation.Name);
                        if (existingShoppinCartItem is not null)
                        {
                            existingShoppinCartItem.Pieces = existingShoppinCartItem.Pieces + entity.Pieces; // UnitTest
                        }
                        else
                        {
                            _shoppingCartItems.Add(entity); // UnitTest
                        }
                    }
                    catch (InvalidOperationException ex)  // UnitTest
                    {
                        // Exception
                    }
                }
                else  // UnitTest
                {
                    // Exception...
                }
            }
        }

        //public void RemoveShoppingCartItem(ShoppingCartItem entity)
        //{
        //    if (entity is not null)
        //    {
        //        _shoppingCartItems.Remove(entity);
        //    }
        //}
    }
}
