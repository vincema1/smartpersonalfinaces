using PersonalFinances.BUSINESS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinances.BUSINESS.Services.Interfaces
{
    public interface IImportProcessor
    {
        ImportReport Process(int dossierId,string filePath, string fileName);

    }
}
