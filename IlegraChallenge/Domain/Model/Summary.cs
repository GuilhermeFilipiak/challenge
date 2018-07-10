using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IlegraChallenge.Domain.Model
{
    public class Summary
    {
        public int AmountOfClients { get; set; }
        public int AmountOfSalesman { get; set; }
        public string MostExpensiveSaleId { get; set; }
        public string WorstSalesmanEver { get; set; }
    }
}
