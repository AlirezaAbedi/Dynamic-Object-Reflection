// See https://aka.ms/new-console-template for more information
using CompareTwoClassObjects;

var source = new List<Employee>
{
    new Employee { Id = 1, Name = "Ali", HireDate = new DateTime(2022,1,1) },
    new Employee { Id = 2, Name = "Sara", HireDate = new DateTime(2023,1,1) },
    new Employee { Id = 3, Name = "hasan", HireDate = new DateTime(2023,1,1) },
};

var destination = new List<Employee>
{
    new Employee { Id = 1, Name = "Ali", HireDate = new DateTime(2023,1,1) }
};

var result = ReflectionObject.CompareByPropertyValue(source, destination, "Name");

var NewItem = result[CompareResult.NewItem];
var UpdatedItem = result[CompareResult.UpdatedItem];

Console.Read();

