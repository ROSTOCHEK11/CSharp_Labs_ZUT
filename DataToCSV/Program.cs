using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace GenericEvents_MiniProject
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string peopleFilePath = @"D:\C code\TimCorey\GenericEvents_MiniProjectApp\CSV_files\people.csv";
            string carsFilePath = @"D:\C code\TimCorey\GenericEvents_MiniProjectApp\CSV_files\cars.csv";



            List<PersonModel> people = new List<PersonModel>
            {

                new PersonModel{FirstName = "Vasia", LastName = "Pupkinheck", Email = "vasiapupkin@gmail.com"},
                new PersonModel{FirstName = "Petia", LastName = "Ivanov", Email = "petroivanov@ukr.net"},
                new PersonModel{FirstName = "Vania", LastName = "DarnPetrov", Email = "ivanpetrov@gmail.com"}

            };


            List<CarModel> cars = new List<CarModel>
            {

                new CarModel{Manufacturer = "Toyota", Model = "Corola"},
                new CarModel{Manufacturer = "BMW", Model = "M5"},
                new CarModel{Manufacturer = "Chevrolet", Model = "Camaro"}

            };

            DataAccess<PersonModel> peopleData = new DataAccess<PersonModel>();
            peopleData.BadEntryFound += PeopleData_BadEntryFound;

            peopleData.SaveToCSV(people, peopleFilePath);


            DataAccess<CarModel> carsData = new DataAccess<CarModel>();
            carsData.BadEntryFound += CarsData_BadEntryFound;

            carsData.SaveToCSV(cars, carsFilePath);


            ReadLine();

        }

        private static void CarsData_BadEntryFound(object sender, CarModel e)
        {
            WriteLine($"Bad Entry found for {e.Manufacturer} {e.Model}");
        }

        private static void PeopleData_BadEntryFound(object sender, PersonModel e)
        {
            WriteLine($"Bad Entry found for {e.FirstName} {e.LastName}");
        }
    }


    




}
