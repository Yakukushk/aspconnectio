using WebApplication2.Models;
using EFCore.BulkExtensions;

namespace WebApplication2.ModelView
{
    public class ExcelCollection : IExcel
    {
        DBLibraryContext _dbLibraryContext = null;

        public ExcelCollection(DBLibraryContext dbLibraryContext) {
            _dbLibraryContext = dbLibraryContext;
        }
        
        public List<Excels> GetExcels()
        {
            return _dbLibraryContext.Excels.ToList();
        }

        public List<Excels> SaveExcels(List<Excels> excels)
        {
            _dbLibraryContext.BulkInsert(excels);
            return excels;
        }
    }
}
