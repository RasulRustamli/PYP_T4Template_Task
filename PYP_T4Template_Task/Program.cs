using System.Data.SqlClient;
using System.IO;
using System.Reflection;

const string connection = "Server=DESKTOP-FHK353D;Database=Northwind;Trusted_Connection=True;MultipleActiveResultSets=true";
SqlConnection con = new SqlConnection(connection);


SqlCommand cmd = new SqlCommand(@"select TABLE_NAME from INFORMATION_SCHEMA.TABLES WHERE 

    TABLE_TYPE = 'BASE TABLE'", con);
if (con.State == System.Data.ConnectionState.Closed)
    con.Open();

var dr = cmd.ExecuteReader();


string path = @"C:\Users\rasul\OneDrive\Masaüstü\PYP_T4Template_Task\PYP_T4Template_Task\Models";
Directory.CreateDirectory(Path.Combine(path));
DirectoryInfo info = new DirectoryInfo(path);
FileInfo[] files= info.GetFiles("*.cs");
if(files.Length == 0)
{
    
    while (dr.Read())
    {
        var templateClass = new string[]
    {
        "using System;",
        "using System.Text;",
        $"namespace {Assembly.GetExecutingAssembly().GetName().Name}.Models;",
        $"public class {dr["TABLE_NAME"].ToString().Replace(" ","")}"+"{}"
    };
        string modelClass = $@"{path}\{dr["TABLE_NAME"]}.cs";
         File.WriteAllLines(modelClass, templateClass);
    }
}

Console.WriteLine(Assembly.GetExecutingAssembly().GetName().Name);
var c = Assembly.GetExecutingAssembly();

Console.WriteLine("dasdsasdadsa");