using Spg.SpengerShop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Domain.Model
{
    public enum ShoppingCartStates { Active = 0, Sent = 1, Unknown = 99 }

    public class ShoppingCart : EntityBase, IFindableByGuid
    {
        public Guid Guid { get; private set; }
        public string Name { get; set; } = string.Empty;
        public ShoppingCartStates ShoppingCartState { get; set; }
        public DateTime CreationDate { get; private set; }
        public int ItemsCount { get; private set; }
        public decimal Summary { get; private set; }

        public int CustomerNavigationId { get; set; }
        public virtual Customer CustomerNavigation { get; set; } = default!;

        private List<ShoppingCartItem> _shoppingCartItems = new();
        public virtual IReadOnlyList<ShoppingCartItem> ShoppingCartItems => _shoppingCartItems;

        protected ShoppingCart()
        { }
        public ShoppingCart(string name, ShoppingCartStates shoppingCartState, DateTime creationDate, Customer customer, Guid guid)
        {
            Name = name;
            ShoppingCartState = shoppingCartState;
            CreationDate = creationDate;
            Guid = guid;
            CustomerNavigation = customer;
        }

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
