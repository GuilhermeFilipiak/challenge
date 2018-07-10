using IlegraChallenge.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IlegraChallenge.Business
{
    public class Convert
    {
        /// <summary>
        /// Convert Line Text to Customer Model
        /// </summary>
        /// <param name="lineText"></param>
        /// <returns></returns>
        public Customer ToCustomerModel(string lineText)
        {
            var arr = lineText.Split('ç');
            //customer logic
            return new Customer()
            {
                Id = arr[0],
                CNPJ = arr[1],
                Name = arr[2],
                BusinessArea = arr[3]
            };
        }

        /// <summary>
        /// Convert Line Text to Salesman Model
        /// </summary>
        /// <param name="lineText"></param>
        /// <returns></returns>
        public Salesman ToSalesmanModel(string lineText)
        {
            var arr = lineText.Split('ç');
            //salesman logic
            return new Salesman()
            {
                Id = arr[0],
                CPF = arr[1],
                Name = arr[2],
                Salary = decimal.Parse(arr[3])
            };
        }

        /// <summary>
        /// Convert Line Text to Sale Model
        /// </summary>
        /// <param name="lineText"></param>
        /// <returns></returns>
        public Sales ToSaleModel(string lineText)
        {
            //sale logic
            var arr = lineText.Split('ç');

            var model = new Sales()
            {
                Id = arr[0],
                SaleId = arr[1],
                SalesmanName = arr[3]
            };

            //add items
            List<Item> items = new List<Item>();

            var arrItem = arr[2].Replace("[", "").Replace("]", "").Split(',');
            var countItem = arrItem[0].Split('-');

            for (int i = 0; i < countItem.Length; i++)
            {
                var item = arrItem[i].Split('-');

                if (!items.Any(x => x.Id == item[0]))
                {
                    items.Add(new Item()
                    {
                        Id = item[0],
                        Quantity = decimal.Parse(item[1]),
                        Price = decimal.Parse(item[2])
                    });
                }
            }

            model.Items = items;

            return model;
        }
    }
}
