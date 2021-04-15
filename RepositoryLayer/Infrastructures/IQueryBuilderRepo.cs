using DatabaseTutor.DTOs;
using DatabaseTutor.DTOs.RequestDTOs.StudentQuery;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Infrastructures
{
    public interface IQueryBuilderRepo
    {
        ResponseDTO<dynamic> GetDatabaseAsLookup();
        ResponseDTO<dynamic> GetTableAsLookup(string DatabaseName);
        ResponseDTO<dynamic> GetTableColumnsAsLookup(string DatabaseName,string TableName);
        ResponseDTO<dynamic> ExecuteQuery(ExecuteQueryRequestDTO requestQuery);
    }
}
