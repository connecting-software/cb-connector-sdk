    public class Example
    {
        #region Example

        public void ReadData(IResultSetLoader loader, IExecutionContext ctx, string connStr, string query)
        {
            using (var conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (SqlCommand sqlcommand = new SqlCommand(query, conn))
                {
                    //Retrieves a list of the variables (i.e. @ parameters).
                    List<Variable> variables = ctx.GetAllVariables().ToList();
                    variables.ForEach(v =>
                    {
                        sqlcommand.Parameters.Add(new SqlParameter(v.Name, ctx.GetVariableValue(v)));
                    });
                    var reader = sqlcommand.ExecuteReader();
                    //Opens an ITableResultLoader object for writing. 
                    using (ITableResultLoader tableLoader = loader.OpenTableResultLoader(ResultColumns))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read() &&
                                        !tableLoader.CancellationToken.IsCancellationRequested)
                            {
                                //Create a new ITableResultRow instance
                                ITableResultRow row = tableLoader.NewRow();
                                //Gets the values in the row, one per column.
                                reader.GetValues(row.GetValues());
                                //Adds a row to the ITableResultLoader.
                                tableLoader.Add(row);
                            }
                            reader.Close();
                        }
                    }
                }
            }
        }

        #endregion
    }




