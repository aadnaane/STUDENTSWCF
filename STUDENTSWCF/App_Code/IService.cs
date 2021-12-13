using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService" in both code and config file together.
[ServiceContract]
public interface IService
{

	[OperationContract]
	string GetData(int value);

	[OperationContract]
	CompositeType GetDataUsingDataContract(CompositeType composite);

	[OperationContract]
	string AddStudentsRecord(Student std);

	[OperationContract]
	DataSet GetStudentsRecords();

	[OperationContract]
	string DeleteRecords(Student std);

	[OperationContract]
	DataSet SearchStudentsRecord(Student std);

	[OperationContract]
	string UpdateStudentsContact(Student std);

	// TODO: Add your service operations here
}

// Use a data contract as illustrated in the sample below to add composite types to service operations.
[DataContract]
public class CompositeType
{
	bool boolValue = true;
	string stringValue = "Hello ";

	[DataMember]
	public bool BoolValue
	{
		get { return boolValue; }
		set { boolValue = value; }
	}

	[DataMember]
	public string StringValue
	{
		get { return stringValue; }
		set { stringValue = value; }
	}
}

[DataContract]
public class Student
{

    string _stdID = "";
    string _name = "";
    string _email = "";
    string _phone = "";
    string _gender = "";

    [DataMember]
    public string StdID
    {
        get { return _stdID; }
        set { _stdID = value; }
    }

    [DataMember]
    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    [DataMember]
    public string Email
    {
        get { return _email; }
        set { _email = value; }
    }

    [DataMember]
    public string Phone
    {
        get { return _phone; }
        set { _phone = value; }
    }

    [DataMember]
    public string Gender
    {
        get { return _gender; }
        set { _gender = value; }
    }
}