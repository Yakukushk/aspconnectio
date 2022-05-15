using WebApplication2.Models;
using System.Collections.Generic;

namespace WebApplication2.ModelView
{
    public interface IExcel
    {
        List<Excels> GetExcels();
        List<Excels> SaveExcels(List<Excels> excels);

    }
}
