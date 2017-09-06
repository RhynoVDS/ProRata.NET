<img src="https://ci.appveyor.com/api/projects/status/6ykjs2ttkx8s80yf?svg=true" />
Download: https://www.nuget.org/packages/ProRata.Net/

# ProRata.NET

A lightweight library to quickly Pro Rate a number across a collection.

ProRata.NET introduces an extension method to IEnumberable<T> called **ProRate** which can be called on any class that implements this interface. Pro Rating can be simple or weighted

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

## Rounding

It is possible to specify to what decimal place you want the function to round to. This is done using the **RoundTo** function.
The following is an example where the function will round each installment to 3 decimal places:

```csharp
var resultObj = people.ProRate(100)
                  .Weight(p=> p.length / sum_Name_Length)
                  .RoundTo(3)
                  .Calculate();
```


