using RDotNet;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistical.R
{


    public class RNet
    {
        private REngine engine;

        public REngine MyProperty
        {
            get { return engine; }
            set { engine = value; }
        }


        public RNet(REngine engine)
        {
            this.engine = engine;
        }

        #region .     R Functions     .

        public void Setwd(string path)
        {
            try
            {
                this.engine.Evaluate(string.Format("setwd(\"{0}\");", path.Replace(@"\", @"/")));
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("The R setwd function failed: {0}", e.Message));
            }
        }

        public string GetWd()
        {
            try
            {
                string[] currentDir = this.engine.Evaluate("getwd();").AsCharacter().ToArray();
                return string.Join("",currentDir);
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("The R getwd function failed: {0}", e.Message));
            }
        }

        public void Source(string script)
        {
            try
            {
                this.engine.Evaluate(string.Format("source(\"{0}\");", script));
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("The R source function failed: {0}", e.Message));
            }
        }

        #endregion

        #region .     Custom Functions    .

        /// <summary>
        /// Method for converting the DataFrame to a DataTable object.
        /// </summary>
        /// <param name="dataset">The DataFrame to be converted.</param>
        /// <returns>DataTable object.</returns>
        public static DataTable DataFrameToDataTable(DataFrame dataset)
        {
            DataTable datatbl = new DataTable();

            if (dataset != null && dataset.RowCount > 0)
            {
                try
                {
                    for (int index = 0; index < dataset.ColumnCount; index++)
                    {
                        string colname = dataset.ColumnNames[index];
                        Type colType = dataset[index][0].GetType();

                        datatbl.Columns.Add(new DataColumn(colname, colType));
                    }

                    DataRow datarow;

                    for (int rowindex = 0; rowindex < dataset.RowCount; rowindex++)
                    {
                        datarow = datatbl.NewRow();

                        for (int index = 0; index < datatbl.Columns.Count; index++)
                        {
                            try
                            {
                                datarow[index] = dataset[index][rowindex];
                            }
                            catch (ArgumentOutOfRangeException)
                            {
                                //continue
                            }
                            catch (IndexOutOfRangeException)
                            {
                                //continue
                            }
                        }

                        datatbl.Rows.Add(datarow);
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(string.Format("Not able to convert the DataFrame to DataTable: {0}", e.Message));
                }
            }

            return datatbl;
        }

        /// <summary>
        /// Method that returns all the information (columns and rows values) of the Dataframe as a unique string.
        /// </summary>
        /// <param name="dataset">The DataFrame object for extracting the raw data.</param>
        /// <param name="delimiter">The separator character for the  values. (Default is semicolon).</param>
        /// <returns>A string containing the data.</returns>
        public static string DataFrameToRawData(DataFrame dataset, string delimiter = ";")
        {
            StringBuilder strB = new StringBuilder();
            string columns = string.Empty;

            try
            {
                for (int rowindex = 0; rowindex < dataset.RowCount; rowindex++)
                {
                    string[] columnValues = new string[dataset.ColumnCount];

                    for (int index = 0; index < dataset.ColumnCount; index++)
                    {
                        try
                        {
                            columnValues[index] += dataset[index][rowindex].ToString();
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            columnValues[index] = string.Empty;
                        }
                        catch (IndexOutOfRangeException)
                        {
                            columnValues[index] = string.Empty;
                        }
                    }

                    //Add row
                    strB.AppendLine(string.Join(delimiter, columnValues));
                }
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("Not able to generate the string visualization of the DataFrame: {0}", e.Message));
            }

            //Add header
            strB.Insert(0, string.Join(delimiter, dataset.ColumnNames) + Environment.NewLine);

            return strB.ToString();
        }

        #endregion

        /// <summary>
        /// Method to help troubleshooting during development. Not part of the solution. 
        /// </summary>
        /// <param name="root"></param>
        /// <param name="indent"></param>
        public static void InspectActivity(Activity root, int indent)
        {
            // Inspect the activity tree using WorkflowInspectionServices.
            IEnumerator<Activity> activities =
                WorkflowInspectionServices.GetActivities(root).GetEnumerator();

            Console.WriteLine("{0}{1}", new string(' ', indent), root.DisplayName);

            while (activities.MoveNext())
            {
                InspectActivity(activities.Current, indent + 2);
            }
        }
    }       
}
