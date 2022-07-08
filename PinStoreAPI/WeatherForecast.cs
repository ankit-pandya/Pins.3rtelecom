using System;
using System.Collections.Generic;

namespace PinStoreAPI
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string Summary { get; set; }


        public class Employee
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string EmailId { get; set; }
        }

        public List<Employee> GetEmployees()
        {
            return new List<Employee>()
        {
            new Employee()
            {
                Id = 1,
                FirstName= "John",
                LastName = "Smith",
                EmailId ="John.Smith@gmail.com"
            },
            new Employee()
            {
                Id = 2,
                FirstName= "Jane",
                LastName = "Doe",
                EmailId ="Jane.Doe@gmail.com"
            }
        };
        }
    }

    
}
