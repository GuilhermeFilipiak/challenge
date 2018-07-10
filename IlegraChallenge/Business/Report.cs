using IlegraChallenge.Domain.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IlegraChallenge.Business
{
    public class Report
    {
        public string dirIn { get; set; }
        /// <summary>
        /// My Constructor
        /// </summary>
        /// <param name="directoryDatFiles">Directory Dat Files</param>
        public Report(string directoryDatFiles)
        {
            dirIn = directoryDatFiles;
        }

        /// <summary>
        /// Generate summary Report in Output Directory
        /// </summary>
        /// <param name="outPutDirectory"></param>
        public void GenerateDatFilesSummary(string outPutDirectory)
        {
            //generate summary logic
            var extract = new Extract();
            var convert = new Convert();

            var files = extract.GetDataFiles(dirIn);
            var pathDoneFile = Path.Combine(outPutDirectory, "summary.done.dat");

            //Create File
            if (!File.Exists(pathDoneFile))
            {
                File.Create(pathDoneFile).Dispose();
            }

            if (files != null && files.Count() > 0)
            {
                var listCustomer = new List<Customer>();
                var listSalesman = new List<Salesman>();
                var listSales = new List<Sales>();

                foreach (var file in files)
                {
                    var lines = extract.GetDataLines(file);

                    if (lines != null && lines.Count() > 0)
                    {
                        foreach (var item in extract.FilterLinesById(lines, Constants.CustomerId).Select(x => convert.ToCustomerModel(x)))
                        {
                            if (!listCustomer.Any(x => x.CNPJ == item.CNPJ))
                            {
                                listCustomer.Add(item);
                            }
                        }

                        foreach (var item in extract.FilterLinesById(lines, Constants.SalesmanId).Select(x => convert.ToSalesmanModel(x)))
                        {
                            if (!listSalesman.Any(x => x.CPF == item.CPF))
                            {
                                listSalesman.Add(item);
                            }
                        }

                        foreach (var item in extract.FilterLinesById(lines, Constants.SalesId).Select(x => convert.ToSaleModel(x)))
                        {
                            if (!listSales.Any(x => x.SaleId == item.SaleId))
                            {
                                listSales.Add(item);
                            }
                        }
                    }

                    var summary = new Summary()
                    {
                        AmountOfClients = listCustomer.Count(),
                        AmountOfSalesman = listSalesman.Count(),
                        MostExpensiveSaleId = listSales.Select(x => new { SaleId = x.SaleId, Total = x.Items.Sum(z => z.Price * z.Quantity) }).OrderByDescending(x => x.Total).FirstOrDefault().SaleId,
                        WorstSalesmanEver = listSales.Select(x => new { SaleId = x.SaleId, SalesmanName = x.SalesmanName, Total = x.Items.Sum(z => z.Price * z.Quantity) }).OrderBy(x => x.Total).FirstOrDefault().SalesmanName
                    };

                    //Write File
                    FileInfo fi = new FileInfo(pathDoneFile);
                    using (TextWriter tw = new StreamWriter(fi.Open(FileMode.Truncate)))
                    {
                        tw.WriteLine($"● ID of the most expensive sale: {summary.MostExpensiveSaleId}");
                        tw.WriteLine($"● Amount of clients in the input file: {summary.AmountOfClients}");
                        tw.WriteLine($"● Amount of salesman in the input file: {summary.AmountOfSalesman}");
                        tw.WriteLine($"● Worst salesman ever: {summary.WorstSalesmanEver}");
                        tw.Close();
                    }
                }
            }
            else
            {
                //Write File
                FileInfo fi = new FileInfo(pathDoneFile);
                using (TextWriter tw = new StreamWriter(fi.Open(FileMode.Truncate)))
                {
                    tw.WriteLine($"● ID of the most expensive sale: #");
                    tw.WriteLine($"● Amount of clients in the input file: #");
                    tw.WriteLine($"● Amount of salesman in the input file: #");
                    tw.WriteLine($"● Worst salesman ever: #");
                    tw.Close();
                }
            }
        }
    }
}
