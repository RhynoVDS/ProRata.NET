<img src="https://ci.appveyor.com/api/projects/status/6ykjs2ttkx8s80yf?svg=true" />
Download: https://www.nuget.org/packages/ProRata.Net/

# ProRata.NET

A lightweight library to quickly Pro Rate a number across a collection.

ProRata.NET introduces an extension method to IEnumberable<T> called **ProRate** which can be called on any class that implements this interface. This means that it can Pro Rate against a List containing any object type. Pro Rating can be simple or weighted.

## Simple Pro Rate

The following is an example of a simple Pro Rata where the number 100 is pro rated evenly across a collection of strings:

```csharp
List<string> people = new List<string>();
people.Add("George");
people.Add("Sue");
people.Add("Sam");
people.Add("Simon");

var resultObj = people.ProRate(100)
                  .Calculate();
```

The above example will calculate 20 allocated to each string as this is an even pro rate.

## Calculate Return Value

Calculate will return an instance of **ProRateResult<T>** which contains a **Result** property that is a dictionary.
The dictionary has its keys equal to items in the collection. 

The following would give you the pro rate amount that was calculated for George

```csharp
var George_ProRataResult = resultObj.Result["George"];
```

The Sum of all the results will always equal to the amount that is being prorated.

## Weighted Pro Rate

Below is an example of a weighted Pro Rate where a number is pro rated based off a weight. 

In this example, the weight is calculated as the length of each persons name:

```csharp
List<string> people = new List<string>();
people.Add("George");
people.Add("Sue");
people.Add("Sam");
people.Add("Simon");

var sum_Name_Length = people.sum(r=> r.length);

var resultObj = people.ProRate(100)
                  .Weight(p=> p.length / sum_Name_Length)
                  .Calculate();
```

The the sum of all string lengths is 17
The weight is the strings length divided by 17.

This will allocated the following to each string:

George = 35.29 (6/17 * 100)
Sue = 17.65 (3/17 * 100)
Sam = 17.65 (3/17 * 100)
Simon = 29.41 (5/17 * 100)

## Rounding

It is possible to specify to what decimal place you want the function to round to. This is done using the **RoundTo** function.
The following is an example where the function will round each installment to 3 decimal places:

```csharp
var resultObj = people.ProRate(100)
                      .Weight(p=> p.length / sum_Name_Length)
                      .RoundTo(3)
                      .Calculate();
```

## Business Case Example

The following is an exampe of how how this can be used:

Suppose we have a university which needs to work out the cost of each semester for a course. 
Suppose we have a Course which costs $30,000. We have 4 Semesters in this course. The number of semesters changes per course.
Suppose in our code we have a course object and a Semester object. The course object contains a list of semesters. Using this framework we can do this:

```csharp
var result = Course.Semesters.ProRate(30000)
                                .Calculate();
```
With 4 semesters we will have 7500 assigned to each semester.
This is accessible as:
```csharp
decimal semester1_worth = result.Result[Semester1_object]
decimal semester2_worth = result.Result[Semester2_object]
decimal semester3_worth = result.Result[Semester3_object]
decimal semester4_worth = result.Result[Semester4_object]
```

Now suppose we have another requirement where the worth of each semester is based off the number of units in that semester. This would be a weighted pro rata where the number of units divided by the total units in the course would make the semester cost.

```csharp
var result= Course.Semesters.ProRate(30000)
                               .Weight(s=> s.Units.Count() / Course.total_units_count)
                               .Calculate();
```



