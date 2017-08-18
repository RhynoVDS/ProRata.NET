<img src="https://ci.appveyor.com/api/projects/status/6ykjs2ttkx8s80yf?svg=true" />

# ProRata.NET

A lightweight library to quickly ProRate a number across a collection.

ProRata.NET introduces an extension method to IEnumberable<T> called ProRate which can be called on any class that implements this interface. Pro Rating can be simple or weighted

##Simple Pro Rate

The following is an example of a simple Pro Rate where a number is pro rated evenly across a collection of strings:

```csharp
 List<string> test = new List<string>();
test.Add("George");
test.Add("Sue");
test.Add("Sam");
test.Add("Simon");

var resultObj = test.ProRate(100)
                  .Calculate();
```

##Calculate Return Value

Calculate will return an instance of **ProRateResult<T>** which contains a **Result** property that is a dictionary
The dictionary has its keys equal to items in the collection. 

The following would give you the pro rate amount that was calculated for George

```
var George_ProRataResult = resultObj.Result["George"];
```

##Weighted Pro Rate

Below is an example of a weighted Pro Rate where a number is pro rated based off a weight. 

The weight is calculated as the length of each persons name:

```csharp
List<string> test = new List<string>();
test.Add("George");
test.Add("Sue");
test.Add("Sam");
test.Add("Simon");

var sum_Name_Length = test.sum(r=> r.length);

var resultObj = test.ProRate(100)
                  .Weight(p=> p.length / sum_Name_Length)
                  .Calculate();
```
