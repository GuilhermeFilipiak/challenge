using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IlegraChallenge.Business
{
    public class Extract
    {
        /// <summary>
        /// GET Data Files in Directory
        /// </summary>
        /// <param name="path">Directory Ex.: C:\temp\</param>
        /// <returns></returns>
        public IEnumerable<string> GetDataFiles(string path)
        {
            if (Directory.Exists(path))
            {
                return Directory.EnumerateFiles(path, "*.dat", SearchOption.AllDirectories);
            }
            return null;
        }

        /// <summary>
        /// Get Complete List of String Lines
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public IEnumerable<string> GetDataLines(string fileName)
        {
            return GetDataLines(fileName, null);
        }

        /// <summary>
        /// GET List of String Lines By Id
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="searchById"></param>
        /// <returns></returns>
        public IEnumerable<string> GetDataLines(string fileName, string searchById)
        {
            if (File.Exists(fileName))
            {
                if (!string.IsNullOrEmpty(searchById))
                {
                    return from line in File.ReadLines(fileName)
                           where line.Substring(0, 3) == searchById
                           select line;
                }
                else
                {
                    return from line in File.ReadLines(fileName)
                           select line;
                }
            }
            return null;
        }

        /// <summary>
        /// Filter List of String in Lines By Id
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<string> FilterLinesById(IEnumerable<string> collection, string id)
        {
            return from line in collection
                   where line.Substring(0, 3) == id
                   select line;
        }
    }
}
