using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IlegraChallenge.Domain.Model
{
    public class Customer
    {
        public string Id { get; set; }
        public string CNPJ { get; set; }
        public string Name { get; set; }
        public string BusinessArea { get; set; }
    }
}
