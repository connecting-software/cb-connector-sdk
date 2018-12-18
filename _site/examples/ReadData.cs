public void ReadData(IResultSetLoader loader, IExecutionContext context, string connectionString, string queryString)
{
			using (var conn = new SqlConnection(connectionString))
			{
			    conn.Open();
			    using (SqlCommand sqlcommand = new SqlCommand(queryString, conn))
			    {
			        //Retrieves a list of the variables (i.e. @ parameters).
			        List<Variable> variables = context.GetAllVariables().ToList();
			        variables.ForEach(v =>
			        {
			            sqlcommand.Parameters.Add(new SqlParameter(v.Name, context.GetVariableValue(v)));
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