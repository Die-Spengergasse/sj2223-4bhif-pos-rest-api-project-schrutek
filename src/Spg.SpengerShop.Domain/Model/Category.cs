using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Domain.Model
{
    public enum CategoryTypes { Food, Dress, Work, Electronics, Sports, Other }
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public CategoryTypes CategoryType { get; set; }
    }
}
