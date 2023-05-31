using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Items.DB.Entities
{
    public class DtoItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string Flavor { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}
