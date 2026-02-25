using System;

class Program
{
    const double Tax_Rate = 0.2;
    static double CalculatePay(double hours, double rate)
    {
        if (hours <0 || rate <0)
        {
            throw new Exception("Hours and rates must be positive");
        }    
    double gross = hours *rate;
    double tax = gross *Tax_Rate;
    double net = gross -tax;
    return net;
    }
    static void Main()
    {
        try
        {
            Console.Write("Enter Employee Name: ");
            string name = Console.ReadLine();

            string [] parts = name.Split(' ');
            if(parts.Length >= 2)
            {
                string first = parts[0];
                string last = parts[1];

                Console.Write("Enter Age: ");
                string ageText = Console.ReadLine();
                int age = int.Parse(ageText);

                Person p = new Person(first, last, age);

                Console.WriteLine("Full Name: " + p.FullName());
                Console.WriteLine("Is Adult: " + p.isAdult());
            }

            Console.Write("Hours Worked : ");
            string hoursText = Console.ReadLine();

            Console.Write("Hoursly Rate :");
            string rateText = Console.ReadLine();

            double hours = double.Parse(hoursText);
            double rate = double.Parse(rateText);
            double netPay = CalculatePay(hours, rate);
            Console.WriteLine(name + " earned $" + netPay.ToString("F2") + " after tax.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: "+ ex.Message);
        }

    }
}

public class Person
{
    public string FirstName{get; private set;}
    public string LastName{get; private set;}
    public int Age{get; private set;}

    public Person(string firstName, string lastName, int age)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name cannot be empty");

        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Last name cannot be empty");

        if (age<0)
            throw new ArgumentException("Age cannot be less than 0");

        FirstName = firstName;
        LastName  = lastName;
        Age = age;
    }
    public string FullName()
    {
        return $"{LastName}, {FirstName}";
    }
    public bool isAdult()
    {
        return Age >= 18;
    }

}