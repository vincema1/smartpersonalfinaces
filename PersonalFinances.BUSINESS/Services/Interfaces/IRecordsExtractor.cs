using PersonalFinances.BUSINESS.Models;
using PersonalFinances.DATA.DataModel;
using System.Collections.Generic;

namespace PersonalFinances.BUSINESS.Services.Interfaces
{
    public interface IRecordsExtractor
    {
        ExtractionResult ProcessSource(string filePath); 
            
    }
    

}
