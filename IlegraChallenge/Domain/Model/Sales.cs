using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IlegraChallenge.Domain.Model
{
    public class Sales
    {
        public string Id { get; set; }
        public string SaleId { get; set; }
        public string SalesmanName { get; set; }
        public IEnumerable<Item> Items { get; set; }
    }
}
