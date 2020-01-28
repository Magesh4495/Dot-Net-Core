using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace Entity.Helpers
{
    public interface IBaseDataAccess
    {
        SqlConnection GetConnection();
        DbCommand GetCommand(DbConnection connection, string commandText, CommandType commandType);
        SqlParameter GetParameter(string parameter, object value);
        SqlParameter GetParameterOut(string parameter, SqlDbType type, object value = null, ParameterDirection parameterDirection = ParameterDirection.InputOutput);
        int ExecuteNonQuery(string procedureName, List<DbParameter> parameters, CommandType commandType = CommandType.StoredProcedure);
        object ExecuteScalar(string procedureName, List<SqlParameter> parameters);
        DbDataReader GetDataReader(string procedureName, List<DbParameter> parameters, CommandType commandType = CommandType.StoredProcedure);
    }
}
