using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in code, svc and config file together.
public class Service : IService
{

	public CompositeType GetDataUsingDataContract(CompositeType composite)
	{
		if (composite == null)
		{
			throw new ArgumentNullException("composite");
		}
		if (composite.BoolValue)
		{
			composite.StringValue += "Suffix";
		}
		return composite;
	}
    private String ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\ACHAHBAR\\AppData\\Local\\Microsoft\\Microsoft SQL Server Local DB\\Instances\\mssqllocaldb\\Enseignement.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";


    public string GetData(int value)
    {
        return string.Format("You entered: {0}", value);
    }

    //C- Add Employee Record  
    public string AddStudentsRecord(Student std)
    {
        string result = "";
        try
        {

            SqlConnection con = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand();

            string Query = @"INSERT INTO tblStudents (StdID,Name,Email,Phone,Gender)  
                                               Values(@StdID,@Name,@Email,@Phone,@Gender)";

            cmd = new SqlCommand(Query, con);
            cmd.Parameters.AddWithValue("@StdID", std.StdID);
            cmd.Parameters.AddWithValue("@Name", std.Name);
            cmd.Parameters.AddWithValue("@Email", std.Email);
            cmd.Parameters.AddWithValue("@Phone", std.Phone);
            cmd.Parameters.AddWithValue("@Gender", std.Gender);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            result = "Record Added Successfully !";
        }
        catch (FaultException fex)
        {
            result = "Error";
        }

        return result;
    }

    //Retrieve Data  
    //Retrive Record  
    public DataSet GetStudentsRecords()
    {
        DataSet ds = new DataSet();
        try
        {

            SqlConnection con = new SqlConnection(ConnectionString);
            string Query = "SELECT * FROM tblStudents";

            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            sda.Fill(ds);
        }
        catch (FaultException fex)
        {
            throw new FaultException<string>("Error: " + fex);
        }

        return ds;
    }

    //Delete Record  
    public string DeleteRecords(Student std)
    {
        string result = "";
        SqlConnection con = new SqlConnection(ConnectionString);
        SqlCommand cmd = new SqlCommand();
        string Query = "DELETE FROM tblStudents Where StdID=@StdID";
        cmd = new SqlCommand(Query, con);
        cmd.Parameters.AddWithValue("@StdID", std.StdID);
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        result = "Record Deleted Successfully!";
        return result;
    }

    //Search Employee Record  
    public DataSet SearchStudentsRecord(Student std)
    {
        DataSet ds = new DataSet();
        try
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            string Query = "SELECT * FROM tblStudents WHERE StdID=@StdID";

            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            sda.SelectCommand.Parameters.AddWithValue("@StdID", std.StdID);
            sda.Fill(ds);
        }
        catch (FaultException fex)
        {
            throw new FaultException<string>("Error:  " + fex);
        }
        return ds;
    }

    //UPDATE RECORDS  
    //Update by Phone Roll   
    public string UpdateStudentsContact(Student std)
    {
        string result = "";
        SqlConnection con = new SqlConnection(ConnectionString);
        SqlCommand cmd = new SqlCommand();

        string Query = "UPDATE tblStudents SET Email=@Email,Phone=@Phone WHERE StdID=@StdID";

        cmd = new SqlCommand(Query, con);
        cmd.Parameters.AddWithValue("@StdID", std.StdID);
        cmd.Parameters.AddWithValue("@Email", std.Email);
        cmd.Parameters.AddWithValue("@Phone", std.Phone);
        con.Open();
        cmd.ExecuteNonQuery();
        result = "Record Updated Successfully !";
        con.Close();

        return result;
    }

}
